using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjects : MonoBehaviour
{
    [SerializeField] private Generator[] generators;

    
    private void Start()
    {
        foreach (var generator in generators)
        {
            generator.Generate();
        }
    }
}
