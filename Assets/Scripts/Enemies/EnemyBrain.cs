using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{

    [Header("Component References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;

    [Header("Enemy Stats")]
    [SerializeField] private float shootingRange = 10f;
    private float shootingRate = 3f;
    private float nextShootTime = 0f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileDamage = 10f;
    [SerializeField] private float movementSpeed = 3f;

    void Update() {
       
        // Distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        // Direction to player
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        // Angle to player
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        // Rotate towards player
        transform.eulerAngles = new Vector3(0, 0, angleToPlayer - 90);

        if(distanceToPlayer < shootingRange) {
            // Shoot with fire rate
            if(Time.time > nextShootTime) {
                nextShootTime = Time.time + shootingRate;
                Shoot();
            }
        } else {
            // Move towards player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        }

    }

    private void Shoot()
    {
        Debug.Log("Shoot");
    }


    void OnDrawGizmos() {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    
}
