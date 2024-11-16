using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    public Sprite[] healthPrefabs; // Массив префабов изображений для HP
   


    public void UpdateHPBar(int HP)
    {
       gameObject.GetComponent<Image>().sprite= healthPrefabs[HP];
        Debug.Log("Смешное хп " + HP);
    }
}
