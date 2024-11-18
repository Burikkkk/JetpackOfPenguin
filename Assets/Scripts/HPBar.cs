using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    public Sprite[] healthPrefabs; // ������ �������� ����������� ��� HP
    
    public void UpdateHPBar(int HP)
    {
        if(HP < 0 || HP >= healthPrefabs.Length)
            return;
        
        gameObject.GetComponent<Image>().sprite= healthPrefabs[HP];
    }
}
