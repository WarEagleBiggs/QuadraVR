using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CubeRaycaster : MonoBehaviour
{
    public float rayLength = 100f; // Length of the raycast
    public string whoami;

    void Update()
    {
        //RayCastCall();
    }

    public void RayCastCall()
    {
        // Positive global axes
        SendRaycast(Vector3.right); // Positive X axis
        SendRaycast(Vector3.up); // Positive Y axis
        SendRaycast(Vector3.forward); // Positive Z axis

        // Negative global axes
        SendRaycast(-Vector3.right); // Negative X axis
        SendRaycast(-Vector3.up); // Negative Y axis
        SendRaycast(-Vector3.forward); // Negative Z axis]
        
        // Diagonals in all directions
        SendRaycast(Vector3.right + Vector3.up);
        SendRaycast(Vector3.right + Vector3.down);
        SendRaycast(Vector3.right + Vector3.forward);
        SendRaycast(Vector3.right + Vector3.back);

        SendRaycast(Vector3.left + Vector3.up);
        SendRaycast(Vector3.left + Vector3.down);
        SendRaycast(Vector3.left + Vector3.forward);
        SendRaycast(Vector3.left + Vector3.back);

        SendRaycast(Vector3.up + Vector3.forward);
        SendRaycast(Vector3.up + Vector3.back);
        SendRaycast(Vector3.down + Vector3.forward);
        SendRaycast(Vector3.down + Vector3.back);

        // Diagonals combining all three axes
        SendRaycast(Vector3.right + Vector3.up + Vector3.forward);
        SendRaycast(Vector3.right + Vector3.up + Vector3.back);
        SendRaycast(Vector3.right + Vector3.down + Vector3.forward);
        SendRaycast(Vector3.right + Vector3.down + Vector3.back);

        SendRaycast(Vector3.left + Vector3.up + Vector3.forward);
        SendRaycast(Vector3.left + Vector3.up + Vector3.back);
        SendRaycast(Vector3.left + Vector3.down + Vector3.forward);
        SendRaycast(Vector3.left + Vector3.down + Vector3.back);
    }

    void SendRaycast(Vector3 direction)
    {
        //RaycastHit hit;

        // Draw ray 
        Debug.DrawRay(transform.position, direction * rayLength, Color.red);

        Physics.SyncTransforms();

        if (whoami == "X")
        {
            // Perform the raycast
            //int ignoreBitMask = (1 << LayerMask.NameToLayer("Ignore Raycast"));
            
            RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, maxDistance: rayLength);

            if (hits.Length > 0)
            {
                int numHits = 0;
                foreach (var hit in hits)
                {
                    // Check if the hit object has the tag "X"
                    if (hit.collider.gameObject != gameObject && hit.collider.CompareTag("X"))
                    {
                        numHits++;
                        Debug.Log(this.gameObject.name + " Hit a X on global " + direction + " axis" +
                                  "    " + hit.collider.gameObject.name);
                    }
                }
                //Debug.Log("num hits: " + numHits + " dir: " + direction);
                if (numHits >= 3)
                {
                    Debug.Log("X Wins!");
                }
            }
        } else if (whoami == "O")
        {
            // Perform the raycast
            //int ignoreBitMask = (1 << LayerMask.NameToLayer("Ignore Raycast"));
            
            RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, maxDistance: rayLength);

            if (hits.Length > 0)
            {
                int numHits = 0;
                foreach (var hit in hits)
                {
                    // Check if the hit object has the tag "O"
                    if (hit.collider.gameObject != gameObject && hit.collider.CompareTag("O"))
                    {
                        numHits++;
                        Debug.Log(this.gameObject.name + " Hit a O on global " + direction + " axis" +
                                  "    " + hit.collider.gameObject.name);
                    }
                }
                //Debug.Log("num hits: " + numHits + " dir: " + direction);
                if (numHits >= 3)
                {
                    Debug.Log("O Wins!");
                }
            }
        }
        

    }
}
