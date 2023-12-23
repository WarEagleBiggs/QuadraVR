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
    public GameMaster m_GameMaster;
    public List<GameObject> Positions;

    private void ChooseRandomAvailablePosition(GameObject newO)
    {
        // --- choose a random available location ---
        while (true) {
            RandomSelectedID = Random.Range(0, Positions.Count-1);
            GameObject obj = Positions[RandomSelectedID];
            EnterCube cubeScript = obj.GetComponent<EnterCube>();

            if (!cubeScript.isTaken) {
                EnterCube ec = Positions[RandomSelectedID].GetComponent<EnterCube>();
                newO.transform.position = ec.transform.position;
                
                // store piece information into grid matrix
                GameCellEntry cell = m_GameMaster.GetGridCell(ec.m_GridCoord);
                if (cell != null) {
                    cell.m_IsOccupied = true;
                    cell.m_PlayerType = PlayerType.O;
                }
                break;
            }
        }
    }
    
    public void PlaceO(GameObject newO)
    {
        bool isPlaced = false;
        
        // if (!isPlaced) {
        //     // --- find opponent's current best run length---
        //
        //     Vector3Int availableCellPos = Vector3Int.zero;
        //     int runLength = m_GameMaster.GetOpenCellOnLongestLine(PlayerType.X, ref availableCellPos);
        //
        //     if (runLength > 0) {
        //         // attempt to block  
        //         isPlaced = true;
        //
        //         GameCellEntry cell = m_GameMaster.GetGridCell(availableCellPos);
        //         newO.transform.position = cell.m_EnterCube.transform.position;
        //
        //         // store piece information into grid matrix
        //         cell.m_IsOccupied = true;
        //         cell.m_PlayerType = PlayerType.O;
        //     }
        // }

        if (!isPlaced) {
            // --- find AIs best open move toward a win ---

            // number of AI's in Game Matrix
            int numAi = m_GameMaster.GetNumberOfPieces(PlayerType.O);

            if (numAi > 0) {
                
                // attempt to add to a neighboring object
                Vector3Int availableCellPos = Vector3Int.zero;
                int runLength = m_GameMaster.GetOpenCellOnLongestLine(PlayerType.O, ref availableCellPos);
               
                if (runLength > 0) {
                    isPlaced = true;
                    GameCellEntry cell = m_GameMaster.GetGridCell(availableCellPos);
                    newO.transform.position = cell.m_EnterCube.transform.position;

                    // store piece information into grid matrix
                    cell.m_IsOccupied = true;
                    cell.m_PlayerType = PlayerType.O;
                }
            }
        }

        if (!isPlaced) {
            ChooseRandomAvailablePosition(newO);
        }
    }
}
