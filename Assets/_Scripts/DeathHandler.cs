using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] public GameObject MenuCanvas;
    public GameObject LoseScreen;
    public GameObject WinScreen;

    // private void Awake()
    // {
    //     MenuCanvas.SetActive(false);
    //     LoseScreen.SetActive(false);
    //     WinScreen.SetActive(false);
    // }
    //
    // public void HandleDeath()
    // {
    //     MenuCanvas.SetActive(true);
    //     LoseScreen.SetActive(true);
    // }
}
