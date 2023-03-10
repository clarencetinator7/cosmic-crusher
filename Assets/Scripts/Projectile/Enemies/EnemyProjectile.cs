using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
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

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            // other.gameObject.GetComponent<Player>().takeDamage(projectileDamage);
            Debug.Log("Hit Player");
            Destroy(gameObject);
        } else if(other.gameObject.tag == "Wall") {
            Destroy(gameObject);
        }
    }

}
