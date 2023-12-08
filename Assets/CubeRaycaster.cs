using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRaycaster : MonoBehaviour
{
    public float rayLength = 10f; // Length of the raycast
    public string whoami;

    void Update()
    {
        
    }

    public void RayCastCall()
    {
        SendRaycast(Vector3.right);  // Global X axis
        SendRaycast(Vector3.up);      // Global Y axis
        SendRaycast(Vector3.forward); // Global Z axis
    }

    void SendRaycast(Vector3 direction)
    {
        RaycastHit hit;

        // Draw ray in the Scene view for debugging
        Debug.DrawRay(transform.position, direction * rayLength, Color.red);

        if (whoami == "X")
        {
            // Perform the raycast
            if (Physics.Raycast(transform.position, direction, out hit, rayLength))
            {
                // Check if the hit object has the tag "Cube"
                if (hit.collider.CompareTag("X"))
                {
                    Debug.Log("Hit a X on global " + direction + " axis" + 
                              "    " + hit.collider.gameObject.name);
                }
            
            }
        } else if (whoami == "O")
        {
            // Perform the raycast
            if (Physics.Raycast(transform.position, direction, out hit, rayLength))
            {
                // Check if the hit object has the tag "Cube"
                if (hit.collider.CompareTag("O"))
                {
                    Debug.Log("Hit a O on global " + direction + " axis" + 
                              "    " + hit.collider.gameObject.name);
                }
            
            }
        }
        
    }
}
