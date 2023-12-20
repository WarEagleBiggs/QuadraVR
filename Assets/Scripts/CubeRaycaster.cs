using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class CubeRaycaster : MonoBehaviour
{
    public float rayLength = 100f; // Length of the raycast
    public string whoami;

    public AudioSource Fanfare;

    public LineConnector LineC;

    public GameMaster GM;

    public GameObject BlueFireworks;
    public GameObject OrangeFireworks;


    public void RayCastCall()
    {
        // Positive global axes
        SendRaycast(Vector3.right); // Positive X axis
        SendRaycast(Vector3.up); // Positive Y axis
        SendRaycast(Vector3.forward); // Positive Z axis
        
        // Diagonals in all directions
        SendRaycast(Vector3.right + Vector3.up);
        SendRaycast(Vector3.right + Vector3.down);
        SendRaycast(Vector3.right + Vector3.forward);
        SendRaycast(Vector3.right + Vector3.back);
        
        SendRaycast(Vector3.up + Vector3.forward);
        SendRaycast(Vector3.up + Vector3.back);
 
        // Diagonals combining all three axes
        SendRaycast(Vector3.right + Vector3.up + Vector3.forward);
        SendRaycast(Vector3.right + Vector3.up + Vector3.back);
        SendRaycast(Vector3.right + Vector3.down + Vector3.forward);
        SendRaycast(Vector3.right + Vector3.down + Vector3.back);
    }

    void SendRaycast(Vector3 direction)
    {
        // Draw ray 
        Debug.DrawRay(transform.position, direction * rayLength, Color.red);

        Physics.SyncTransforms();
        
        RaycastHit[] hits1 = Physics.RaycastAll(transform.position, direction, maxDistance: rayLength);
        RaycastHit[] hits2 = Physics.RaycastAll(transform.position, -direction, maxDistance: rayLength);
        List<RaycastHit> hits = hits1.Concat(hits2).ToList();


        if (whoami == "X")
        {
            if (hits1.Length > 0 || hits2.Length > 0)
            {
                int numHits = 0;
                foreach (var hit in hits)
                {
                    // Check if the hit object has the tag "X"
                    if (hit.collider.gameObject != gameObject && hit.collider.CompareTag("X"))
                    {
                        numHits++;
                    }
                }
                
                if (numHits >= 3)
                {
                    Debug.Log("X Wins!");
                    Fanfare.Play();
                    BlueFireworks.SetActive(true);
                    
                    GM.EndUI.SetActive(true);

                    GM.isGamePlaying = false;
                    GM.background_O.SetActive(false);
                    GM.background_X.SetActive(true);
                    GM.Text_Top.SetText("X Wins!");

                    Debug.Log("Line Renderer: adding verts " + (hits.Count + 1));
                    List<Vector3> verts = new List<Vector3>();
                    for (int i = 0; i < hits.Count; i++)
                    {
                        verts.Add(hits[i].transform.localPosition);
                    }
                    verts.Add(transform.localPosition);
                    
                    LineC.SetVerts(verts.ToArray());
                }
            }
        } else if (whoami == "O")
        {
            if (hits1.Length > 0 || hits2.Length > 0)
            {
                int numHits = 0;
                foreach (var hit in hits)
                {
                    // Check if the hit object has the tag "O"
                    if (hit.collider.gameObject != gameObject && hit.collider.CompareTag("O"))
                    {
                        numHits++;
                    }
                }
                
                if (numHits >= 3)
                {
                    Debug.Log("O Wins!");
                    Fanfare.Play();

                    OrangeFireworks.SetActive(true);
                    
                    GM.EndUI.SetActive(true);

                    GM.isGamePlaying = false;
                    GM.background_O.SetActive(true);
                    GM.background_X.SetActive(false);
                    GM.Text_Top.SetText("O Wins!");



                    Debug.Log("Line Renderer: adding verts " + (hits.Count + 1));
                    List<Vector3> verts = new List<Vector3>();
                    for (int i = 0; i < hits.Count; i++)
                    {
                        verts.Add(hits[i].transform.localPosition);
                    }
                    verts.Add(transform.localPosition);
                    
                    LineC.SetVerts(verts.ToArray());
                }
            }
        }
    }
}
