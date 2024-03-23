using System;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public NPC NPC;
    private bool _used;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (_used) return;
        NPC.StartDialog();
        _used = true;
    }
}