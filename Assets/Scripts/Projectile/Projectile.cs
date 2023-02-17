using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projectileDamage;

    public void SetProjectileDamage(float damage) {
        projectileDamage = damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<Enemy>().takeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }

}
