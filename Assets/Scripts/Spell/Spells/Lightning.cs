using System;
using UnityEngine;

public class Lightning : Spell
{
    public Attacking attacking;
    public GameObject hand;

    private Transform _root;
    private new void Awake()
    {
        base.Awake();
        _root = GameObject.Find("Spells").transform;
        attacking.Root = _root;
    }

    public override void Selected()
    {
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