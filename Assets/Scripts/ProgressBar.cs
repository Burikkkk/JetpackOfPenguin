using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameProgress : MonoBehaviour
{
    [SerializeField] private float[] levelTimes; // ����� ����������� ������� ������
    private float totalGameTime;                 // ����� ����� ����������� ���� �������
    private float elapsedTime;                   // �����, ����������� � ����
    private int currentLevel;                    // ������� �������

    public Image progressBar;                    // UI-������� ��� ��������-����
    public Sprite[] progressSprites;             // ������ �������� ��� ����������� ���������

    void Awake()
    {
        DontDestroyOnLoad(gameObject);           // ��������� ������ ��� ����� �����
        LoadProgress();                          // ��������� ����������� �������� ��� ������
    }

    void Start()
    {
        // ������������ ����� ����� ����
        totalGameTime = 0;
        foreach (float levelTime in levelTimes)
        {
            totalGameTime += levelTime;
        }

        currentLevel = CalculateCurrentLevel();  // ������������� ������� �� ������ ������������ ���������

        UpdateProgressBar();                     // ��������� ��������-��� ����� ��� �������
    }

    void Update()
    {
        // ����������� `elapsedTime` � ����������� �� ������� �� ������
        if (currentLevel < levelTimes.Length)
        {
            elapsedTime += Time.deltaTime;

            // ���������, ������ �� ����� ������� �������
            if (elapsedTime >= levelTimes[currentLevel])
            {
                elapsedTime = levelTimes[currentLevel]; // ������������ �� ��������� ������� ������
                currentLevel++;                         // ������� � ���������� ������
                SaveProgress();                         // ��������� �������� ��� �������� �� ����� �������
            }
        }
    }

    // ��������� ������� ������� �� ������ �������
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

    // ��������� ������� �����������
    public float CalculateProgressPercentage()
    {
        return (elapsedTime / totalGameTime) * 100;
    }

    // ����� ��� ���������� ��������-���� � ����������� �� �������� �����������
    public void UpdateProgressBar()
    {
        float progressPercentage = CalculateProgressPercentage();

        // ���������� ������ ����������� ��� ��������-���� � ����������� �� ��������� �����������
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

        // ��������� ����������� ��������-����
        if (progressBar != null)
        {
            progressBar.enabled = true;              // �������� ��������-���
            progressBar.sprite = progressSprites[spriteIndex];
        }
    }

    // ���������� ���������
    private void SaveProgress()
    {
        PlayerPrefs.SetFloat("GameProgressElapsedTime", elapsedTime);
        PlayerPrefs.Save();
    }

    // �������� ���������
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

    // ������� ��������� (��������, ��� ����� ����)
    public void ResetProgress()
    {
        elapsedTime = 0;
        currentLevel = 0;
        PlayerPrefs.DeleteKey("GameProgressElapsedTime");
        UpdateProgressBar();
    }

    // ����� ��������-���� �� ������ ���������
    public void ShowProgressOnGameOver()
    {
        if (progressBar != null)
        {
            progressBar.enabled = true;
            UpdateProgressBar();
        }
    }
}
