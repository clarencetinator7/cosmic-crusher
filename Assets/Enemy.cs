using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Projectile") {
            Debug.Log("Enemy hit by projectile");
            Destroy(collision.gameObject);
        }
    }

}
