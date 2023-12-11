using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceGame : MonoBehaviour
{
    public GameObject gameBoard;
    public Quaternion quats = new Quaternion(270, 180,0,0);
    public float angle;


    // Update is called once per frame
    void Update()
    {
        gameBoard.transform.eulerAngles = new Vector3(-90, angle, 0);
        //gameBoard.transform.rotation = quats;
    }
}
