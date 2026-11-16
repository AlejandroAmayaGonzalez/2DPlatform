using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] private float moveSpeed = 2f;

    [Header("Raycast Settings")]
    [SerializeField] private float wallCheckDistance = 0.5f; // Distance to check for walls
    [SerializeField] private float enemyCheckDistance = 0.7f; // Distance to check for enemies
    [SerializeField] private float groundCheckDistance = 1f; // Distance to check for ground
    [SerializeField] private float groundCheckOffset = 0.1f; // Offset for ground check

    [Header("Facing")]
    [SerializeField] private facing startDirection = facing.Right;
    enum facing { Left, Right }
    private int direction = 1; // 1 for right, -1 for left

    Rigidbody2D rb;

    void Start(){ 
        rb = GetComponent<Rigidbody2D>();

        if (startDirection == facing.Left){ TurnAround(); }
    }

    void FixedUpdate(){
        if (IsHittingWall() || IsAtEdge() || IsHittingEnemy()){
            TurnAround();
        }

        rb.linearVelocity = new Vector2(moveSpeed * direction, 0f);
    }

    /// <summary>
    /// Flip the sprite according to the defined direction
    /// </summary>
    private void TurnAround(){
        direction = -direction;
        transform.localScale = new Vector2(direction, 1f);
    }

    /// <summary>
    /// Check if the enemy is at the edge of a platform
    /// </summary>
    /// <returns>true if at the edge, false otherwise</returns>
    private bool IsAtEdge(){
        Vector2 origin = rb.position + new Vector2(groundCheckOffset * direction, 0f);

        Debug.DrawRay(origin, Vector2.down * groundCheckDistance, Color.blue);

        // Check for ground using raycast
        return !Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, LayerMask.GetMask("Ground"));
    }

    /// <summary>
    /// Check if the enemy is hitting a wall in the direction it's moving
    /// </summary>
    /// <returns>true if hitting a wall, false otherwise</returns>
    private bool IsHittingWall(){
        Vector2 origin = rb.position;
        Vector2 rayDirection = Vector2.right * direction;

        Debug.DrawRay(origin, rayDirection * wallCheckDistance, Color.red);

        // Check for walls using raycast
        return Physics2D.Raycast(origin, rayDirection, wallCheckDistance, LayerMask.GetMask("Ground"));
    }

    /// <summary>
    /// Check if the enemy is hitting another enemy
    /// </summary>
    /// <returns>true if hitting an enemy, false otherwise</returns>
    private bool IsHittingEnemy(){
        Vector2 origin = rb.position;
        Vector2 rayDirection = Vector2.right * direction;

        Debug.DrawRay(origin, rayDirection * enemyCheckDistance, Color.green);

        // Save the detected collision
        RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, enemyCheckDistance, LayerMask.GetMask("Enemies"));

        return hit.collider != null && hit.collider.gameObject != gameObject; // Ensure it's not hitting itself
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){ 
            other.GetComponent<PlayerHealth>().TakeDamage(); 
        }
    }
}
