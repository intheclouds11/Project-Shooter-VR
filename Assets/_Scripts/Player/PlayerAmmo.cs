using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    [SerializeField] private int currentAmmo = 10;
    [SerializeField] private int maxAmmo = 20;
    
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
    }

    public void ReduceAmmo()
    {
        currentAmmo--;
    }
}