using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectile;



    [Header("Weapon Stats")]
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileDamage = 10f;

    public void Shoot() {
        GameObject newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        newProjectile.GetComponent<Projectile>().SetProjectileDamage(projectileDamage);
        Rigidbody2D projectileRb = newProjectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(firePoint.up * projectileSpeed, ForceMode2D.Impulse);

    }

    public IEnumerator shootCoroutine(){
        while(true){
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

}
