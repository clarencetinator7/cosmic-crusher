using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAiming : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

        // Get the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // Get the direction from the player to the mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Get the angle from the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the player
        // -90 is to offset the rotation of the sprite (which is facing up)
        transform.eulerAngles = new Vector3(0, 0, angle - 90);

    }
}
