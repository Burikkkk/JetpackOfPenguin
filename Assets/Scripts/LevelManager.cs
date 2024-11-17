using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Добавим для работы с загрузкой сцен

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;  // Массив всех уровней (фонов)
    [SerializeField] private float[] levelTimes;   // Время каждого уровня
    private int currentLevel;                      // Текущий уровень

    public bool IsFirstLevelActive { get; private set; }  // Флаг для проверки первого уровня

    void Start()
    {
        // Устанавливаем флаг первого уровня как активный
        IsFirstLevelActive = true;
    }

    void Update()
    {
        levelTimes[currentLevel] -= Time.deltaTime;
        if (levelTimes[currentLevel] <= 0.0f)
        {
            if (currentLevel == levelTimes.Length - 1)
            {
                // Если это второй уровень (последний), показываем сцену Win
                ShowWinScene();
            }
            else
            {
                // Переход на следующий уровень
                levels[currentLevel].GetComponent<LevelGraphics>().StartFading(false);
                currentLevel++;
                levels[currentLevel].GetComponent<LevelGraphics>().StartFading(true);

                // Если перешли на следующий уровень, то флаг переключается
                if (currentLevel > 0)
                {
                    IsFirstLevelActive = false;
                }
            }
        }
    }

    // Метод для показа сцены "Win"
    private void ShowWinScene()
    {
        // Загрузка сцены Win
        SceneManager.LoadScene("Win");
    }
}
