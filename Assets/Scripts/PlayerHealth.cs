using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public HPBar bar;  // ������ �� HPBar ��� ���������� UI
    private GameProgress gameProgress;

    void Start()
    {
        gameProgress = FindObjectOfType<GameProgress>();
        currentHealth = maxHealth;
        UpdateHealthUI();  // ������������� UI
    }

    // ����� ��� ���������� ��������
    public void TakeDamage()
    {
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthUI();
    }


    // ����� ��� ���������� ��������
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
        Debug.Log("����� �����!");

        GameProgress gameProgress = FindObjectOfType<GameProgress>();
        if (gameProgress != null)
        {
            gameProgress.ShowProgressOnGameOver();
        }

        // �������� ����� ��������� �����
        Invoke("LoadGameOverScene", 0.5f);  // �������� 0.5 ������� ����� ���������
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadSceneAsync(2);
    }

    // ���������� UI ��������
    private void UpdateHealthUI()
    {
        bar.UpdateHPBar(currentHealth - 1);  // ���������� ����������� �������� ����� HPBar
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            Heal(1);  // ��������������� �������� ��� ������������ � ������
            Destroy(collision.gameObject);  // ������� ����� ����� ������������
        }

        // ����� �������� ��������� ��������� � �������� (���������� ��������)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();  // ��������� �������� ��� ������������ � ���������
        }
    }
}
