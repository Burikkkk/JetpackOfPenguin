using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject volna;             // ������ �����
    [SerializeField] private float speedRight;             // �������� �������� ������
    [SerializeField] private float speedUp;                // �������� �������
    private float speedDown;                               // �������� ������
    private bool isUp = false;                             // ����, ���� ��������� ������� ������
    private bool isDown = false;                           // ����, ���� ��������� ������ ������

    [SerializeField] private List<ParticleSystem> splashParticleSystems; // ������ ���� ������ ������ ��� �����

    void Start()
    {
        speedDown = speedUp * 0.75f; // ������������� �������� ������ ������� ������ �������� �������
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
        // ��������� �����, ���� ������ ����� ������ ����
        if (Input.GetMouseButton(0) && !isUp)
        {
            Vector3 newPosition = volna.transform.position;
            newPosition.y += speedUp * Time.deltaTime;
            volna.transform.position = newPosition;
        }
        // �������� �����, ���� ����� ������ ���� �� ������
        else if (!Input.GetMouseButton(0) && !isDown)
        {
            Vector3 newPosition = volna.transform.position;
            newPosition.y -= speedDown * Time.deltaTime;
            volna.transform.position = newPosition;
        }
    }

    private void HandleSplashEffect()
    {
        // �������� ������ ��� ������� (��� ������ ������� ������)
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
        // �������� ������ ��� ��������� (���� ����� ����������)
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

        // ������������� ������, ���� ����� �� ���������
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
        // ������������ ���������� ������� ������� ������
        if (collision.gameObject.tag == "BorderUp")
        {
            isUp = true;
        }
        // ������������ ���������� ������ ������� ������
        if (collision.gameObject.tag == "BorderDown")
        {
            isDown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ������� ���� ������� �������, ���� ����� �������� �
        if (collision.gameObject.tag == "BorderUp")
        {
            isUp = false;
        }
        // ������� ���� ������ �������, ���� ����� �������� �
        if (collision.gameObject.tag == "BorderDown")
        {
            isDown = false;
        }
    }
}
