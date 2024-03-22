using System;
using UnityEngine;

public class Lightning : Spell
{
    public Attacking attacking;

    private Transform _root;
    private new void Awake()
    {
        base.Awake();
        _root = GameObject.Find("Spells").transform;
    }

    public override void Cast(Vector3 position, Quaternion rotation)
    {
        
    }
}