using System;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public NPC NPC;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        NPC.StartDialog();
        Destroy(gameObject);
    }
}