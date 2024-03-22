using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feya : MonoBehaviour
{
    private Transform player;

    [SerializeField] private Vector3 minRange;
    [SerializeField] private Vector3 maxRange;
    [SerializeField] private float speed;
    private Vector3 offset;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        InvokeRepeating("ChangePos", 1, 2);
    }
    
    void ChangePos(){
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        offset = dir * Random.Range(minRange[0], maxRange[0]) + Vector3.up * Random.Range(minRange[1], maxRange[1]);
    }

    void Update()
    {
        transform.forward = (transform.position - new Vector3(player.position.x, transform.position.y, player.position.z)).normalized;
        transform.position = Vector3.MoveTowards(transform.position, player.position + offset, speed * Time.deltaTime);
    }
}
