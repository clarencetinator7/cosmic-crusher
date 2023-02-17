using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float hitPoints = 100f;


    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Projectile") {
            Debug.Log("Enemy hit by projectile");
            Destroy(collision.gameObject);
        }
    }

    public void takeDamage(float damage) {
        hitPoints -= damage;
        if(hitPoints <= 0)
            Destroy(gameObject);
    }


}
