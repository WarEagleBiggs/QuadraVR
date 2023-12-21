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

    public GameMaster GameMasterScript;

    public Vector3 PositionToset;
    public GameObject GameBoard;

    public GameObject BGSintro;
    public List<GameObject> MenuObjs;
    public List<GameObject> GuideObjs;
    public List<GameObject> AboutObjs;

    public OVRManager MyMan;
    private void Start()
    {

        
        Click = Singleton.Instance.GetComponent<AudioSource>();
        MyMan.isInsightPassthroughEnabled = Singleton.Instance.isPassthrough;
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NextButton")
        {
            Click.Play();
            Singleton.Instance.MapPosition = BalGame.WholeGame.transform.position;
            Singleton.Instance.isPassthrough = MyMan.isInsightPassthroughEnabled;
            SceneManager.LoadScene(1);
        } else if (other.tag == "MenuButton")
        {
            Click.Play();

            Singleton.Instance.MapPosition = BalGame.WholeGame.transform.position;
            Singleton.Instance.isPassthrough = MyMan.isInsightPassthroughEnabled;
            SceneManager.LoadScene(0);
        } else if (other.tag == "TbButton")
        {
            Click.Play();

            Singleton.Instance.MapPosition = BalGame.WholeGame.transform.position;
            Singleton.Instance.isPassthrough = MyMan.isInsightPassthroughEnabled;
            SceneManager.LoadScene(1);
        } else if (other.tag == "AiButton")
        {
            Click.Play();
            Singleton.Instance.isPassthrough = MyMan.isInsightPassthroughEnabled;
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
        } else if (other.tag == "GuideButton")
        {
            Click.Play();
            foreach (var obj in MenuObjs)
            {
                obj.SetActive(false);
            }
            foreach (var obj in GuideObjs)
            {
                obj.SetActive(true);
            }
            
        } else if (other.tag == "AboutButton")
        {
            Click.Play();
            foreach (var obj in MenuObjs)
            {
                obj.SetActive(false);
            }
            foreach (var obj in AboutObjs)
            {
                obj.SetActive(true);
            }
            
        } else if (other.tag == "BackButton_Guide")
        {
            Click.Play();
            foreach (var obj in GuideObjs)
            {
                obj.SetActive(false);
            }
            foreach (var obj in MenuObjs)
            {
                obj.SetActive(true);
            }
            
        } else if (other.tag == "BackButton_About")
        {
            Click.Play();
            foreach (var obj in AboutObjs)
            {
                obj.SetActive(false);
            }
            foreach (var obj in MenuObjs)
            {
                obj.SetActive(true);
            }
            
        } else if (other.tag == "PassthroughButton")
        {
            Click.Play();

            if (MyMan.isInsightPassthroughEnabled == false)
            {
                MyMan.isInsightPassthroughEnabled = true;
            } else if (MyMan.isInsightPassthroughEnabled == true)
            {
                MyMan.isInsightPassthroughEnabled = false;
            }
        }
    }
}
