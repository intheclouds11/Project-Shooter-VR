using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    [SerializeField] private AmmoSlot[] _ammoSlots;

    // TODO: will delete during course
    [SerializeField] private int currentAmmo = 10;
    [SerializeField] private int maxAmmo = 20;
    
    [Serializable]
    private class AmmoSlot
    {
        public AmmoType _ammoType;
        public int currentAmmo;
        public int maxAmmo;
    }

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