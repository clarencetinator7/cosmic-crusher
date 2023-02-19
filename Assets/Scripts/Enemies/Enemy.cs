using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private EnemySO enemySO;

    [Header("Enemy Stats")]
    private float hitPoints;
    private float movementSpeed;
    private float damage;
    private float shootRange;
    [SerializeField] private Transform firePoint;
    private float fireRate;
    private GameObject projectile;

    [Header("Defaults")]
    [SerializeField] private float projectileSpeed = 15f;

    void Start() {
        hitPoints = enemySO.hitPoints;
        movementSpeed = enemySO.movementSpeed;
        damage = enemySO.damage;
        shootRange = enemySO.shootRange;
        fireRate = enemySO.fireRate;
        projectile = enemySO.projectile;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Projectile") {
            Debug.Log("Enemy hit by projectile");
            Destroy(collision.gameObject);
        }
    }

    public float getShootRange() {
        return shootRange;
    }

    public float getMovementSpeed() {
        return movementSpeed;
    }

    public float getFireRate() {
        return fireRate;
    }

    public void takeDamage(float damage) {
        hitPoints -= damage;
        if(hitPoints <= 0)
            Destroy(gameObject);
    }

    public void Shoot() {
        GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        Debug.Log(newProjectile);
        newProjectile.GetComponent<EnemyProjectile>().SetProjectileDamage(damage);
        Rigidbody2D projectileRb = newProjectile.GetComponent<Rigidbody2D>();
        // projectileRb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);
        projectileRb.velocity = firePoint.up * projectileSpeed;
    }


}
