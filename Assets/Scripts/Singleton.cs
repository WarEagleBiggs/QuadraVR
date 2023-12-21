using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }


    public bool isPassthrough = true;
    public bool isFirstLaunch = true;
    public  bool canPressButton = true;

    public Vector3 MapPosition;

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
    
    public IEnumerator PressCooldown()
    {
        canPressButton = false;
        yield return new WaitForSeconds(0.5f);
        canPressButton = true;
    }

}
