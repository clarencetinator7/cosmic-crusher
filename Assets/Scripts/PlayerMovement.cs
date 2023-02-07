using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputAction inputActions;
    private InputAction movement;
    private Rigidbody2D rb;

    [SerializeField]
    private float movementSpeed = 5f;
    [SerializeField]
    private float maxVelocity = 5f;
    private Vector2 moveDirection;
    private Vector2 newVelocity;
    
    private void Awake() {
        inputActions = new PlayerInputAction();
        movement = inputActions.Player.Move;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        movement.Enable();
        movement.performed += OnMovement;
    }

    private void Update() {
        // This code is for rotating the player towards the direction of movement
        // if (moveDirection != Vector2.zero) {
        //     float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        //     transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // }
        moveDirection = movement.ReadValue<Vector2>();
        newVelocity = moveDirection * movementSpeed;
    }

    private void FixedUpdate() {
        // rb.velocity = newVelocity;

        rb.velocity = Vector2.ClampMagnitude(rb.velocity + newVelocity, maxVelocity);
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.1f);
    }

    private void OnMovement(InputAction.CallbackContext context) {
        Debug.Log("Movement: " + context.ReadValue<Vector2>());
    }
}
