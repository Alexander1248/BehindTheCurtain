using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTrigger : MonoBehaviour
{
    public int myID;
    public GameManager gameManager;
    public bool destroyOnActivate;

    void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Player")) return;
        gameManager.triggerActivated(myID);
        if (destroyOnActivate) Destroy(gameObject);
    }
}
