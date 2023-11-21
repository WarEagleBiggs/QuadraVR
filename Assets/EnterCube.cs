using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class EnterCube : MonoBehaviour
{
    public MeshRenderer mRend;

    public GameMaster gm;


    public HandGrabInteractor HGI;

    public bool isTaken;

    private void Start()
    {
        mRend = GetComponent<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("O") || other.CompareTag("X")) && !isTaken && !HGI.IsGrabbing)
        {
            // Snap the piece in place
            isTaken = true;
            HandGrabInteractable grab = other.gameObject.GetComponent<HandGrabInteractable>();
            grab.enabled = false;
            other.gameObject.transform.position = this.transform.position;
            mRend.enabled = false;

            // Parse the cube's name to get its layer, column, and row
            string[] splitName = this.gameObject.name.Split('_');
            if (splitName.Length == 4 && splitName[0] == "Cube")
            {
                // Subtract 1 to convert to 0-based index
                int layer = int.Parse(splitName[1]) - 1;
                int column = int.Parse(splitName[2]);
                int row = int.Parse(splitName[3]);

                // Check if layer is within bounds (since it's 0-indexed after subtraction)
                if(layer >= 0 && layer < 4)
                {
                    // Update the board and check for a win
                    bool won = gm.TakeTurn(layer, column, row);
                    if (won)
                    {
                        Debug.Log(gm.turn == 0 ? "X Wins!" : "O Wins!");
                    }
                }
                else
                {
                    Debug.LogError("Cube layer index out of bounds: " + this.gameObject.name);
                }
            }
            else
            {
                Debug.LogError("Invalid cube name for determining grid position: " + this.gameObject.name);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "O" || other.tag == "X")
        {
            mRend.enabled = false;
        }
    }
}
