using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticles : MonoBehaviour
{
    [SerializeField] private PlayerEnterTrigger _playerEnterTrigger;
    private ParticleSystem _particleSystem;
    
    void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        _playerEnterTrigger.Trigger += _particleSystem.Play;
    } 
    private void OnDisable()
    {
        _playerEnterTrigger.Trigger -= _particleSystem.Play;
    }
}
