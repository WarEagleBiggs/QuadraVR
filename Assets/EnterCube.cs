using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class EnterCube : MonoBehaviour
{
    //whoami
    public int coords;
    
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
        if (other.tag == "O" || other.tag == "X")
        {
            if(!isTaken){
            mRend.enabled = true;
            }

            
            if (!HGI.IsGrabbing && isTaken == false)
            {
                //snap
                isTaken = true;
                
                
                
                HandGrabInteractable grab = other.gameObject.GetComponent<HandGrabInteractable>();
                grab.enabled = false;

                other.gameObject.transform.position = this.transform.position;
                
                mRend.enabled = false;
                
                gm.TakeTurn();
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
