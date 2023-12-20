using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MyPoke : MonoBehaviour
{
    public BalanceGame BalGame;
    public AudioSource Click;

    public Vector3 PositionToset;
    public GameObject GameBoard;

    private void Start()
    {
        Click = Singleton.Instance.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NextButton")
        {
            Click.Play();
            Singleton.Instance.MapPosition = BalGame.WholeGame.transform.position;
            SceneManager.LoadScene(1);
        } else if (other.tag == "MenuButton")
        {
            Click.Play();

            Singleton.Instance.MapPosition = BalGame.WholeGame.transform.position;

            SceneManager.LoadScene(0);
        } else if (other.tag == "TbButton")
        {
            Click.Play();

            Singleton.Instance.MapPosition = BalGame.WholeGame.transform.position;

            SceneManager.LoadScene(1);
        } else if (other.tag == "AiButton")
        {
            Click.Play();

            SceneManager.LoadScene(2);
        } else if (other.tag == "TurnLeft")
        {
            Click.Play();

            Debug.Log("turn left");
            BalGame.angle = BalGame.angle + 45;
        } else if (other.tag == "TurnRight")
        {
            Click.Play();

            Debug.Log("turn right");
            BalGame.angle = BalGame.angle - 45;
        } else if (other.tag == "ResetBoard")
        {
            Click.Play();
            GameBoard.transform.position = PositionToset;
            //set board
        }
    }
}
