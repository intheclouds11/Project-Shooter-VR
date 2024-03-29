using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> GameStateChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.Playing);
    }

    public void UpdateGameState(GameState newstate)
    {
        state = newstate;

        switch (newstate)
        {
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newstate), newstate, null);
        }

        GameStateChanged?.Invoke(newstate);
    }

    private void HandleLose()
    {
    }

    private void HandleWin()
    {
    }

    private void HandlePlaying()
    {
        
    }
}

public enum GameState
{
    Playing,
    Win,
    Lose,
}
