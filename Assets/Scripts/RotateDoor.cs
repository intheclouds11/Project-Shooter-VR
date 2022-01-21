using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDoor : MonoBehaviour
{
    [SerializeField] private GameObject DoorTriggerGameObject;
    private DoorTrigger doorTrigger;
    private bool doorTriggered;
    [SerializeField] Transform pivotPoint;
    [SerializeField] private float closeSpeed = 1f;

    void Start()
    {
        doorTrigger = DoorTriggerGameObject.GetComponent<DoorTrigger>();
    }

    void Update()
    {
        doorTriggered = doorTrigger.doorTriggered;
        if (doorTriggered)
        {
            if (transform.eulerAngles.y >= 180)
            {
                doorTrigger.doorTriggered = false;
                return;
            }

            this.transform.RotateAround(pivotPoint.position, Vector3.up, closeSpeed * Time.deltaTime);
        }
    }
}