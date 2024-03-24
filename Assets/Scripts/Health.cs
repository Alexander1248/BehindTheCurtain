using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHP = 100;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private UnityEvent<float, float> onDamageDeal;

    private float _hp;


    private void Start()
    {
        _hp = maxHP;

    }

    public void DealDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            onDeath.Invoke();
            return;
        }
        onDamageDeal.Invoke(_hp, maxHP);
    }
    
    
}