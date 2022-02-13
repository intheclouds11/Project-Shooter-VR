using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int _currentSceneIndex;

    public void ReloadScene()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_currentSceneIndex);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(_currentSceneIndex + 1);
    }
}