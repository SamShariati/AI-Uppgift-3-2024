using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Get input from horizontal and vertical axes
        float horizontalInput = Input.GetAxis("Horizontal"); // Left and right (A/D or Arrow Keys)
        float verticalInput = Input.GetAxis("Vertical");     // Up and down (W/S or Arrow Keys)

        // Calculate movement vector (x for horizontal, z for vertical)
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        // Normalize movement to ensure consistent speed in all directions
        if (movement.magnitude > 1f)
        {
            movement = movement.normalized;
        }

        // Apply movement to the object
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}
