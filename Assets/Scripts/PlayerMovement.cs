using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject volna;             // Объект волны
    [SerializeField] private float speedRight;             // Скорость движения вправо
    [SerializeField] private float speedUp;                // Скорость подъёма
    private float speedDown;                               // Скорость спуска
    private bool isUp = false;                             // Флаг, если достигнут верхний предел
    private bool isDown = false;                           // Флаг, если достигнут нижний предел

    [SerializeField] private List<ParticleSystem> splashParticleSystems; // Список всех систем частиц для брызг

    void Start()
    {
        speedDown = speedUp * 0.75f; // Устанавливаем скорость спуска немного меньше скорости подъёма
    }

    void Update()
    {
        RightMove();
        UpDownMove();
        HandleSplashEffect();
    }

    private void RightMove()
    {
        Vector3 newPosition = volna.transform.position;
        newPosition.x += speedRight * Time.deltaTime;
        volna.transform.position = newPosition;
    }

    private void UpDownMove()
    {
        // Поднимаем волну, если нажата левая кнопка мыши
        if (Input.GetMouseButton(0) && !isUp)
        {
            Vector3 newPosition = volna.transform.position;
            newPosition.y += speedUp * Time.deltaTime;
            volna.transform.position = newPosition;
        }
        // Опускаем волну, если левая кнопка мыши не нажата
        else if (!Input.GetMouseButton(0) && !isDown)
        {
            Vector3 newPosition = volna.transform.position;
            newPosition.y -= speedDown * Time.deltaTime;
            volna.transform.position = newPosition;
        }
    }

    private void HandleSplashEffect()
    {
        // Включаем брызги при подъеме (для каждой системы частиц)
        if (Input.GetMouseButton(0))
        {
            foreach (var splash in splashParticleSystems)
            {
                if (!splash.isPlaying)
                {
                    splash.Play();
                }
            }
        }
        // Включаем брызги при сплошении (если волна опускается)
        else if (!Input.GetMouseButton(0) && !isDown)
        {
            foreach (var splash in splashParticleSystems)
            {
                if (!splash.isPlaying)
                {
                    splash.Play();
                }
            }
        }

        // Останавливаем брызги, если волна не двигается
        if (!Input.GetMouseButton(0) && isDown)
        {
            foreach (var splash in splashParticleSystems)
            {
                if (splash.isPlaying)
                {
                    splash.Stop();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Обрабатываем достижение верхней границы волной
        if (collision.gameObject.tag == "BorderUp")
        {
            isUp = true;
        }
        // Обрабатываем достижение нижней границы волной
        if (collision.gameObject.tag == "BorderDown")
        {
            isDown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Снимаем флаг верхней границы, если волна покидает её
        if (collision.gameObject.tag == "BorderUp")
        {
            isUp = false;
        }
        // Снимаем флаг нижней границы, если волна покидает её
        if (collision.gameObject.tag == "BorderDown")
        {
            isDown = false;
        }
    }
}
