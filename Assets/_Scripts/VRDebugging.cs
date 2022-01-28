using UnityEngine;
using UnityEngine.XR;

public class VRDebugging : MonoBehaviour
{
    [SerializeField] private bool cursorLocked;
    [SerializeField] private GameObject XRDeviceSim;

    private void Start()
    {
        DetectHmd();
    }

    private void Update()
    {
        UpdateCursorLockState();
    }

    private void UpdateCursorLockState()
    {
        Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
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