using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject LoseScreen;
    [SerializeField] private GameObject WinScreen;

    private void OnEnable()
    {
        GameManager.GameStateChanged += DisplayMenu;
    }

    private void OnDisable()
    {
        GameManager.GameStateChanged -= DisplayMenu;
    }

    private void DisplayMenu(GameState state)
    {
        MenuCanvas.SetActive(state == GameState.Lose || state == GameState.Win);
        LoseScreen.SetActive(state == GameState.Lose);
        WinScreen.SetActive(state == GameState.Win);
    }
}
