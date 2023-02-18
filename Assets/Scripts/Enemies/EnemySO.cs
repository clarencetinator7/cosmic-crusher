using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject 
{
    [Header("Enemy Stats")]
    public float hitPoints;
    public float movementSpeed;

    [Header("Enemy Attack")]
    public float damage;
    public GameObject projectile;
    public float shootRange;
    public float fireRate;
}
