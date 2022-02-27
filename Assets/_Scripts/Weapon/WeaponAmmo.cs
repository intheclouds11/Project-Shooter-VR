using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private int currentAmmo = 10;
    [SerializeField] private int maxAmmo = 20;

    public int GetCurrentAmmo(WeaponType weaponType)
    {
        return currentAmmo;
    }

    public int GetMaxAmmo(WeaponType weaponType)
    {
        return maxAmmo;
    }

    public void Reload(WeaponType weaponType, int amount)
    {
        if (weaponType == _weaponType)
        {
            currentAmmo += amount;
        }
        else
        {
            Debug.Log("!!Tried adding incorrect ammo type!!");
        }
    }

    public void ReduceAmmo(WeaponType weaponType)
    {
        currentAmmo--;
    }
}