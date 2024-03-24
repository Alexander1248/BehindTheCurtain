using System;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public NPC NPC;
    public NPC[] NPCs;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        NPC.StartDialog();
        foreach (var npc in NPCs)
            if (npc) npc.StartDialog();
        Destroy(gameObject);
    }
}