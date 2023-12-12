using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class BalanceGame : MonoBehaviour
{
    public GameObject Torus;
    public GameObject OtherEl;
    public GameObject gameBoard;
    public Quaternion quats = new Quaternion(270, 180,0,0);
    public float angle;

    public GameObject WholeGame;
    
    public HandGrabInteractor HGIR;
    public HandGrabInteractor HGIL;

    private void Start()
    {
       
        WholeGame.transform.position = Singleton.Instance.MapPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Torus.transform.rotation 
        Torus.transform.eulerAngles = new Vector3(-90, Torus.gameObject.transform.eulerAngles.y, 0);
        OtherEl.transform.eulerAngles = new Vector3(-90, OtherEl.gameObject.transform.eulerAngles.y, 0);

        if (!HGIR.IsGrabbing && !HGIL.IsGrabbing)
        {
            gameBoard.transform.eulerAngles = new Vector3(gameBoard.transform.eulerAngles.x, angle, gameBoard.transform.eulerAngles.z);

        }
        //gameBoard.transform.rotation = quats;
    }
}
