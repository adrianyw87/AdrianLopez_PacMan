using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GosthMovement : MonoBehaviour
{
    public static event Action playerTouched = delegate { };

    PlayerController playerController;

    public Transform[] waypoints;
    int totalWaypoints;
    int currentPoint = 0;

    public float speed = 0.3f;

    enum EnemyState
    {
        moving,
        stop
    }

    EnemyState enemyState;

    private void Awake()
    {
        GameFlowManager.startGame += StartMove;
        GameFlowManager.stopGame += StopMove;
        
        totalWaypoints = waypoints.Length - 1;
        enemyState = EnemyState.stop;

        playerController = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate()
    {
        if(enemyState == EnemyState.moving)
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

    void StartMove()
    {
        enemyState = EnemyState.moving;
    }

    void StopMove()
    {
        enemyState = EnemyState.stop;
        GameFlowManager.startGame -= StartMove;
        GameFlowManager.stopGame -= StopMove;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController.PlayerTouched();
        }
    }

}
