using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;


    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        Debug.Log($"Hit! HP remaining: {hitPoints}");
        
        if (hitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}