using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GosthMovement : MonoBehaviour
{
    public Transform[] waypoints;
    int totalWaypoints;
    int currentPoint = 0;

    public float speed = 0.3f;

    private void Awake()
    {
        totalWaypoints = waypoints.Length - 1;
        Debug.Log(totalWaypoints);
    }

    void FixedUpdate()
    {
        // Aproximarse al siguiente waypoint
        if (transform.position != waypoints[currentPoint].position)
        {
            Vector2 position = Vector2.MoveTowards(transform.position, waypoints[currentPoint].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(position);
        }else if (currentPoint < totalWaypoints)
        {
            currentPoint++;
        }else if(currentPoint == totalWaypoints)
        {
            currentPoint = 0;
        }
    }
}
