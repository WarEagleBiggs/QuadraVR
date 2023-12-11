using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    public Transform playerTransform; // Assign this to the player's transform in the inspector

    void Update()
    {
        if (playerTransform != null)
        {
            // Get the current rotation of the UI panel
            Vector3 currentRotation = transform.eulerAngles;

            // Get the Y axis rotation of the player
            float playerYRotation = playerTransform.eulerAngles.y;

            // Set the UI panel's rotation to match the player's Y rotation, but keep its original X and Z rotation
            transform.eulerAngles = new Vector3(currentRotation.x, playerYRotation + 180, currentRotation.z);
        }
    }
}
