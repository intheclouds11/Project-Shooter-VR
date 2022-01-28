using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private PlayerHealth _targetHealth;
    [SerializeField] private float _damage = 40f;

    private void Awake()
    {
        _targetHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if (_targetHealth == null) return;
        
        _targetHealth.TakeDamage(_damage);
        
        if (_targetHealth.hitPoints <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}