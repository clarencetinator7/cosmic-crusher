using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputAction inputActions;
    private InputAction movement;
    private InputAction fire;
    private Rigidbody2D rb;

    [SerializeField]
    private float movementSpeed = 1f;
    [SerializeField]
    private float maxVelocity = 5f;
    [SerializeField]
    private float acceleration = 0.1f;
    private Vector2 moveDirection;
    private Vector2 newVelocity;
        
    private bool isFiring = false;

    private void Awake() {
        inputActions = new PlayerInputAction();
        movement = inputActions.Player.Move;
        fire = inputActions.Player.Fire;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        movement.Enable();

        fire = inputActions.Player.Fire;
        fire.Enable();
        fire.performed += OnAttack; 
        fire.canceled += OnAttack;
    }

    private void Update() {
        moveDirection = movement.ReadValue<Vector2>();
        // newVelocity = moveDirection * movementSpeed;
        if(!isFiring) {
            newVelocity += moveDirection * movementSpeed;
            maxVelocity = 7f;
        } else {
            // Slow down the player while charging the attack
            newVelocity = moveDirection * 0.5f;
            maxVelocity = 0.5f;
        }
    }

    private void FixedUpdate() {
        rb.velocity = newVelocity;
        // Limit the velocity of the player
        rb.velocity = Vector2.ClampMagnitude(rb.velocity + newVelocity, maxVelocity);
        // Slow down the player
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.1f);
    }

    private void OnAttack(InputAction.CallbackContext context) {

        if(context.performed) {
            isFiring = true;
        } else if (context.canceled) {
            isFiring = false;
        }

    }

}
