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
    public int turn;

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

    // Update is called once per frame
    void Update()
    {
        if (turn == 0)
        {
            //x turn
            background_X.SetActive(true);
            background_O.SetActive(false);

            Text_Top.SetText("X Turn");
        } else if (turn == 1)
        {
            //o turn
            background_X.SetActive(false);
            background_O.SetActive(true);

            Text_Top.SetText("O Turn");
        }
    }
}
