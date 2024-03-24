using System;
using UnityEngine;

public class SoulArrowActor : MonoBehaviour
{
    public Transform root;
    public ParticleSystem particle;
    public MeshRenderer renderer;
    public AudioSource source;
    [Space]
    public float damage = 10;
    public float speed = 10;
    public float lifetime = 10;
    public GameObject collisionPref;
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(root.forward * speed, ForceMode.VelocityChange);
        Invoke(nameof(Destroy), lifetime);
    }

    private void OnCollisionEnter(Collision other)
    {
        var health = other.gameObject.GetComponent<Health>();
        if (health) health.DealDamage(damage);
        Instantiate(collisionPref, transform.position, root.rotation, root);
        Destroy(GetComponent<Rigidbody>());
        CancelInvoke(nameof(Destroy));
        Invoke(nameof(Destroy), 1);
        renderer.enabled = false;
        particle.Stop();
        source.Play();
    }

    private void Destroy()
    {
        Destroy(root.gameObject);
    }
}