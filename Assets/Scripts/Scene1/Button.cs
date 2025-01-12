using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public float targetY = 3f; // Target y position
    public float moveSpeed = 2f; // Speed of the movement

    private bool isMoving = false;
    private Vector3 targetPosition;

    void Start()
    {
        // Optional: Initialization if needed
    }

    void Update()
    {
        if (isMoving)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Stop moving when the target position is reached
            if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
            {
                transform.position = targetPosition; // Ensure exact final position
                isMoving = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object interacting with the button is the Player
        if (other.CompareTag("Player") && !isMoving)
        {
            targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
            isMoving = true;
        }
    }
}
