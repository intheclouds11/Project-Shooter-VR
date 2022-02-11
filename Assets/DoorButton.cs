using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DoorButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float _openForce = -3f;
    private bool isUnlocked;

    private void Awake()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        if (!isUnlocked)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddRelativeForce(_openForce, 0, 0, ForceMode.Impulse);
        }

        isUnlocked = true;
    }

    public void Interact()
    {
        Unlock();
    }
}