using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGraphics : MonoBehaviour
{
    private List<SpriteRenderer> graphicObjects;
    [SerializeField] private bool enabledOnStart = false;
    [SerializeField] private float timeToFade = 2.0f;
    [SerializeField] private float scrollSpeed = 1.0f;  // Скорость движения фона и препятствий

    private Color startColor = Color.white;
    private Vector3 initialPosition;  // Начальная позиция фона

    private void Start()
    {
        graphicObjects = new List<SpriteRenderer>();
        AddAllGraphicObjects(transform);
        SetAllRenderers(enabledOnStart);
        initialPosition = transform.position;  // Запоминаем начальную позицию
    }

    private void Update()
    {
        // Движение фона и всех его объектов
        //transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
    }

    private void AddAllGraphicObjects(Transform parent)
    {
        foreach (Transform child in parent)
        {
            var childObject = child.gameObject;
            if (childObject.GetComponent<SpriteRenderer>() != null)
            {
                graphicObjects.Add(childObject.GetComponent<SpriteRenderer>());
            }
            AddAllGraphicObjects(child);
        }
    }

    public void SetAllRenderers(bool value)
    {
        foreach (var spriteRenderer in graphicObjects)
        {
            spriteRenderer.enabled = value;
        }
    }

    public void StartFading(bool direction)
    {
        StartCoroutine(FadeGraphics(direction));
    }

    private IEnumerator FadeGraphics(bool direction)
    {
        if (direction)
        {
            SetAllRenderers(true);
        }
        float elapsedTime = 0f;

        while (elapsedTime < timeToFade)
        {
            float percent = elapsedTime / timeToFade;

            foreach (var spriteRenderer in graphicObjects)
            {
                startColor.a = direction ? percent : 1 - percent;
                spriteRenderer.color = startColor;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (!direction)
        {
            SetAllRenderers(false);
            transform.position = initialPosition; // Сброс позиции фона после скрытия
        }
    }
}