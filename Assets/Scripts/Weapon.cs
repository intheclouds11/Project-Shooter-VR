using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon : MonoBehaviour
{
    private XRBaseInteractable XRBaseInteractable;
    [SerializeField] ActionBasedController leftActionBasedController;
    [SerializeField] ActionBasedController rightActionBasedController;
    private InputAction leftTriggerAction;
    private InputAction rightTriggerAction;
    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject hitVFX;
    private Transform spawnAtRuntime;

    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 25f;

    void Start()
    {
        XRBaseInteractable = GetComponent<XRBaseInteractable>();
        leftTriggerAction = leftActionBasedController.activateAction.action;
        rightTriggerAction = rightActionBasedController.activateAction.action;
        spawnAtRuntime = GameObject.FindWithTag("Spawn at Runtime").transform;
    }

    void Update()
    {
        if (XRBaseInteractable.isSelected)
        {
            if (leftTriggerAction.triggered || rightTriggerAction.triggered)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
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
            if (hit.transform.CompareTag("Enemy"))
            {
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                target.TakeDamage(damage);
            }
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        var impact = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        impact.transform.parent = spawnAtRuntime;
    }
}