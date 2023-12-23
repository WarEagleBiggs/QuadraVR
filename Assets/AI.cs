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

    private List<GameObject> AiObjects = new List<GameObject>();

    private void ChooseRandomAvailablePosition(GameObject newO)
    {
        // --- choose a random available location ---
        while (true)
        {
            RandomSelectedID = Random.Range(0, Positions.Count-1);
            GameObject obj = Positions[RandomSelectedID];
            EnterCube cubeScript = obj.GetComponent<EnterCube>();

            if (!cubeScript.isTaken)
            {
                Debug.Log("Fill spot: " + RandomSelectedID);
                newO.transform.position = Positions[RandomSelectedID].transform.position;
                AiObjects.Add(newO);
                break;
            }

        }
    }
    
    public void PlaceO(GameObject newO)
    {
        bool isAdded = false;
        if (AiObjects.Count > 0)
        {
            // attempt to add to a neighboring object
            // todo
            
            //int maxInRow = FindMaxInRow();
        }

        if (!isAdded)
        {
            ChooseRandomAvailablePosition(newO);
        }

    }
}
