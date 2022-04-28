using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;

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
        menuCanvas.SetActive(state == GameState.Lose || state == GameState.Win);
        loseScreen.SetActive(true);
        winScreen.SetActive(state == GameState.Win);
    }
}