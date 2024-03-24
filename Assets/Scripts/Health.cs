using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHP = 100;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private UnityEvent<float, float> onDamageDeal;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float _hp;

    [SerializeField] private int amountRestore;

    [SerializeField] private bool autoHeal;


    private void Start()
    {
        _hp = maxHP;

    }

    void restoretoplayer(){
        GameObject.FindAnyObjectByType<SpellCaster>().RestoreCells(amountRestore);
    }

    void heal(){
        _hp += 3;
        if (_hp >= maxHP){
            _hp = maxHP;
            CancelInvoke("heal");
        }
        onDamageDeal.Invoke(_hp, maxHP);
    }

    public void DealDamage(float damage, Vector3 kick)
    {
        if (rb) rb.AddForce(kick, ForceMode.Impulse);

        _hp -= damage;
        onDamageDeal.Invoke(_hp, maxHP);
        if (autoHeal){
            CancelInvoke("heal");
            InvokeRepeating("heal", 4, 0.5f);
        }
        if (_hp <= 0)
        {
            restoretoplayer();
            onDeath.Invoke();
            return;
        }
    }
    
    
}