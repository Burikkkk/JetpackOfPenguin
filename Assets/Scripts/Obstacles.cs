using System.Collections.Generic;
using UnityEngine;

public class IcicleGenerator : MonoBehaviour
{
    [Header("Общие настройки")]
    [SerializeField] private float initialScrollSpeed = 1.0f; // Начальная скорость движения препятствий
    [SerializeField] private float maxScrollSpeed = 3.0f; // Максимальная скорость
    [SerializeField] private float difficultyIncreaseRate = 0.1f; // Скорость роста сложности
    [SerializeField] private float initialSpawnDistance = 3f; // Начальное расстояние между препятствиями
    [SerializeField] private float minSpawnDistance = 1f; // Минимальное расстояние между препятствиями

    [Header("Префабы препятствий")]
    [SerializeField] private GameObject topIciclePrefab;  // Префаб верхней сосульки
    [SerializeField] private GameObject bottomIciclePrefab;  // Префаб нижней сосульки
    [SerializeField] private GameObject orcaPrefab;  // Префаб касатки
    [SerializeField] private GameObject birdPrefab;  // Префаб птицы

    [Header("Позиции для сосулек")]
    [SerializeField] private float topIcicleY = 5f;  // Позиция верхней сосульки
    [SerializeField] private float bottomIcicleY = -5f;  // Позиция нижней сосульки

    private float currentScrollSpeed;
    private float currentSpawnDistance;
    private Vector3 lastSpawnPosition;
    private Queue<GameObject> spawnedObjects = new Queue<GameObject>();
    private Transform player;

    private LevelManager levelManager;  // Ссылка на LevelManager

    private List<GameObject> allGeneratedObjects = new List<GameObject>();  // Список для хранения всех объектов

    private void Start()
    {
        player = Camera.main.transform;
        lastSpawnPosition = transform.position;

        // Получаем ссылку на LevelManager
        levelManager = FindObjectOfType<LevelManager>();

        // Начальные значения скорости и расстояния
        currentScrollSpeed = initialScrollSpeed;
        currentSpawnDistance = initialSpawnDistance;
    }

    private void Update()
    {
        if (levelManager != null)
        {
            if (levelManager.IsFirstLevelActive)
            {
                GenerateIcicles();  // Генерация сосулек на первом уровне
            }
            else
            {
                GenerateOrcasAndBirds();  // Генерация касаток и птиц на втором уровне
            }
        }
    }

    private void GenerateIcicles()
    {
        // Постепенное увеличение скорости и уменьшение расстояния для сосулек
        currentScrollSpeed = Mathf.Min(currentScrollSpeed + difficultyIncreaseRate * Time.deltaTime, maxScrollSpeed);
        currentSpawnDistance = Mathf.Max(currentSpawnDistance - difficultyIncreaseRate * Time.deltaTime, minSpawnDistance);

        // Двигаем сосульки на игрока
        foreach (var icicle in spawnedObjects)
        {
            icicle.transform.position += Vector3.left * currentScrollSpeed * Time.deltaTime;
        }

        // Спавним новую сосульку, если игрок достаточно близко
        if (player.position.x >= lastSpawnPosition.x - currentSpawnDistance)
        {
            SpawnAlternatingIcicle();
        }
    }

    private void SpawnAlternatingIcicle()
    {
        int currentIcicleType = Random.Range(0, 2); // Выбираем случайно тип сосульки

        GameObject iciclePrefab = currentIcicleType == 0 ? topIciclePrefab : bottomIciclePrefab;
        float yPosition = currentIcicleType == 0 ? topIcicleY : bottomIcicleY;

        // Появление сосульки
        Vector3 spawnPosition = lastSpawnPosition + Vector3.right * currentSpawnDistance;
        spawnPosition.y = yPosition;

        GameObject spawnedIcicle = Instantiate(iciclePrefab, spawnPosition, Quaternion.identity);
        spawnedObjects.Enqueue(spawnedIcicle);
        allGeneratedObjects.Add(spawnedIcicle);  // Добавляем сосульку в общий список
        lastSpawnPosition = spawnPosition;

        // Удаляем старые сосульки
        if (spawnedObjects.Count > 10)
        {
            Destroy(spawnedObjects.Dequeue());
        }
    }

    private void GenerateOrcasAndBirds()
    {
        // Генерация касаток
        if (Random.value < 0.5f)  // 50% шанс на касатку
        {
            GameObject orca = Instantiate(orcaPrefab, new Vector3(10f, Random.Range(-4f, -2f), 0), Quaternion.identity);
            allGeneratedObjects.Add(orca);  // Добавляем касатку в список
        }

        // Генерация птиц
        if (Random.value < 0.3f)  // 30% шанс на птицу
        {
            GameObject bird = Instantiate(birdPrefab, new Vector3(10f, Random.Range(3f, 5f), 0), Quaternion.identity);
            bird.GetComponent<Bird>().StartFlying();  // Начинаем движение птицы
            allGeneratedObjects.Add(bird);  // Добавляем птицу в список
        }
    }

    // Коллизии для касаток и птиц
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Проверяем, если столкновение с игроком
        {
            Destroy(gameObject);  // Уничтожаем объект (касатку или птицу)
        }
    }

    // Метод для очистки объектов после завершения первого уровня
    public void ClearAllGeneratedObjects()
    {
        foreach (var obj in allGeneratedObjects)
        {
            Destroy(obj);  // Уничтожаем все созданные объекты
        }

        allGeneratedObjects.Clear();  // Очищаем список объектов
    }
}
