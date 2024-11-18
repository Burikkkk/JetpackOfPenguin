using UnityEngine;

public class Orca : MonoBehaviour
{
    [SerializeField] private float speed = 2f;  // �������� �������� �������
    [SerializeField] private float lifespan = 10f;  // ����� ����� �������, ����� �������� ��� ��������

    private void Start()
    {
        // ���������� ������� ����� �������� �����
        //Destroy(gameObject, lifespan);
    }

    private void Update()
    {
        // ������� ������� �����
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // ���������, ���� ������������ � �������
        {
            Destroy(gameObject);  // ���������� �������
        }
    }
}
