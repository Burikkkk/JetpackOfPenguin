using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSource : MonoBehaviour
{
    public static MusicSource instance;
    public AudioSource audioSource;

    private void Start()
    {
        if (instance == null)
            instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
}
