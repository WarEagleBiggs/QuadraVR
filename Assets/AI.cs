using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AI : MonoBehaviour
{
    public int RandomSelectedID;
    public List<GameObject> Positions;

    public void PlaceO(GameObject newO)
    {
        while (true)
        {
            RandomSelectedID = Random.Range(0, Positions.Count-1);
            GameObject obj = Positions[RandomSelectedID];
            EnterCube cubeScript = obj.GetComponent<EnterCube>();

            if (!cubeScript.isTaken)
            {
                Debug.Log("Fill spot: " + RandomSelectedID);
                newO.transform.position = Positions[RandomSelectedID].transform.position;
                Debug.Log(newO);
                break;
            }
        }
    }
}
