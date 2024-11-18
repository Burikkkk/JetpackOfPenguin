using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public HPBar bar;  // Ссылка на HPBar для обновления UI
    private GameProgress gameProgress;

    void Start()
    {
        gameProgress = FindObjectOfType<GameProgress>();
        currentHealth = maxHealth;
        UpdateHealthUI();  // Инициализация UI
    }

    // Метод для уменьшения здоровья
    public void TakeDamage()
    {
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthUI();
    }


    // Метод для увеличения здоровья
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    public void Die()
    {
        Debug.Log("Игрок погиб!");

        GameProgress gameProgress = FindObjectOfType<GameProgress>();
        if (gameProgress != null)
        {
            gameProgress.ShowProgressOnGameOver();
        }

        // Задержка перед загрузкой сцены
        Invoke("LoadGameOverScene", 0.5f);  // Подождем 0.5 секунды перед загрузкой
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadSceneAsync(2);
    }

    // Обновление UI здоровья
    private void UpdateHealthUI()
    {
        bar.UpdateHPBar(currentHealth - 1);  // Обновление отображения здоровья через HPBar
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            Heal(1);  // Восстанавливаем здоровье при столкновении с рыбкой
            Destroy(collision.gameObject);  // Удаляем рыбку после столкновения
        }

        // Здесь добавьте обработку попадания в сосульки (уменьшение здоровья)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();  // Уменьшаем здоровье при столкновении с сосулькой
        }
    }
}
