using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float hitPoints = 100f;

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
    }
}
