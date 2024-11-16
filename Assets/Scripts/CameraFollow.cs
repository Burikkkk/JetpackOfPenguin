using System;
using UnityEngine;



public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private float startOffset;

    private void Start()
    {
        startOffset= transform.position.x-Player.position.x;
    }

    private void Update()
    {

        Vector3 newposition = transform.position;
        newposition.x = startOffset + Player.position.x;
        transform.position = newposition;
    }


   
}

