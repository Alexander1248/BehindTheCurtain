using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Feya : MonoBehaviour
{
    private Transform player;

    [SerializeField] private Vector3 minRange;
    [SerializeField] private Vector3 maxRange;
    [SerializeField] private float speed;
    private Vector3 offset;

    private bool seePlayer;
    [SerializeField] private float seeDistance;

    [Space]
    public GameObject castPref;
    public AudioSource castSound;
    [FormerlySerializedAs("chargeDistance")] public float castShift;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        InvokeRepeating("ChangePos", 1, 2);
        InvokeRepeating(nameof(Attack), 1, 1);
    }
    
    void ChangePos(){
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        offset = dir * Random.Range(minRange[0], maxRange[0]) + Vector3.up * Random.Range(minRange[1], maxRange[1]);
    }

    private void Attack()
    {
        // Attack call
        if (!seePlayer) return;
        
        Instantiate(castPref, transform.position - transform.forward * castShift, 
            Quaternion.LookRotation((player.position - transform.position).normalized));
        castSound.Play();
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= seeDistance){
            seePlayer = true;
        }
        else if (seePlayer){
            seePlayer = false;
        }

        if (!seePlayer) return;

        transform.forward = (transform.position - new Vector3(player.position.x, transform.position.y, player.position.z)).normalized;
        transform.position = Vector3.MoveTowards(transform.position, player.position + offset, speed * Time.deltaTime);
    }
}
