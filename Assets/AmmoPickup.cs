using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private GameObject _fxPrefab;
    [SerializeField] private int _ammoAmount;
    [SerializeField] private WeaponType _weaponType;
    private PlayerAmmo _playerAmmo;

// levitating variables. Put into reusable levitating script instead?
    [SerializeField] private float _levitateMagnitude;
    private Vector3 tempPos;
    private Vector3 positionOffset;
    public float frequency;

    void Start()
    {
        positionOffset = transform.position;
        _playerAmmo = FindObjectOfType<PlayerAmmo>();
    }

    void Update()
    {
        tempPos = positionOffset;
        tempPos.y += Mathf.Sin(frequency * Time.fixedTime) * _levitateMagnitude;
        transform.position = tempPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            if (_playerAmmo != null) _playerAmmo.AddAmmo(_weaponType, _ammoAmount);

            Instantiate(_fxPrefab, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
    }
}