using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C : MonoBehaviour
{
    private Vector3 mydir;
    private float myspeed;
    private bool active;
    [SerializeField] private GameObject boomPref;

    public void getDirection(Vector3 dir, float speed){
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        mydir = dir;
        myspeed = speed;
        active = true;
    }

    void Update(){
        if (!active) return;
        transform.position += mydir * myspeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")) return;
        GameObject parct = Instantiate(boomPref, transform.position, Quaternion.identity);
        Destroy(parct, 3);
        Destroy(gameObject);
    }
}
