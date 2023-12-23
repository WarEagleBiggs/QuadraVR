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
        // number of AI's in Game Matrix
        int numAi = m_GameMaster.GetNumberOfPieces(PlayerType.O);
        
        bool isAdded = false;
        if (numAi > 0) {
            // attempt to add to a neighboring object
            Vector3Int cellPos = Vector3Int.zero;
            if (m_GameMaster.GetOpenCellOnLongestLine(PlayerType.O, ref cellPos)) {
                
                isAdded = true;
                
                GameCellEntry cell = m_GameMaster.GetGridCell(cellPos);

                newO.transform.position = cell.m_EnterCube.transform.position;

                // store piece information into grid matrix
                cell.m_IsOccupied = true;
                cell.m_PlayerType = PlayerType.O;
            }
        }

        if (!isAdded) {
            ChooseRandomAvailablePosition(newO);
        }

    }
}
