using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon : MonoBehaviour
{
    // [Header("Input")]
    // // private XRBaseInteractable _xrBaseInteractable;
    // [SerializeField]
    // private ActionBasedController leftActionBasedController;
    //
    // [SerializeField] private ActionBasedController rightActionBasedController;
    // private InputAction _leftTriggerAction;
    // private InputAction _rightTriggerAction;

    [Header("VFX")] [SerializeField] private ParticleSystem muzzleFlashVFX;
    [SerializeField] private GameObject hitVFX;
    private Transform _spawnAtRuntime;

    [Header("Audio")] private AudioSource _sfx;
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private AudioClip emptyMagSFX;
    [SerializeField] private AudioClip reloadSFX;

    [Header("Weapon Stats")] [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 25f;

    private WeaponAmmo _weaponAmmo;
    [SerializeField] private PlayerAmmo _playerAmmo;
    private TextMeshProUGUI _ammoText;

    private void Awake()
    {
        _spawnAtRuntime = GameObject.FindWithTag("Spawn at Runtime").transform;
        _sfx = GetComponent<AudioSource>();
        _weaponAmmo = GetComponent<WeaponAmmo>();
        _playerAmmo = FindObjectOfType<PlayerAmmo>();
        _ammoText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        // _xrBaseInteractable = GetComponent<XRBaseInteractable>();
        // _leftTriggerAction = leftActionBasedController.activateAction.action;
        // _rightTriggerAction = rightActionBasedController.activateAction.action;
        _ammoText.text = $"{_weaponAmmo.GetCurrentAmmo()} / {_weaponAmmo.GetMaxAmmo()}";
    }

    private void Update()
    {
        // Now I'm calling shoot from GrabInteractable Activate event
        // if (_xrBaseInteractable.isSelected)
        // {
        //     if (_leftTriggerAction.triggered || _rightTriggerAction.triggered)
        //     {
        //         Shoot();
        //     }
        // }
    }

    public void Shoot()
    {
        if (_weaponAmmo.GetCurrentAmmo() > 0)
        {
            _sfx.PlayOneShot(shootSFX);
            PlayMuzzleFlash();
            ProcessRaycast();
            _weaponAmmo.ReduceAmmo();
            _ammoText.text = $"{_weaponAmmo.GetCurrentAmmo()} / {_weaponAmmo.GetMaxAmmo()}";
        }
        
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlashVFX.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range) && hit.transform != null)
        {
            CreateHitImpact(hit);
            if (hit.transform.TryGetComponent(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
            }
            else if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                if (interactable == null) return;
                interactable.Interact();
            }
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        var impact = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        impact.transform.parent = _spawnAtRuntime;
    }
}