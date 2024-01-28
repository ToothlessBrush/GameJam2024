using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;
    public float moveSpeed = 2f;
    public float chaseRange = 5f;

    private int currentPoint = 0;

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseRange)
        {
            // Chase the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Patrol between points
            Patrol();
        }
    }

    void Patrol()
    {
        // Move towards the current patrol point
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);

        // Check if the enemy has reached the current point
        if (Vector2.Distance(transform.position, patrolPoints[currentPoint].position) < 0.1f)
        {
            // Move to the next patrol point
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }
}