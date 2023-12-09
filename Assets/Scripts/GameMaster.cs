using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMaster : MonoBehaviour
{
    //blue is x
    //orange is o
    
    //turn == 0 is blue turn
    //turn == 1 is orange turn
    public int turn = 1;

    //colored backgrounds
    public GameObject background_X;
    public GameObject background_O;

    //texts
    public TextMeshProUGUI Text_Top;
    public TextMeshProUGUI Text_X;
    public TextMeshProUGUI Text_O;

    //x and o
    public GameObject obj_x;
    public GameObject obj_o;

    private GameObject instantiatedObj;
    public GameObject parent_X;
    public GameObject parent_O;

    private void Start()
    {
        
    }

    // Call this method when a turn is taken
    public void TakeTurn()
    {
        // Switch turn
        turn = 1 - turn; // This will toggle between 0 and 1

        // Instantiate the object based on the turn and set its position
        if (turn == 0)
        {
            instantiatedObj = Instantiate(obj_x, obj_x.transform.position, Quaternion.Euler(90, 45, 0)); // Add the position and default rotation
            instantiatedObj.transform.SetParent(parent_X.transform, false); // false to not apply world position to local
            instantiatedObj.transform.localScale = obj_x.transform.localScale; // Set the local scale to match obj_x
            instantiatedObj.transform.localPosition = Vector3.zero; // Set local position to (0,0,0)
            
            
        }
        else if (turn == 1)
        {
            instantiatedObj = Instantiate(obj_o, obj_o.transform.position, Quaternion.identity); // Add the position and default rotation
            instantiatedObj.transform.SetParent(parent_O.transform, false); // false to not apply world position to local
            instantiatedObj.transform.localScale = obj_o.transform.localScale; // Set the local scale to match obj_x
            instantiatedObj.transform.localPosition = Vector3.zero; // Set local position to (0,0,0)
            
            
        }

        // Activate the instantiated object
        instantiatedObj.SetActive(true);

        
    }

    void Update()
    {
        if (turn == 0)
        {
            background_X.SetActive(true);
            background_O.SetActive(false);
            Text_Top.SetText("X Turn");
        } else if (turn == 1)
        {
            background_X.SetActive(false);
            background_O.SetActive(true);
            Text_Top.SetText("O Turn");
        }
    }
}