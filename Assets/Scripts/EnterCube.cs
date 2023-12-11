using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class EnterCube : MonoBehaviour
{
    public MeshRenderer mRend;

    public GameMaster gm;


    public HandGrabInteractor HGIR;
    public HandGrabInteractor HGIL;

    public bool isTaken;
    public bool isX;
    public bool isO;

    private void Start()
    {
        mRend = GetComponent<MeshRenderer>();
    }

    public GameObject PresetParent;
    private void OnTriggerStay(Collider other)
    {
        CubeRaycaster cubeRc = other.gameObject.GetComponent<CubeRaycaster>();
        
        if (other.tag == "O" || other.tag == "X")
        {
            XOchecker test = other.gameObject.GetComponent<XOchecker>();
            test.canRun = false;
            if(!isTaken)
            {
                mRend.enabled = true;
            }

            
            if (!HGIL.IsGrabbing && !HGIR.IsGrabbing && isTaken == false)
            {

                other.gameObject.transform.parent = PresetParent.transform;
                
                test.amInSpot = true;
                
                if (other.tag == "O")
                {
                    isO = true;
                } else if (other.tag == "X")
                {
                    isX = true;
                }
                
                //snap
                isTaken = true;
                
                
                
                HandGrabInteractable grab = other.gameObject.GetComponent<HandGrabInteractable>();
                grab.enabled = false;

                other.gameObject.transform.position = this.transform.position;
                
                mRend.enabled = false;
                
                cubeRc.RayCastCall();
                
                gm.TakeTurn();
            }
            test.Move();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "O" || other.tag == "X")
        {
            XOchecker test = other.gameObject.GetComponent<XOchecker>();
            test.canRun = true;
            mRend.enabled = false;
        }
    }
}
