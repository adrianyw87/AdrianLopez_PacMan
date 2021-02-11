using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public List<Image> Lifes = new List<Image>();

    int damagePoints = 0;

    void Start()
    {
        damagePoints = 0;
    }

    void Update()
    {
        
    }

    public void CheckDamage()
    {        
        if(damagePoints != Lifes.Count)
        {
            Lifes[damagePoints].GetComponent<Image>().enabled = false;
            damagePoints++;       
        }
    }
}
