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

    public enum DifficultyLevel
    {
        Easy = 0, 
        Normal, 
        Hard
    }

    public class ProbabilityTable
    {
        public int m_RandomProb;
        public int m_AggressiveProb;
        public int m_PassiveProb;

        public ProbabilityTable()
        {
            m_RandomProb = 50;
            m_AggressiveProb = 75;
            m_PassiveProb = 100;
        }
        
        public ProbabilityTable(int randomP, int aggressiveP, int passiveP)
        {
            m_RandomProb = randomP;
            m_AggressiveProb = aggressiveP;
            m_PassiveProb = passiveP;
        }

    }

    public static ProbabilityTable s_EasyProbability = new ProbabilityTable(40, 65, 100);
    public static ProbabilityTable s_NormalProbability = new ProbabilityTable(25, 60, 100);
    public static ProbabilityTable s_HardProbability = new ProbabilityTable(5, 70, 100);

    private bool ChooseRandomAvailablePosition(GameObject newO)
    {
        bool isPlaced = false;
        
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
                    isPlaced = true;
                }
                break;
            }
        }

        return isPlaced;
    }

    private bool BlockPlayer(GameObject newO)
    {
        bool isPlaced = false;                
        // --- find opponent's current best run length---
    
        Vector3Int availableCellPos = Vector3Int.zero;
        int runLength = m_GameMaster.GetOpenCellOnLongestLine(PlayerType.X, ref availableCellPos);
    
        if (runLength > 0) {
            // attempt to block  
            isPlaced = true;
    
            GameCellEntry cell = m_GameMaster.GetGridCell(availableCellPos);
            newO.transform.position = cell.m_EnterCube.transform.position;
    
            // store piece information into grid matrix
            cell.m_IsOccupied = true;
            cell.m_PlayerType = PlayerType.O;
        }

        return isPlaced;
    }

    private bool PlaceInLine(GameObject newO)
    {
        bool isPlaced = false;
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

        return isPlaced;
    }
    
    
    public void PlaceO(GameObject newO, DifficultyLevel difficultyLevel)
    {
        bool isPlaced = false;

        int randomDraw = Random.Range(0, 100);

        ProbabilityTable table = new ProbabilityTable();
        switch (difficultyLevel) {
            case DifficultyLevel.Easy:
                table = s_EasyProbability;
                break;
            case DifficultyLevel.Normal:
                table = s_NormalProbability;
                break;
            case DifficultyLevel.Hard:
                table = s_HardProbability;
                break;
        }
            
        if (randomDraw < table.m_RandomProb) {
            isPlaced = ChooseRandomAvailablePosition(newO);
        } else if (randomDraw < table.m_AggressiveProb) {
            isPlaced = BlockPlayer(newO);
        } else if (randomDraw < table.m_PassiveProb) {
            isPlaced = PlaceInLine(newO);
        }
        if (!isPlaced) {
            // fallback 
            isPlaced = ChooseRandomAvailablePosition(newO);
        }

        if (!isPlaced) {
            ChooseRandomAvailablePosition(newO);
        }
    }
}
