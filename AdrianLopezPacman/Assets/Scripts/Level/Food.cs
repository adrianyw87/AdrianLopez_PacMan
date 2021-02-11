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
            if(foodGenerator.pickedFood == foodGenerator.totalFood)
            {
                foodGenerator.AllFoodCollected();                
            }
        }

        // Restamos puntos de comida a recoger en aquellos puntos que choquen con obstáculos y eliminamos eso puntos de comida
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
            foodGenerator.totalFood--;
        }
    }
}
