using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    FoodGenerator foodGenerator;

    private void Start()
    {
        foodGenerator = GameObject.Find("FoodGenerator").GetComponent<FoodGenerator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {        
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            foodGenerator.pickedFood++;            
        }

        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
            foodGenerator.totalFood--;
        }
    }
}
