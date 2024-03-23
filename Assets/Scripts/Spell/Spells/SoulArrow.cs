using System;
using UnityEngine;

public class SoulArrow : Spell
{
    public GameObject prefab;

    private Transform _root;
    private new void Awake()
    {
        base.Awake();
        _root = GameObject.Find("Spells").transform;
    }

    public override void Selected()
    {
    }

    public override void Deselected()
    {
    }

    public override void Cast(Vector3 position, Quaternion rotation)
    {
        var dir = rotation * Vector3.forward;
        Instantiate(prefab, position + dir * 2, rotation, _root);
    }
}