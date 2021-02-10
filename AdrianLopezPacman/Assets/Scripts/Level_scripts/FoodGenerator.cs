using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public int rows = 15;
    public int columns = 15;

    // Coordenadas iniciales para el el generador de comida (centro del cuadrado más arriba y a la izquierda del level: comprobar en la escena)
    public float initialX;
    public float initialY;

    public GameObject Generator;
    public GameObject Food;

    public int totalFood;
    public int pickedFood;

    void Awake()
    {        
        Vector2 initialPosition = new Vector2(initialX, initialY);
        Vector2 currentPosition = initialPosition;
        Generator.transform.position = currentPosition;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject.Instantiate(Food, Generator.transform.position, Generator.transform.rotation);
                currentPosition = Generator.transform.position;
                Generator.transform.position = currentPosition + Vector2.right;
                totalFood++;
            }
            currentPosition = new Vector2(initialX, Generator.transform.position.y);
            Generator.transform.position = currentPosition + Vector2.down;
            //TODO: eliminar aquellos objetos que se instancien dentro de obstáculos (ahora en Food.cs)
        }
    }

    private void Update()
    {
        if(pickedFood == totalFood)
        {
            Debug.Log("has ganado");
        }
    }
}
