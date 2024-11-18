using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private int healAmount = 0;  // Сколько здоровья восстанавливает рыбка

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // Если столкновение с пингвином
        {
            /*PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);  // Восстанавливаем здоровье пингвина
                Destroy(gameObject);  // Удаляем рыбку после столкновения
            }*/
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
