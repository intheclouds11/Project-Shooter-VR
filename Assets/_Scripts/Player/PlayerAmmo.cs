using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using XRController = UnityEngine.InputSystem.XR.XRController;

public class PlayerAmmo : MonoBehaviour
{
    [SerializeField] private AmmoSlot[] _ammoSlots;
    private Text _ammoBeltText;
    private int _currentWeaponSlotAmmo;
    private int _currentWeaponSlotMaxAmmo;
    private WeaponType _currentWeaponType;
    [SerializeField] GameObject _ammoMagPrefab;

    [SerializeField] InputActionAsset playerControlsVR;
    private InputAction rhGrip;
    private InputAction lhGrip;

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
        EnableVRControls();
    }

    private void EnableVRControls()
    {
        var gameplayActionMapRH = playerControlsVR.FindActionMap("XRI RightHand");
        var gameplayActionMapLH = playerControlsVR.FindActionMap("XRI LeftHand");

        rhGrip = gameplayActionMapRH.FindAction("Select");
        rhGrip.Enable();
        lhGrip = gameplayActionMapLH.FindAction("Select");
        lhGrip.Enable();
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
        if (other.CompareTag("Right Hand"))
        {
            Debug.Log("player ammo trigger");
            rhGrip.performed += SpawnAmmoMag;
        }
        else if (other.CompareTag("Left Hand"))
        {
            Debug.Log("player ammo trigger");
            lhGrip.performed += SpawnAmmoMag;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Right Hand"))
        {
            rhGrip.performed -= SpawnAmmoMag;
        }
        else if (other.CompareTag("Left Hand"))
        {
            lhGrip.performed -= SpawnAmmoMag;
        }
    }

    private void SpawnAmmoMag(InputAction.CallbackContext obj)
    {
        Debug.Log("spawn player ammo");
        Instantiate(_ammoMagPrefab, transform.position, quaternion.identity);
        AmmoMag ammoMag = _ammoMagPrefab.GetComponent<AmmoMag>();
        ammoMag._weaponType = _currentWeaponType;
        ammoMag._ammoAmount = RemoveAmmoFromSlot();
        ammoMag.GetComponentInChildren<Text>().text = $"{ammoMag._ammoAmount + " " + ammoMag._weaponType + " Rounds"}";
        _ammoBeltText.text =
            $"{_currentWeaponType} Ammo: {GetSlotAmmoAmount(_currentWeaponType)} / {GetSlotMaxAmmoAmount(_currentWeaponType)}";
    }

    private int RemoveAmmoFromSlot()
    {
        if (GetSlotAmmoAmount(_currentWeaponType) >= 10)
        {
            return GetAmmoSlot(_currentWeaponType).currentAmmo -= 10;
        }
        else if (GetSlotAmmoAmount(_currentWeaponType) > 0)
        {
            return GetAmmoSlot(_currentWeaponType).currentAmmo -= GetAmmoSlot(_currentWeaponType).currentAmmo;
        }
        else
        {
            Debug.Log("No ammo left in ammo belt!!");
            return 0;
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