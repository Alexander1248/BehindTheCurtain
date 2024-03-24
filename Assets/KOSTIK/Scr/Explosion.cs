using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius;
    public float damage;

    void Start()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, Vector3.up);

        for(int i = 0; i < hits.Length; i++){
            if (hits[i].collider.gameObject.CompareTag("Player")) continue;
            var health = hits[i].collider.gameObject.GetComponent<Health>();
            if (health != null) {
                health.DealDamage(damage, Vector3.zero);
            }
        }
    }
}
