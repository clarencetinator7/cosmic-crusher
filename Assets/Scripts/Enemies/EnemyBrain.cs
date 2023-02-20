using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{

    [Header("Component References")]
    [SerializeField] private GameObject player;
    [SerializeField] private Enemy enemy;

    [Header("Enemy Stats")]
    private float movementSpeed;
    private float shootingRange;
    private float shootingRate; 
    private float nextShootTime = 0f;

    void Start() {
        enemy = GetComponent<Enemy>();
        player = GameObject.Find("Player Ship");
        movementSpeed = enemy.getMovementSpeed();
        shootingRange = enemy.getShootRange();
        shootingRate = enemy.getFireRate();
    }

    void Update() {
       
        lookAtPlayer();

        // Distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);


        if(distanceToPlayer < shootingRange) {
            // Shoot with fire rate
            if(Time.time > nextShootTime) {
                nextShootTime = Time.time + shootingRate;
                enemy.Shoot();
            }
        } else {
            // Move towards player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        }

    }

    void OnDrawGizmos() {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    void lookAtPlayer() {
        // Direction to player
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        // Angle to player
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        // Rotate towards player
        Vector3 rotationData = new Vector3(0, 0, angleToPlayer - 90);
        // Slow rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotationData), 0.1f); 
    }
 
}
