using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject volna;
    [SerializeField] private float speedRight;
    [SerializeField] private float speedUp;
    private float speedDown;
    private bool isUp=false;
    private bool isDown=false;

    // Start is called before the first frame update
    void Start()
    {
        speedDown = speedUp * 0.75f;

    }

    // Update is called once per frame
    void Update()
    {
        RightMove();
        UpDownMove();
    }

    private void RightMove()
    {
        Vector3 newposition = volna.transform.position;
        newposition.x += speedRight * Time.deltaTime;
        volna.transform.position = newposition;
    }

    private void UpDownMove()
    {
        if(Input.GetMouseButton(0)) //ëêì
        {
            if(!isUp)
            {
                Vector3 newposition = volna.transform.position;
                newposition.y += speedUp * Time.deltaTime;
                volna.transform.position = newposition;
            }
        }
        else
            if (!isDown)
            {
                Vector3 newposition = volna.transform.position;
                newposition.y -= speedDown * Time.deltaTime;
                volna.transform.position = newposition;
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="BorderUp")
        {
            isUp= true;
        }
        if (collision.gameObject.tag == "BorderDown")
        {
            isDown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderUp")
        {
            isUp = false;
        }
        if (collision.gameObject.tag == "BorderDown")
        {
            isDown = false;
        }
    }
}
