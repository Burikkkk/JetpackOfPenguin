using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private int[] amounts;
    [SerializeField] private float randomY;
    [SerializeField] private float startX;
    [SerializeField] private float randomMinX;
    [SerializeField] private float randomMaxX;
    public bool generateOnStart = true;

    // Start is called before the first frame update
    void Start()
    {
        if(generateOnStart)
            Generate();
    }
    

    public void Generate()
    {
        for(int i=0; i<objects.Length; i++)
        {
            float lastPosition = startX;
            for (int j = 0; j < amounts[i]; j++)
            {
                Vector3 position = objects[i].transform.position;
                position.x = lastPosition + Random.Range(randomMinX, randomMaxX);
                position.y += randomY * Random.Range(-1f, 1f);
                var newObject = Instantiate(objects[i], position, Quaternion.identity, transform);
                lastPosition = position.x;
                var parallax = newObject.GetComponent<Parallax>();
                if (parallax != null)
                {
                    parallax.cam = Camera.main.gameObject;
                    parallax.repeats = false;
                }
            }
        }
    }
}
