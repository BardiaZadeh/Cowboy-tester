using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Transform platform; // Reference to the platform to move
    public float moveSpeed = 2f; // Speed at which the platform moves
    public float targetX = -24f; // Target position in the x-direction

    private bool isActivated = false;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = platform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated && other.CompareTag("Player")) // Adjust the tag to match the object you want to trigger the pressure plate
        {
            isActivated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isActivated && other.CompareTag("Player")) // Adjust the tag to match the object you want to trigger the pressure plate
        {
            isActivated = false;
        }
    }

    private void Update()
    {
        if (isActivated)
        {
            // Move platform towards the target position in the x-direction
            platform.position = new Vector3(
                Mathf.MoveTowards(platform.position.x, originalPosition.x + targetX, moveSpeed * Time.deltaTime),
                platform.position.y,
                platform.position.z
            );
        }
        else
        {
            // Move platform back to its original position
            platform.position = new Vector3(
                Mathf.MoveTowards(platform.position.x, originalPosition.x, moveSpeed * Time.deltaTime),
                platform.position.y,
                platform.position.z
            );
        }
    }
}
