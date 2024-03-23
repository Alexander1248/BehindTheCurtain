using System;
using UnityEngine;

public class SoulArrowActor : MonoBehaviour
{
    public Transform root;
    public ParticleSystem particle;
    [Space]
    public float speed = 10;
    public float lifetime = 10;
    public GameObject collisionPref;
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(root.forward * speed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0) 
            Destroy(root.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(collisionPref, transform.position, root.rotation, root);
        Invoke(nameof(Destroy), 1);
        gameObject.SetActive(false);
    }

    private void Destroy()
    {
        Destroy(root.gameObject);
    }
}