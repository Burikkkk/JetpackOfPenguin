using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Метод для начала игры
    public GameObject progress;
    public void StartGame()
    {
        if(progress != null)
            Destroy(progress);
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(1);
    }

    private void Start()
    {
        //progress = GameObject.FindWithTag();
    }

    // Метод для выхода из игры
    public void ExitGame()
    {

        // Если игра запущена в редакторе, то остановить игру
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Если игра запущена как сборка, то закрыть приложение
        Application.Quit();
        #endif
    }

    public void MainMenu()
    {
        if(progress != null)
            Destroy(progress);
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }

    
}
