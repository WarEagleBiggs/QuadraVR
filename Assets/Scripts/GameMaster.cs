using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum PlayerType
{
    X = 0,
    O = 1
}

public class GameCellEntry
{
    public bool m_IsOccupied;
    public GameObject m_PlayerObj;
    public PlayerType m_PlayerType;

    public GameCellEntry()
    {
        m_IsOccupied = false;
        m_PlayerObj = null;
        m_PlayerType = PlayerType.X;
    }
}

public class GameMaster : MonoBehaviour
{
    //blue is x
    //orange is o

    //turn == 0 is blue turn
    //turn == 1 is orange turn
    public int turn = 1;

    public bool isGamePlaying = true;
    public GameObject EndUI;

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

    public MyPoke PokeScript;

    public AI AiObject;
    public bool isAiGame = false;

    public bool isMainMenu;

    public List<Transform> m_GridLayers = new List<Transform>();

    private GameCellEntry[,,] m_GameMatrix = new GameCellEntry[4, 4, 4];

    private void PopulateGameMatrix()
    {
        if (m_GridLayers.Count != 4)
        {
            Debug.LogError("Error, grid layers should be 4");
        }
        else
        {
            Vector3Int coord = Vector3Int.zero;
            // --- bottom to top (y) 16 children ---
            foreach (var layer in m_GridLayers)
            {
                coord.z = 0;
                // --- left to right (x) ---
                foreach (Transform cell in layer)
                {
                    EnterCube entry = cell.GetComponent<EnterCube>();
                    // store coordinate
                    entry.m_GridCoord = coord;
                    coord.z = coord.x == 3 ? coord.z + 1 : coord.z;
                    coord.x = coord.x < 3 ? coord.x + 1 : 0;
                }

                coord.y = coord.y < 3 ? coord.y + 1 : 0;
            }
        }

        // --- initialize GameObjects with GameCellEntry ---
        for (int x = 0; x < 4; ++x) {
            for (int y = 0; y < 4; ++y) {
                for (int z = 0; z < 4; ++z) {
                    m_GameMatrix[x, y, z] = new GameCellEntry();
                }
            }
        }

    }

    public GameCellEntry GetGridCell(Vector3Int coord)
    {
        GameCellEntry ret = null;
        if (m_GameMatrix != null)
        {
            ret = m_GameMatrix[coord.x, coord.y, coord.z];
        }

        return ret;
    }


private void Start()
    {
        // populate the layout of the game matrix
        PopulateGameMatrix();
        
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            isMainMenu = true;
        }
        else
        {
            isMainMenu = false;
        }
        
        if (Singleton.Instance.isFirstLaunch && isMainMenu)
        {
            Singleton.Instance.isFirstLaunch = false;
            
            PokeScript.BGSintro.SetActive(true);
            
            foreach (var obj in PokeScript.MenuObjs)
            {
                obj.SetActive(false);
            }
            
            StartCoroutine(BiggsIntroAnim(PokeScript.BGSintro, PokeScript.MenuObjs));
        }
    }

    // Call this method when a turn is taken
    public void TakeTurn()
    {
        // Switch turn
        turn = 1 - turn; // This will toggle between 0 and 1
        
        

        // Instantiate the object based on the turn and set its position
        if (turn == 0 && isGamePlaying)
        {
            instantiatedObj = Instantiate(obj_x, obj_x.transform.position, Quaternion.Euler(90, 45, 0)); // Add the position and default rotation
            instantiatedObj.transform.SetParent(parent_X.transform, false); // false to not apply world position to local
            instantiatedObj.transform.localScale = obj_x.transform.localScale; // Set the local scale to match obj_x
            instantiatedObj.transform.localPosition = Vector3.zero; // Set local position to (0,0,0)

        }
        else if (turn == 1 && isGamePlaying)
        {
           

            instantiatedObj = Instantiate(obj_o, obj_o.transform.position, Quaternion.identity); // Add the position and default rotation
            instantiatedObj.transform.SetParent(parent_O.transform, false); // false to not apply world position to local
            instantiatedObj.transform.localScale = obj_o.transform.localScale; // Set the local scale to match obj_x
            instantiatedObj.transform.localPosition = Vector3.zero; // Set local position to (0,0,0)
            
            if (isAiGame)
            {
                AiObject.PlaceO(instantiatedObj);
                
            }
        }

        // Activate the instantiated object
        instantiatedObj.SetActive(true);

        
    }

    void Update()
    {
        //sets UI
        if (turn == 0 && isGamePlaying)
        {
            background_X.SetActive(true);
            background_O.SetActive(false);
            Text_Top.SetText("X Turn");
        } else if (turn == 1 && isGamePlaying)
        {
            background_X.SetActive(false);
            background_O.SetActive(true);
            Text_Top.SetText("O Turn");
        }
        
        
    }

    public IEnumerator BiggsIntroAnim(GameObject oneObj, List<GameObject> objs)
    {
        
        yield return new WaitForSeconds(7);

        oneObj.SetActive(false);

        foreach (var obj in objs)
        {
            obj.SetActive(true);
        }
    }

   
    
    

    
}