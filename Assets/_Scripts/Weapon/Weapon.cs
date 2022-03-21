using System.Collections;
using TMPro;
using UnityEngine;

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

        [Header("VFX")] [SerializeField] private ParticleSystem muzzleFlashVFX;
        [SerializeField] private GameObject hitVFX;
        private Transform _spawnAtRuntime;

        [Header("Audio")] private AudioSource _audioSource;
        [SerializeField] private AudioSource _pumpAudioSource;
        
        [SerializeField] private AudioClip shootSFX;
        [SerializeField] private AudioClip emptyMagSFX;
        [SerializeField] private AudioClip reloadSFX;
        [SerializeField] private AudioClip pumpSFX;

        [Header("Weapon Stats")] [SerializeField] private float range = 100f;
        [SerializeField] private float damage = 25f;

        private WeaponAmmo _weaponAmmo;
        [SerializeField] private WeaponType _weaponType;
        private TextMeshProUGUI _ammoText;
        private bool _canShoot = true;
        [SerializeField] private float _autoFireRate = 0.2f;
        [SerializeField] private Transform _raycastOrigin;
        private PlayerAmmo _playerAmmo;

        private IEnumerator _shootCoroutine;

        private void Awake()
        {
            _spawnAtRuntime = GameObject.FindWithTag("Spawn at Runtime").transform;
            _audioSource = GetComponent<AudioSource>();
            _weaponAmmo = GetComponent<WeaponAmmo>();
            _ammoText = GetComponentInChildren<TextMeshProUGUI>();
            _playerAmmo = FindObjectOfType<PlayerAmmo>();
        }

        private void Start()
        {
            // _xrBaseInteractable = GetComponent<XRBaseInteractable>();
            // _leftTriggerAction = leftActionBasedController.activateAction.action;
            // _rightTriggerAction = rightActionBasedController.activateAction.action;
            _ammoText.text = $"{_weaponAmmo.GetCurrentAmmo(_weaponType)} / {_weaponAmmo.GetMaxAmmo(_weaponType)}";
        }

        public void HoldingWeapon()
        {
            _playerAmmo.ChangeAmmoBeltTextToCurrentWeapon(_weaponType);
        }

        public void ShootSemiAuto()
        {
            if (_weaponAmmo.GetCurrentAmmo(_weaponType) > 0)
            {
                _audioSource.PlayOneShot(shootSFX);
                PlayMuzzleFlash();
                ProcessRaycast();
                _weaponAmmo.ReduceAmmo(_weaponType);
                _ammoText.text = $"{_weaponAmmo.GetCurrentAmmo(_weaponType)} / {_weaponAmmo.GetMaxAmmo(_weaponType)}";
            }
        }

        public void ShootPumpAction()
        {
            if (_weaponAmmo.GetCurrentAmmo(_weaponType) > 0 && _canShoot)
            {
                _canShoot = false;
                _audioSource.PlayOneShot(shootSFX);
                PlayMuzzleFlash();
                ProcessRaycast();
                _weaponAmmo.ReduceAmmo(_weaponType);
                _ammoText.text = $"{_weaponAmmo.GetCurrentAmmo(_weaponType)} / {_weaponAmmo.GetMaxAmmo(_weaponType)}";
            }
        }

        public void PumpAction()
        {
            if (!_canShoot)
            {
                _pumpAudioSource.PlayOneShot(pumpSFX);
                _canShoot = true;
            }
        }

        public void ShootRapidFireCoroutine()
        {
            _shootCoroutine = ShootRapidFire();
            StartCoroutine(_shootCoroutine);
        }

        public void StopShooting()
        {
            StopCoroutine(_shootCoroutine);
        }

        private IEnumerator ShootRapidFire()
        {
            if (_weaponAmmo.GetCurrentAmmo(_weaponType) > 0)
            {
                _audioSource.PlayOneShot(shootSFX);
                PlayMuzzleFlash();
                ProcessRaycast();
                _weaponAmmo.ReduceAmmo(_weaponType);
                _ammoText.text = $"{_weaponAmmo.GetCurrentAmmo(_weaponType)} / {_weaponAmmo.GetMaxAmmo(_weaponType)}";
                yield return new WaitForSeconds(_autoFireRate);
                yield return ShootRapidFire();
            }
        }

        private void PlayMuzzleFlash()
        {
            muzzleFlashVFX.Play();
        }

        private void ProcessRaycast()
        {
            if (Physics.Raycast(_raycastOrigin.position, transform.forward, out var hit, range) && hit.transform != null)
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
}