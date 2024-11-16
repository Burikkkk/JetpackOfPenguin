using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGraphics : MonoBehaviour
{
    private List<SpriteRenderer> graphicObjects;
    [SerializeField] private bool enabledOnStart = false;
    [SerializeField] private float timeToFade = 2.0f;

    private Color startColor = Color.white;

    private void Start()
    {
        graphicObjects = new List<SpriteRenderer>();
        AddAllGraphicObjects(transform);
        SetAllRenderers(enabledOnStart);
        
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

    // true - появляется, false - исчезает
    public void StartFading(bool direction)
    {
        StartCoroutine(FadeGraphics(direction));
    }
    
    private IEnumerator FadeGraphics(bool direction)
    {
        if(direction)
        {
            SetAllRenderers(true);
        }
        float elapsedTime = 0f;

        while (elapsedTime < timeToFade)
        {
            float percent;

            percent = elapsedTime / timeToFade;

            foreach (var spriteRenderer in graphicObjects)
            {
                if (direction)
                {
                    startColor.a = percent;
                }
                else
                {
                    startColor.a = 1 - percent;
                }
                spriteRenderer.color = startColor;
            }
            
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        if (!direction)
        {
            SetAllRenderers(false);
        }
    }
}
