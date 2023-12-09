using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRaycaster : MonoBehaviour
{
    public float rayLength = 10f; // Length of the raycast
    public string whoami;

    void Update()
    {
        RayCastCall();
    }

    public void RayCastCall()
    {
        // Positive global axes
        SendRaycast(Vector3.right);    // Positive X axis
        SendRaycast(Vector3.up);       // Positive Y axis
        SendRaycast(Vector3.forward);  // Positive Z axis

        // Negative global axes
        SendRaycast(-Vector3.right);   // Negative X axis
        SendRaycast(-Vector3.up);      // Negative Y axis
        SendRaycast(-Vector3.forward); // Negative Z axis
    }

    void SendRaycast(Vector3 direction)
    {
        RaycastHit hit;

        // Draw ray 
        Debug.DrawRay(transform.position, direction * rayLength, Color.red);

        if (whoami == "X")
        {
            // Perform the raycast
            if (Physics.Raycast(transform.position, direction, out hit, rayLength))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // Hit self, ignore this hit
                    return;
                }
                // Check if the hit object has the tag "X"
                if (hit.collider.CompareTag("X"))
                {
                    Debug.Log(this.gameObject.name + " Hit a X on global " + direction + " axis" + 
                              "    " + hit.collider.gameObject.name);
                }
            
            }
        } else if (whoami == "O")
        {
            // Perform the raycast
            if (Physics.Raycast(transform.position, direction, out hit, rayLength))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // Hit self, ignore this hit
                    return;
                }
                // Check if the hit object has the tag "O"
                if (hit.collider.CompareTag("O"))
                {
                    Debug.Log(this.gameObject.name + " Hit a O on global " + direction + " axis" + 
                              "    " + hit.collider.gameObject.name);
                }
            
            }
        }
        
    }
}
