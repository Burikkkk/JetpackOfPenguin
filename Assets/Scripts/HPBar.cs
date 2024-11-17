using UnityEngine;

public class HPBar : MonoBehaviour
{
    public GameObject[] healthPrefabs;  // ������ �������� ����������� ��� HP
    private GameObject currentHealthImage;  // ������� ������ ����������� ��� HP

    private PlayerHealth playerHealth;  // ������ �� PlayerHealth ��� ������������ ��������

    void Start()
    {
        // �������� ����� ��������� PlayerHealth � �����
        playerHealth = FindObjectOfType<PlayerHealth>();

        // ���� ������ �� ������, ������� ��������������
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth �� ������ � �����. ���������, ��� ������ � ���� ����������� ������������ �� �����.");
            return;
        }

        // �������������� HP ���
        UpdateHPBar(playerHealth.currentHealth);
    }

    void Update()
    {
        // ��������� HP ���, ����� �������� ��������
        if (playerHealth != null)
        {
            UpdateHPBar(playerHealth.currentHealth);
        }
    }

    // ����� ��� ���������� HP ����
    public void UpdateHPBar(int currentHealth)
    {
        // ���� playerHealth �����������, ��������� ���������� ������
        if (playerHealth == null) return;

        int maxHealth = playerHealth.maxHealth;  // �������� ������������ ��������

        // ���������� ���������� ������ ����������� (���� ����)
        if (currentHealthImage != null)
        {
            Destroy(currentHealthImage);
        }


        // ������ ��� ������� �������� ����������� �� ������ �������� ��������
        int index = Mathf.FloorToInt((currentHealth / (float)maxHealth) * healthPrefabs.Length);
        index = Mathf.Clamp(index, 0, healthPrefabs.Length - 1);  // ��������, ��� ������ �� ������� �� �������

        // ������� ����� ������ ����������� ��� ����������� ��������
        Vector3 newPosition = healthPrefabs[index].transform.position;
        newPosition.x += Camera.main.transform.position.x;  // ������������ ������� � ������ ������
        currentHealthImage = Instantiate(healthPrefabs[index], newPosition, Quaternion.identity, transform);  // ������� ������
    }
}
