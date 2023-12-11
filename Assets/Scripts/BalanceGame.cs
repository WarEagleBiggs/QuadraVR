using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceGame : MonoBehaviour
{
    public GameObject gameBoard;
    public Quaternion quats;


    // Update is called once per frame
    void Update()
    {
        //gameBoard.transform.eulerAngles = new Vector3(-90, gameBoard.transform.eulerAngles.y, 0);
        gameBoard.transform.rotation = quats;
    }
}
