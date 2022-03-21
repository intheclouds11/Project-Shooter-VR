using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmo : MonoBehaviour
{
    [SerializeField] private AmmoSlot[] _ammoSlots;
    private Text _ammoBeltText;
    private int _currentWeaponSlotAmmo;
    private int _currentWeaponSlotMaxAmmo;
    private WeaponType _currentWeaponType;
    [SerializeField] GameObject _ammoMag;

    [Serializable]
    private class AmmoSlot
    {
        public WeaponType _WeaponType;
        public int currentAmmo;
        public int maxAmmo;
    }

    private void Start()
    {
        _ammoBeltText = GetComponentInChildren<Text>();
    }

    public void ChangeAmmoBeltTextToCurrentWeapon(WeaponType weaponType)
    {
        _currentWeaponType = weaponType;
        _currentWeaponSlotAmmo = GetAmmoSlot(weaponType).currentAmmo;
        _currentWeaponSlotMaxAmmo = GetAmmoSlot(weaponType).maxAmmo;
        _ammoBeltText.text = $"{weaponType} Ammo: {_currentWeaponSlotAmmo} / {_currentWeaponSlotMaxAmmo}";
    }

    public int GetSlotAmmoAmount(WeaponType weaponType)
    {
        return GetAmmoSlot(weaponType).currentAmmo;
    }

    public int GetSlotMaxAmmoAmount(WeaponType weaponType)
    {
        return GetAmmoSlot(weaponType).maxAmmo;
    }

    public void AddAmmo(WeaponType weaponType, int amount)
    {
        GetAmmoSlot(weaponType).currentAmmo += amount;
        if (_currentWeaponType == weaponType)
        {
            _ammoBeltText.text =
                $"{weaponType} Ammo: {GetAmmoSlot(weaponType).currentAmmo} / {_currentWeaponSlotMaxAmmo}";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Instantiate(_ammoMag, transform.position, quaternion.identity);
            _ammoMag.GetComponent<AmmoMag>()._weaponType = _currentWeaponType;
            // _ammoMag._ammoAmount = 
            RemoveAmmoFromSlot();
            _ammoBeltText.text =
                $"{_currentWeaponType} Ammo: {GetSlotAmmoAmount(_currentWeaponType)} / {GetSlotMaxAmmoAmount(_currentWeaponType)}";
        }
    }

    private void RemoveAmmoFromSlot()
    {
        if (GetSlotAmmoAmount(_currentWeaponType) >= 10)
        {
            GetAmmoSlot(_currentWeaponType).currentAmmo -= 10;
        }
        else if (GetSlotAmmoAmount(_currentWeaponType) > 0)
        {
            GetAmmoSlot(_currentWeaponType).currentAmmo -= GetAmmoSlot(_currentWeaponType).currentAmmo;
        }
        else
        {
            Debug.Log("No ammo left in ammo belt!!");
        }
    }

    private AmmoSlot GetAmmoSlot(WeaponType weaponType)
    {
        foreach (var slot in _ammoSlots)
        {
            if (slot._WeaponType == weaponType)
            {
                return slot;
            }
        }

        return null;
    }
}