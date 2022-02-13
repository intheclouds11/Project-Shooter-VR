using System;
using UnityEngine;

public class RotateDoor : MonoBehaviour
{
    [SerializeField] private float closeSpeed = 1f;
    [SerializeField] private PlayerEnterTrigger _PlayerEnterTrigger;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _PlayerEnterTrigger.Trigger += Rotate;
    }

    private void OnDisable()
    {
        _PlayerEnterTrigger.Trigger -= Rotate;
    }

    public void Rotate()
    {
        _rb.AddTorque(0, closeSpeed, 0);
    }
}