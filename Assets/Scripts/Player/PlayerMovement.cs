using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement Settings")]
    private PlayerInputAction inputActions;
    private InputAction movement;
    private Vector2 newVelocity;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float maxVelocity = 5f;
    [SerializeField] private Vector2 moveDirection;


    // TODO: MOVE FIRE TO PLAYERFIRE SCRIPT

    [Header("Weapon / Firing Settings")]
    private InputAction fire;
    private GameObject weaponSlot;
    Weapon weaponScript;
    Coroutine lastRoutine;
    [SerializeField] private GameObject equippedWeapon;



    private void Awake() {
        inputActions = new PlayerInputAction();
        movement = inputActions.Player.Move;
        fire = inputActions.Player.Fire;
        rb = GetComponent<Rigidbody2D>();
        weaponSlot = GameObject.Find("Weapon Slot");
    }

    private void OnEnable() {
        movement.Enable();

        fire = inputActions.Player.Fire;
        fire.Enable();
        fire.started += OnAttack;
        fire.performed += OnAttack; 
        fire.canceled += OnAttack;
    }

    private void Update() {
        moveDirection = movement.ReadValue<Vector2>();
        newVelocity = moveDirection * movementSpeed;
    }

    private void FixedUpdate() {
        rb.velocity = newVelocity;
        // Limit the velocity of the player
        rb.velocity = Vector2.ClampMagnitude(rb.velocity + newVelocity, maxVelocity);
        // Slow down the player
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.1f);
    }

    private void OnAttack(InputAction.CallbackContext context) {
        equippedWeapon = weaponSlot.transform.GetChild(0).gameObject;        
        if(context.performed && equippedWeapon != null) {
            // Get the weapon slot child
            weaponScript = equippedWeapon.GetComponent<Weapon>();
            lastRoutine = StartCoroutine(weaponScript.shootCoroutine());
        } else if (context.canceled) {
            StopCoroutine(lastRoutine);
        } 
    }

}