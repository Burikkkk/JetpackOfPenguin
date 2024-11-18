using UnityEngine;

public class Orca : MonoBehaviour
{
    [SerializeField] private float speed = 2f;  // Скорость движения касатки
    [SerializeField] private float lifespan = 10f;  // Время жизни касатки, после которого она исчезает

    private void Start()
    {
        // Уничтожаем касатку через заданное время
        //Destroy(gameObject, lifespan);
    }

    private void Update()
    {
        // Двигаем касатку влево
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Проверяем, если столкновение с игроком
        {
            Destroy(gameObject);  // Уничтожаем касатку
        }
    }
}
