using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyPoke : MonoBehaviour
{
    public BalanceGame BalGame;
    
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NextButton")
        {
            Singleton.Instance.MapPosition = BalGame.WholeGame.transform.position;
            SceneManager.LoadScene(0);
        } else if (other.tag == "MenuButton")
        {
            Singleton.Instance.MapPosition = BalGame.WholeGame.transform.position;

            //SceneManager.LoadScene(0);
        } else if (other.tag == "TbButton")
        {
            Singleton.Instance.MapPosition = BalGame.WholeGame.transform.position;

            SceneManager.LoadScene(0);
        } else if (other.tag == "ExitButton")
        {
            Application.Quit();
        } else if (other.tag == "TurnLeft")
        {
            Debug.Log("turn left");
            BalGame.angle = BalGame.angle + 45;
        } else if (other.tag == "TurnRight")
        {
            Debug.Log("turn right");
            BalGame.angle = BalGame.angle - 45;
        } 
    }
}
