using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyPoke : MonoBehaviour
{
    public BalanceGame BalGame;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NextButton")
        {
            
            SceneManager.LoadScene(1);
        } else if (other.tag == "MenuButton")
        {
            
            SceneManager.LoadScene(0);
        } else if (other.tag == "TbButton")
        {
            
            SceneManager.LoadScene(1);
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
        }
    }
}
