using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using intheclouds;
using UnityEngine;

public class AmmoMag : MonoBehaviour
{
    public WeaponType _weaponType;
    public int _ammoAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Assault Rifle") && _weaponType == WeaponType.Assault)
        {
            WeaponAmmo weaponAmmo = other.GetComponent<WeaponAmmo>();
            weaponAmmo.Reload(_weaponType, _ammoAmount);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Shotgun") && _weaponType == WeaponType.Shotgun)
        {
            WeaponAmmo weaponAmmo = other.GetComponent<WeaponAmmo>();
            weaponAmmo.Reload(_weaponType, _ammoAmount);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Handgun") && _weaponType == WeaponType.Handgun)
        {
            WeaponAmmo weaponAmmo = other.GetComponent<WeaponAmmo>();
            weaponAmmo.Reload(_weaponType, _ammoAmount);
            Destroy(this.gameObject);
        }
    }
}