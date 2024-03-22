using System;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    public float speed = 10;
    public float lifetime = 10;
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0) 
            Destroy(gameObject);
    }
}