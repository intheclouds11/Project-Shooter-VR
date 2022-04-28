using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace intheclouds
{
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

        [SerializeField] private ParticleSystem muzzleFlashVFX;
        [SerializeField] private GameObject hitVFX;
        [SerializeField] private Transform spawnAtRuntime;

        private AudioSource audioSource;
        [SerializeField] private AudioSource pumpAudioSource;

        [SerializeField] private AudioClip shootSFX;
        [SerializeField] private AudioClip emptyMagSFX;
        [SerializeField] private AudioClip reloadSFX;
        [SerializeField] private AudioClip pumpSFX;

        [SerializeField] private float range = 100f;

        [SerializeField] private float damage = 25f;

        private WeaponAmmo weaponAmmo;
        [SerializeField] private WeaponType weaponType;
        private TextMeshProUGUI ammoText;
        private bool canShoot = true;
        [SerializeField] private float autoFireRate = 0.2f;
        [SerializeField] private Transform raycastOrigin;
        private PlayerAmmo playerAmmo;

        private IEnumerator shootCoroutine;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            weaponAmmo = GetComponentInChildren<WeaponAmmo>();
            ammoText = GetComponentInChildren<TextMeshProUGUI>();
            playerAmmo = FindObjectOfType<PlayerAmmo>();
        }

        private void Start()
        {
            UpdateAmmoDisplay();
        }

        public void UpdateAmmoDisplay()
        {
            ammoText.text = $"{weaponAmmo.GetCurrentAmmo(weaponType)} / {weaponAmmo.GetMaxAmmo(weaponType)}";
        }

        public void PlayReloadSFX()
        {
            audioSource.PlayOneShot(reloadSFX);
        }

        public void HoldingWeapon()
        {
            playerAmmo.ChangeAmmoBeltTextToCurrentWeapon(weaponType);
        }

        public void ShootSemiAuto()
        {
            if (weaponAmmo.GetCurrentAmmo(weaponType) > 0)
            {
                audioSource.PlayOneShot(shootSFX);
                PlayMuzzleFlash();
                ProcessRaycast();
                weaponAmmo.ReduceAmmo(weaponType);
            }
        }

        public void ShootPumpAction()
        {
            if (weaponAmmo.GetCurrentAmmo(weaponType) > 0 && canShoot)
            {
                canShoot = false;
                audioSource.PlayOneShot(shootSFX);
                PlayMuzzleFlash();
                ProcessRaycast();
                weaponAmmo.ReduceAmmo(weaponType);
            }
        }

        public void PumpAction()
        {
            if (!canShoot)
            {
                pumpAudioSource.PlayOneShot(pumpSFX);
                canShoot = true;
            }
        }

        public void ShootRapidFireCoroutine()
        {
            shootCoroutine = ShootRapidFire();
            StartCoroutine(shootCoroutine);
            
            IEnumerator ShootRapidFire()
            {
                if (weaponAmmo.GetCurrentAmmo(weaponType) > 0)
                {
                    audioSource.PlayOneShot(shootSFX);
                    PlayMuzzleFlash();
                    ProcessRaycast();
                    weaponAmmo.ReduceAmmo(weaponType);
                    yield return new WaitForSeconds(autoFireRate);
                    yield return ShootRapidFire();
                }
            }
        }

        public void StopShooting()
        {
            StopCoroutine(shootCoroutine);
        }

        private void PlayMuzzleFlash()
        {
            muzzleFlashVFX.Play();
        }

        private void ProcessRaycast()
        {
            if (Physics.Raycast(raycastOrigin.position, transform.forward, out var hit, range) &&
                hit.transform != null)
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
            
            void CreateHitImpact(RaycastHit hit)
            {
                var impact = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
                impact.transform.parent = spawnAtRuntime;
            }
        }
    }
}