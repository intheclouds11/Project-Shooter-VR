using System;
using System.Collections;
using System.Collections.Generic;
using intheclouds;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponAmmo : MonoBehaviour
{
    [FormerlySerializedAs("_weaponType")] [SerializeField] private WeaponType weaponType;
    [SerializeField] private int currentAmmo = 10;
    [SerializeField] private int maxAmmo = 20;
    private Weapon weapon;

    private void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }

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
        if (weaponType == this.weaponType)
        {
            currentAmmo += amount;
            weapon.UpdateAmmoDisplay();
            weapon.PlayReloadSFX();
        }
        else
        {
            Debug.Log("!!Tried adding incorrect ammo type!!");
        }
    }

    public void ReduceAmmo(WeaponType weaponType)
    {
        currentAmmo--;
        weapon.UpdateAmmoDisplay();
    }
}