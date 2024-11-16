using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public HPBar bar;



    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        
    }

    public void TakeDamage()
    {
        currentHealth --;     
        if (currentHealth == 0)
        {
            Die();
        }
        UpdateHealthUI();
    }

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
        SceneManager.LoadSceneAsync(2); // Загрузка сцены поражения

    }

   

    private void UpdateHealthUI()
    {
        bar.UpdateHPBar(currentHealth);
    }

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            Heal(1);
            Destroy(collision.gameObject);
        }
    }
}
