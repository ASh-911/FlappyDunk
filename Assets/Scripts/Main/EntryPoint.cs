using FlappyDank.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    private static EntryPoint _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            StartInitialization();
            SceneManager.LoadSceneAsync("GameScene");
        }
        else
        {
            Debug.LogError("Only one instance of EntryPoint is allowed");
        }
    }

    private void StartInitialization()
    {
        SuperController.Init();
    }
}
