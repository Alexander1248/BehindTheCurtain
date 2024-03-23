using System;
using UnityEngine;

public class Lightning : Spell
{
    public Attacking attacking;
    public GameObject hand;
    public Animator animator;

    private new void Awake()
    {
        base.Awake();
    }

    public override void Selected()
    {
        animator.enabled = false;
        hand.SetActive(true);
    }

    public override void Deselected()
    {
        hand.SetActive(false);
    }
    public override void Cast(Vector3 position, Quaternion rotation)
    {
        attacking.throwLightning();
    }
}