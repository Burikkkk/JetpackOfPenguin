using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameProgress : MonoBehaviour
{
    [SerializeField] private float[] levelTimes; // Время прохождения каждого уровня
    private float totalGameTime;                 // Общее время прохождения всех уровней
    private float elapsedTime;                   // Время, проведенное в игре
    private int currentLevel;                    // Текущий уровень

    public Image progressBar;                    // UI-элемент для прогресс-бара
    public Sprite[] progressSprites;             // Массив спрайтов для отображения прогресса

    void Awake()
    {
        DontDestroyOnLoad(gameObject);           // Сохраняем объект при смене сцены
        LoadProgress();                          // Загружаем сохраненный прогресс при старте
    }

    void Start()
    {
        // Рассчитываем общее время игры
        totalGameTime = 0;
        foreach (float levelTime in levelTimes)
        {
            totalGameTime += levelTime;
        }

        currentLevel = CalculateCurrentLevel();  // Устанавливаем уровень на основе сохраненного прогресса

        UpdateProgressBar();                     // Обновляем прогресс-бар сразу при запуске
    }

    void Update()
    {
        // Увеличиваем `elapsedTime` в зависимости от времени на уровне
        if (currentLevel < levelTimes.Length)
        {
            elapsedTime += Time.deltaTime;

            // Проверяем, прошел ли игрок текущий уровень
            if (elapsedTime >= levelTimes[currentLevel])
            {
                elapsedTime = levelTimes[currentLevel]; // Ограничиваем до максимума времени уровня
                currentLevel++;                         // Переход к следующему уровню
                SaveProgress();                         // Сохраняем прогресс при переходе на новый уровень
            }
        }
    }

    // Вычисляем текущий уровень на основе времени
    private int CalculateCurrentLevel()
    {
        float accumulatedTime = 0;
        for (int i = 0; i < levelTimes.Length; i++)
        {
            accumulatedTime += levelTimes[i];
            if (elapsedTime < accumulatedTime)
                return i;
        }
        return levelTimes.Length - 1;
    }

    // Вычисляем процент прохождения
    public float CalculateProgressPercentage()
    {
        return (elapsedTime / totalGameTime) * 100;
    }

    // Метод для обновления прогресс-бара в зависимости от процента прохождения
    public void UpdateProgressBar()
    {
        float progressPercentage = CalculateProgressPercentage();

        // Определяем индекс изображения для прогресс-бара в зависимости от процентов прохождения
        int spriteIndex = 0;
        if (progressPercentage >= 75)
        {
            spriteIndex = 3;
        }
        else if (progressPercentage >= 50)
        {
            spriteIndex = 2;
        }
        else if (progressPercentage >= 25)
        {
            spriteIndex = 1;
        }
        else if (progressPercentage > 0)
        {
            spriteIndex = 0;
        }

        // Обновляем изображение прогресс-бара
        if (progressBar != null)
        {
            progressBar.enabled = true;              // Включаем прогресс-бар
            progressBar.sprite = progressSprites[spriteIndex];
        }
    }

    // Сохранение прогресса
    private void SaveProgress()
    {
        PlayerPrefs.SetFloat("GameProgressElapsedTime", elapsedTime);
        PlayerPrefs.Save();
    }

    // Загрузка прогресса
    private void LoadProgress()
    {
        if (PlayerPrefs.HasKey("GameProgressElapsedTime"))
        {
            elapsedTime = PlayerPrefs.GetFloat("GameProgressElapsedTime");
        }
        else
        {
            elapsedTime = 0;
        }
    }

    // Очистка прогресса (например, для новой игры)
    public void ResetProgress()
    {
        elapsedTime = 0;
        currentLevel = 0;
        PlayerPrefs.DeleteKey("GameProgressElapsedTime");
        UpdateProgressBar();
    }

    // Показ прогресс-бара на экране поражения
    public void ShowProgressOnGameOver()
    {
        if (progressBar != null)
        {
            progressBar.enabled = true;
            UpdateProgressBar();
        }
    }
}
