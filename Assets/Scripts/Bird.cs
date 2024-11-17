using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float speed = 3f;  // Скорость полета птицы
    [SerializeField] private float verticalRange = 2f;  // Максимальное вертикальное отклонение
    [SerializeField] private float verticalSpeed = 2f;  // Скорость вертикальных колебаний (амплитуда)
    [SerializeField] private float randomOffsetTime = 0f; // Сдвиг по времени для случайной траектории

    private float initialYPosition;  // Начальная высота
    private float verticalOffset;    // Отклонение для каждой птицы (генерируется случайно)
    private Vector3 initialPosition; // Начальная позиция птицы для учета времени

    private void Start()
    {
        // Генерация случайного отклонения по времени для уникальной траектории каждой птицы
        verticalOffset = Random.Range(-Mathf.PI, Mathf.PI);  // Случайная траектория (от -π до π)
        initialYPosition = transform.position.y;  // Запоминаем начальную высоту

        // Сдвиг начальной позиции птицы по времени для уникальной траектории
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Двигаем птицу по горизонтали влево с фиксированной скоростью
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Двигаем птицу по вертикали с синусоидальной траекторией (верх-низ)
        float newY = initialYPosition + Mathf.Sin(Time.time * verticalSpeed + verticalOffset + randomOffsetTime) * verticalRange;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Метод для старта полета птицы
    public void StartFlying()
    {
        // Можно добавить начальную анимацию или поведение при старте
    }

    // При столкновении с игроком
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Если столкновение с игроком
        {
            Destroy(gameObject);  // Уничтожаем птицу
        }
    }
}
