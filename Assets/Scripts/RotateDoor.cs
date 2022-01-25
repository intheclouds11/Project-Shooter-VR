using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotateDoor : MonoBehaviour
{
    [SerializeField] private float closeSpeed = 1f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Rotate()
    {
        // while (transform.eulerAngles.y <= 180)

        // this.transform.RotateAround(pivotPoint.position, Vector3.up, closeSpeed);
        rb.AddTorque(0, closeSpeed, 0);
    }
}

