using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VRDebugging : MonoBehaviour
{
    [SerializeField] private bool cursorLocked;
    [SerializeField] private GameObject XRDeviceSim;

    void Start()
    {
        DetectHmd();
    }

    private void Update()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void DetectHmd()
    {
        Debug.Log("XR Device detected: " + XRSettings.loadedDeviceName);

        if (XRSettings.loadedDeviceName == "OpenXR Display")
        {
            Debug.Log("Case 1: HMD detected");
            XRDeviceSim.SetActive(false); // disable XRDeviceSimulator while using HMD + controllers
        }
        else
        {
            Debug.Log("Case 2: No HMD detected");
            Cursor.lockState = CursorLockMode.Locked;
            cursorLocked = true;
        }
    }
}