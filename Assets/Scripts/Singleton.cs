using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }


    public bool isPassthrough = true;
    public bool isFirstLaunch = true;
    public  bool canPressButton = true;

    public bool isEasy = false;
    public bool isNormal = false;
    public bool isHard = false;

    public Vector3 MapPosition;

    private float timer = 0f;
    private float toggleInterval = 5f; // Interval in seconds
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= toggleInterval)
        {
            timer = 0f;
            canPressButton = true;
        }
    }
    

    public IEnumerator PressCooldown()
    {
        canPressButton = false;
        yield return new WaitForSeconds(0.5f);
        canPressButton = true;
    }

}
