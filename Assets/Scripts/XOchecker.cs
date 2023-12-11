using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class XOchecker : MonoBehaviour
{
    public HandGrabInteractor HGIR;
    public HandGrabInteractor HGIL;

    public bool amInSpot;
    
    public Transform refer;

    public bool canRun = true;
    

    // Update is called once per frame
    void Update()
    {
        if (canRun)
        {
            Move();
        }
    }

    public void Move()
    {
        if (!amInSpot && !HGIR.IsGrabbing && !HGIL.IsGrabbing)
        {
            //move to orign
            this.gameObject.transform.position = refer.transform.position;
        }
    }
}
