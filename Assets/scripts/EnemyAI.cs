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
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }
    void ChasePlayer()
    {
        // Chase the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        FaceTarget(player.position);
    }
    void Patrol()
    {
        if (patrolPoints != null && patrolPoints.Length > 0 && currentPoint >= 0 && currentPoint < patrolPoints.Length && patrolPoints[currentPoint] != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, patrolPoints[currentPoint].position) < .675f)
            {
                Debug.Log("Reached patrol point: " + currentPoint);
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
                Debug.Log("Moving to next patrol point: " + currentPoint + " (Reset to first point if last point reached)");
            }
        }
        else
        {
            Debug.LogError("Patrol points are not properly assigned or current point is invalid.");
        }
        FaceTarget(patrolPoints[currentPoint].position);
    }
    void FaceTarget(Vector2 targetPosition)
    {
        if (targetPosition.x > transform.position.x)
        {
            // Target is to the right, face right
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (targetPosition.x < transform.position.x)
        {
            // Target is to the left, face left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        // No change if target is directly above or below
    }
}