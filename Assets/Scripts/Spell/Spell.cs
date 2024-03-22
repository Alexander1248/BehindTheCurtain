using System;
using UnityEngine;

[Serializable]
public abstract class Spell : MonoBehaviour
{
    public int manaCost;
    public float cooldown;
    private float _timer;
    public float Timer
    {
        get => _timer;
        set => _timer = value;
    }

    protected void Awake()
    {
        _timer = cooldown;
    }

    public abstract void Cast(Vector3 position, Quaternion rotation);
}