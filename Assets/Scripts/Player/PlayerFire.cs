using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{

    private PlayerInputAction inputActions;
    private InputAction Fire;

    [SerializeField] private Rigidbody2D rb;

    private float holdStartTime;
    private float holdTime;
    [SerializeField]
    private float maxHoldTime = 5f;
    private bool isHolding = false;
    [SerializeField] private GameObject projectile;
    private GameObject newProjectile;
    [SerializeField] private Transform firePoint;

    void Awake() {
        inputActions = new PlayerInputAction();
        Fire = inputActions.Player.Fire;
    }

    void OnEnable() {
        Fire.Enable();
        Fire.started += OnAttack;
        Fire.performed += OnAttack;
        Fire.canceled += OnAttack;
    }

    void OnDisable() {
        Fire.Disable();
    }

    void Start() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        // TODO: SLOW PLAYER WHILE HOLDING ATTACK
    }

    private void launchProjectile() {
        // Launch projectile
        Rigidbody2D projectileRb = newProjectile.GetComponent<Rigidbody2D>();

        newProjectile.transform.parent = null;
        Destroy(newProjectile, 5f);
        projectileRb.bodyType = RigidbodyType2D.Dynamic;
        projectileRb.AddForce(firePoint.up * holdTime * 15f, ForceMode2D.Impulse);
    }

    private void OnAttack(InputAction.CallbackContext context) {
        if (context.started) {
            holdStartTime = Time.time;
            isHolding = true;
            Debug.Log("Attack Hold");

            // add indicator of holding attack by instantiating a projectile
            newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
            newProjectile.transform.parent = firePoint;

        }
        else if (context.canceled && isHolding) {
            holdTime = Time.time - holdStartTime;
            holdTime = Mathf.Min(holdTime, maxHoldTime);
            isHolding = false;
            Debug.Log("Attack Release");
            Debug.Log("Hold Time: " + holdTime);

            launchProjectile();
            
            // Add knockback to player
            rb.AddForce(-transform.up * holdTime * 5f, ForceMode2D.Impulse);


        }
    }

   
}
