using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // ����� ��� ������ ����
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

    // ����� ��� ������ �� ����
    public void ExitGame()
    {

        // ���� ���� �������� � ���������, �� ���������� ����
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // ���� ���� �������� ��� ������, �� ������� ����������
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
