using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    [SerializeField] private float[] levelTimes;
    private int currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        levelTimes[currentLevel] -= Time.deltaTime; 
        if (levelTimes[currentLevel]<=0.0f)
        {
            if(currentLevel==levelTimes.Length-1)
            {
                //lastWindow.SetActive(true); //окно победы/поражени€
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        // ≈сли игра запущена как сборка, то закрыть приложение
        Application.Quit();
#endif
            }
            else
            {
                levels[currentLevel].GetComponent<LevelGraphics>().StartFading(false);
                currentLevel++;
                levels[currentLevel].GetComponent<LevelGraphics>().StartFading(true);


            }
        }
    }
}
