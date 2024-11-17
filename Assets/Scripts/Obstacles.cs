using System.Collections.Generic;
using UnityEngine;

public class IcicleGenerator : MonoBehaviour
{
    [Header("����� ���������")]
    [SerializeField] private float initialScrollSpeed = 1.0f; // ��������� �������� �������� �����������
    [SerializeField] private float maxScrollSpeed = 3.0f; // ������������ ��������
    [SerializeField] private float difficultyIncreaseRate = 0.1f; // �������� ����� ���������
    [SerializeField] private float initialSpawnDistance = 3f; // ��������� ���������� ����� �������������
    [SerializeField] private float minSpawnDistance = 1f; // ����������� ���������� ����� �������������

    [Header("������� �����������")]
    [SerializeField] private GameObject topIciclePrefab;  // ������ ������� ��������
    [SerializeField] private GameObject bottomIciclePrefab;  // ������ ������ ��������
    [SerializeField] private GameObject orcaPrefab;  // ������ �������
    [SerializeField] private GameObject birdPrefab;  // ������ �����

    [Header("������� ��� �������")]
    [SerializeField] private float topIcicleY = 5f;  // ������� ������� ��������
    [SerializeField] private float bottomIcicleY = -5f;  // ������� ������ ��������

    private float currentScrollSpeed;
    private float currentSpawnDistance;
    private Vector3 lastSpawnPosition;
    private Queue<GameObject> spawnedObjects = new Queue<GameObject>();
    private Transform player;

    private LevelManager levelManager;  // ������ �� LevelManager

    private List<GameObject> allGeneratedObjects = new List<GameObject>();  // ������ ��� �������� ���� ��������

    private void Start()
    {
        player = Camera.main.transform;
        lastSpawnPosition = transform.position;

        // �������� ������ �� LevelManager
        levelManager = FindObjectOfType<LevelManager>();

        // ��������� �������� �������� � ����������
        currentScrollSpeed = initialScrollSpeed;
        currentSpawnDistance = initialSpawnDistance;
    }

    private void Update()
    {
        if (levelManager != null)
        {
            if (levelManager.IsFirstLevelActive)
            {
                GenerateIcicles();  // ��������� ������� �� ������ ������
            }
            else
            {
                GenerateOrcasAndBirds();  // ��������� ������� � ���� �� ������ ������
            }
        }
    }

    private void GenerateIcicles()
    {
        // ����������� ���������� �������� � ���������� ���������� ��� �������
        currentScrollSpeed = Mathf.Min(currentScrollSpeed + difficultyIncreaseRate * Time.deltaTime, maxScrollSpeed);
        currentSpawnDistance = Mathf.Max(currentSpawnDistance - difficultyIncreaseRate * Time.deltaTime, minSpawnDistance);

        // ������� �������� �� ������
        foreach (var icicle in spawnedObjects)
        {
            icicle.transform.position += Vector3.left * currentScrollSpeed * Time.deltaTime;
        }

        // ������� ����� ��������, ���� ����� ���������� ������
        if (player.position.x >= lastSpawnPosition.x - currentSpawnDistance)
        {
            SpawnAlternatingIcicle();
        }
    }

    private void SpawnAlternatingIcicle()
    {
        int currentIcicleType = Random.Range(0, 2); // �������� �������� ��� ��������

        GameObject iciclePrefab = currentIcicleType == 0 ? topIciclePrefab : bottomIciclePrefab;
        float yPosition = currentIcicleType == 0 ? topIcicleY : bottomIcicleY;

        // ��������� ��������
        Vector3 spawnPosition = lastSpawnPosition + Vector3.right * currentSpawnDistance;
        spawnPosition.y = yPosition;

        GameObject spawnedIcicle = Instantiate(iciclePrefab, spawnPosition, Quaternion.identity);
        spawnedObjects.Enqueue(spawnedIcicle);
        allGeneratedObjects.Add(spawnedIcicle);  // ��������� �������� � ����� ������
        lastSpawnPosition = spawnPosition;

        // ������� ������ ��������
        if (spawnedObjects.Count > 10)
        {
            Destroy(spawnedObjects.Dequeue());
        }
    }

    private void GenerateOrcasAndBirds()
    {
        // ��������� �������
        if (Random.value < 0.5f)  // 50% ���� �� �������
        {
            GameObject orca = Instantiate(orcaPrefab, new Vector3(10f, Random.Range(-4f, -2f), 0), Quaternion.identity);
            allGeneratedObjects.Add(orca);  // ��������� ������� � ������
        }

        // ��������� ����
        if (Random.value < 0.3f)  // 30% ���� �� �����
        {
            GameObject bird = Instantiate(birdPrefab, new Vector3(10f, Random.Range(3f, 5f), 0), Quaternion.identity);
            bird.GetComponent<Bird>().StartFlying();  // �������� �������� �����
            allGeneratedObjects.Add(bird);  // ��������� ����� � ������
        }
    }

    // �������� ��� ������� � ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // ���������, ���� ������������ � �������
        {
            Destroy(gameObject);  // ���������� ������ (������� ��� �����)
        }
    }

    // ����� ��� ������� �������� ����� ���������� ������� ������
    public void ClearAllGeneratedObjects()
    {
        foreach (var obj in allGeneratedObjects)
        {
            Destroy(obj);  // ���������� ��� ��������� �������
        }

        allGeneratedObjects.Clear();  // ������� ������ ��������
    }
}
