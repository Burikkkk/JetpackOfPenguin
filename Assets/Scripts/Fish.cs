using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private int healAmount = 0;  // ������� �������� ��������������� �����

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))  // ���� ������������ � ���������
        {
            /*PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);  // ��������������� �������� ��������
                Destroy(gameObject);  // ������� ����� ����� ������������
            }*/
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
