using UnityEngine;

public class HPBar : MonoBehaviour
{
    public GameObject[] healthPrefabs;  // Массив префабов изображений для HP
    private GameObject currentHealthImage;  // Текущий префаб изображения для HP

    private PlayerHealth playerHealth;  // Ссылка на PlayerHealth для отслеживания здоровья

    void Start()
    {
        // Пытаемся найти компонент PlayerHealth в сцене
        playerHealth = FindObjectOfType<PlayerHealth>();

        // Если объект не найден, выводим предупреждение
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth не найден в сцене. Убедитесь, что объект с этим компонентом присутствует на сцене.");
            return;
        }

        // Инициализируем HP бар
        UpdateHPBar(playerHealth.currentHealth);
    }

    void Update()
    {
        // Обновляем HP бар, когда здоровье меняется
        if (playerHealth != null)
        {
            UpdateHPBar(playerHealth.currentHealth);
        }
    }

    // Метод для обновления HP бара
    public void UpdateHPBar(int currentHealth)
    {
        // Если playerHealth отсутствует, прерываем выполнение метода
        if (playerHealth == null) return;

        int maxHealth = playerHealth.maxHealth;  // Получаем максимальное здоровье

        // Уничтожаем предыдущий префаб изображения (если есть)
        if (currentHealthImage != null)
        {
            Destroy(currentHealthImage);
        }


        // Индекс для массива префабов изображений на основе текущего здоровья
        int index = Mathf.FloorToInt((currentHealth / (float)maxHealth) * healthPrefabs.Length);
        index = Mathf.Clamp(index, 0, healthPrefabs.Length - 1);  // Убедимся, что индекс не выходит за границы

        // Создаем новый префаб изображения для отображения здоровья
        Vector3 newPosition = healthPrefabs[index].transform.position;
        newPosition.x += Camera.main.transform.position.x;  // Корректируем позицию с учетом камеры
        currentHealthImage = Instantiate(healthPrefabs[index], newPosition, Quaternion.identity, transform);  // Создаем префаб
    }
}
