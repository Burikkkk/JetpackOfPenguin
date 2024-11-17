using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private int healAmount = 1;  // ������� �������� ��������������� �����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // ���� ������������ � ���������
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);  // ��������������� �������� ��������
                Destroy(gameObject);  // ������� ����� ����� ������������
            }
        }
    }
}
