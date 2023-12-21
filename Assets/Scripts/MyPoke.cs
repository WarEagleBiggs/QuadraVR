using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    public GameObject HTPM;
    public GameObject HTPA;
    public OVRManager MyMan;

    
    private void Start()
    {

        
        Click = Singleton.Instance.GetComponent<AudioSource>();
        MyMan.isInsightPassthroughEnabled = Singleton.Instance.isPassthrough;
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NextButton" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();
            Singleton.Instance.MapPosition = BalGame.WholeGame.transform.position;
            Singleton.Instance.isPassthrough = MyMan.isInsightPassthroughEnabled;
            Singleton.Instance.canPressButton = true;
            SceneManager.LoadScene(1);
        } else if (other.tag == "MenuButton" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();

            Singleton.Instance.MapPosition = BalGame.WholeGame.transform.position;
            Singleton.Instance.isPassthrough = MyMan.isInsightPassthroughEnabled;
            Singleton.Instance.canPressButton = true;

            SceneManager.LoadScene(0);
        } else if (other.tag == "TbButton" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();

            Singleton.Instance.MapPosition = BalGame.WholeGame.transform.position;
            Singleton.Instance.isPassthrough = MyMan.isInsightPassthroughEnabled;
            Singleton.Instance.canPressButton = true;

            SceneManager.LoadScene(1);
        } else if (other.tag == "AiButton" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();
            Singleton.Instance.MapPosition = BalGame.WholeGame.transform.position;
            Singleton.Instance.isPassthrough = MyMan.isInsightPassthroughEnabled;
            Singleton.Instance.canPressButton = true;

            SceneManager.LoadScene(2);
        } else if (other.tag == "TurnLeft" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();

            Debug.Log("turn left");
            BalGame.angle = BalGame.angle + 45;
        } else if (other.tag == "TurnRight" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();

            Debug.Log("turn right");
            BalGame.angle = BalGame.angle - 45;
        } else if (other.tag == "ResetBoard" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();
            GameBoard.transform.position = PositionToset;
            //set board
        } else if (other.tag == "GuideButton" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();
            foreach (var obj in MenuObjs)
            {
                obj.SetActive(false);
            }
            foreach (var obj in GuideObjs)
            {
                obj.SetActive(true);
            }
            
        } else if (other.tag == "AboutButton" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();
            foreach (var obj in MenuObjs)
            {
                obj.SetActive(false);
            }
            foreach (var obj in AboutObjs)
            {
                obj.SetActive(true);
            }
            
        } else if (other.tag == "BackButton_Guide" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();
            foreach (var obj in GuideObjs)
            {
                obj.SetActive(false);
            }
            foreach (var obj in MenuObjs)
            {
                obj.SetActive(true);
            }
            
        } else if (other.tag == "BackButton_About" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();
            foreach (var obj in AboutObjs)
            {
                obj.SetActive(false);
            }
            foreach (var obj in MenuObjs)
            {
                obj.SetActive(true);
            }
            
        } else if (other.tag == "PassthroughButton" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();

            if (MyMan.isInsightPassthroughEnabled == false)
            {
                MyMan.isInsightPassthroughEnabled = true;
            } else if (MyMan.isInsightPassthroughEnabled == true)
            {
                MyMan.isInsightPassthroughEnabled = false;
            }
        } else if (other.tag == "HTPM" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();
            foreach (var obj in MenuObjs)
            {
                obj.SetActive(false);
            }
            HTPM.SetActive(true);
        } else if (other.tag == "BackButton_HTPM" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();
            foreach (var obj in MenuObjs)
            {
                obj.SetActive(true);
            }
            HTPM.SetActive(false);
        } else if (other.tag == "HTPA" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();
            foreach (var obj in MenuObjs)
            {
                obj.SetActive(false);
            }
            HTPA.SetActive(true);
        } else if (other.tag == "BackButtonHTPA" && Singleton.Instance.canPressButton)
        {
            StartCoroutine(Singleton.Instance.PressCooldown());
            Click.Play();
            foreach (var obj in MenuObjs)
            {
                obj.SetActive(true);
            }
            HTPA.SetActive(false);
        }
    }
}
