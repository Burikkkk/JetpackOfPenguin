using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public GameObject cam;
    public float parallexEffect;
    public bool repeats = true;
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
        if(cam == null)
            return;
        
        float temp = (cam.transform.position.x * (1 - parallexEffect));
        float dist = (cam.transform.position.x * parallexEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
        
        if (!repeats)
            return;
        
        if (temp > startpos + length)
        {
            startpos += length * 2;
        }
        else if (temp < startpos - length)
        {
            startpos -= length * 2;
        }
    }
}