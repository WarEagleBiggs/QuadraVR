using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCube : MonoBehaviour
{
    public MeshRenderer mRend;

    private void Start()
    {
        mRend = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "O" || other.tag == "X")
        {
            mRend.enabled = true;
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
