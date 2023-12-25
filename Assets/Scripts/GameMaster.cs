using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public enum PlayerType
{
    X = 0,
    O = 1
}

public class GameCellEntry
{
    public bool m_IsOccupied;
    public EnterCube m_EnterCube;
    public PlayerType m_PlayerType;

    public GameCellEntry()
    {
        m_IsOccupied = false;
        m_EnterCube = null;
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
    
    public AI.DifficultyLevel m_AiDifficulty = AI.DifficultyLevel.Easy;
    
    public List<Transform> m_GridLayers = new List<Transform>();

    private GameCellEntry[,,] m_GameMatrix = new GameCellEntry[4, 4, 4];

    private void PopulateGameMatrix()
    {
        // --- initialize GameObjects with GameCellEntry ---
        for (int x = 0; x < 4; ++x) {
            for (int y = 0; y < 4; ++y) {
                for (int z = 0; z < 4; ++z) {
                    m_GameMatrix[x, y, z] = new GameCellEntry();
                }
            }
        }

        if (m_GridLayers.Count != 4) {
            Debug.LogError("Error, grid layers should be 4");
        }else {
            Vector3Int coord = Vector3Int.zero;
            // --- bottom to top (y) 16 children ---
            foreach (var layer in m_GridLayers) {
                coord.z = 0;
                // --- left to right (x) ---
                foreach (Transform cell in layer) {
                    EnterCube ec = cell.GetComponent<EnterCube>();
                    // store coordinate
                    ec.m_GridCoord = coord;

                    // store EnterCube instance inside of matrix
                    GetGridCell(coord).m_EnterCube = ec;
                    
                    coord.z = coord.x == 3 ? coord.z + 1 : coord.z;
                    coord.x = coord.x < 3 ? coord.x + 1 : 0;
                }

                coord.y = coord.y < 3 ? coord.y + 1 : 0;
            }
        }
    }

    public int GetNumberOfPieces(PlayerType type)
    {
        int count = 0;
        
        foreach (var entry in m_GameMatrix) {
            if (entry.m_PlayerType == type) {
                count++;
            }
        }

        return count;
    }


    private bool IsCellCoordinateValid(Vector3Int cell)
    {
        return (cell.x >= 0 && cell.x < 4 && cell.y >= 0 && cell.y < 4 && cell.z >= 0 && cell.z < 4);
    }

    private Vector3Int[] m_AllDirections =
        new Vector3Int[]
        {
            new Vector3Int(1, 0, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(0, 0, 1),
            new Vector3Int(0, 0, -1),

            // leave x
            new Vector3Int(1, 1, 0),
            new Vector3Int(1, -1, 0),
            new Vector3Int(1, 0, 1),
            new Vector3Int(1, 0, -1),

            new Vector3Int(-1, 1, 0),
            new Vector3Int(-1, -1, 0),
            new Vector3Int(-1, 0, 1),
            new Vector3Int(-1, 0, -1),

            // leave y
            new Vector3Int(-1, 1, 0),
            new Vector3Int(0, 1, 1),
            new Vector3Int(0, 1, -1),
            
            new Vector3Int(-1, -1, 0),
            new Vector3Int(0, -1, 1),
            new Vector3Int(0, -1, -1),
            
            // leave z
            new Vector3Int(-1, 0, 1),
            new Vector3Int(0, 1, 1),
            new Vector3Int(0, -1, 1),

            new Vector3Int(-1, 0, -1),
            new Vector3Int(0, 1, -1),
            new Vector3Int(0, -1, -1),
        };
    
    
    private List<Vector3Int> GetAllNeighbors(Vector3Int cell)
    {
        List<Vector3Int> neighbors = new List<Vector3Int>();

        foreach (var dirV in m_AllDirections) {
            if (IsCellCoordinateValid(cell + dirV)) {
                neighbors.Add(cell + dirV);
            }
        }
        
        return neighbors;
    }

    private int GetRunLengthWithOpenInDirection(Vector3Int startPos, Vector3Int dirV, PlayerType type, ref Vector3Int openMove)
    {
        // --- for valid run with openings in direction, return run length > 0 ---
        
        int validRunLength = 0;

        const int maxLength = 4;
        int potentialRunLength = 1;
        int distance = 1;

        bool isMoveValid = false;
        
        while (true) {
            
            Vector3Int testPos = startPos + (dirV * distance);
            
            if (!IsCellCoordinateValid(testPos)) {
                break;
            }
           
            GameCellEntry testEntry = GetGridCell(testPos);
        
            if (testEntry.m_IsOccupied) { 
                // cell is occupied
                if (testEntry.m_PlayerType == type) {
                    // same type
                    potentialRunLength++;
                } else {
                    // opposite type, not a valid direction
                    break;
                }
            } else {
                // available cell 
                if (!isMoveValid) {
                    // set open move
                    openMove = testPos;
                    isMoveValid = true;
                } else {
                    // success, done searching
                    validRunLength = potentialRunLength;
                    break;
                }
                potentialRunLength++;
            }

            if (potentialRunLength == maxLength && isMoveValid) {
                // success, game winning length
                validRunLength = potentialRunLength;
                break;
            }
            
            // increment 
            distance++;
        }
       
        return validRunLength;
    }
    
    public int GetOpenCellOnLongestLine(PlayerType type, ref Vector3Int openPos)
    {
        int maxRunLength = 0;

        // --- loop through every element in Matrix that matches type ---
        for (int x = 0; x < 4; ++x) {
            for (int y = 0; y < 4; ++y) {
                for (int z = 0; z < 4; ++z) {
                    Vector3Int pos = new Vector3Int(x, y, z);
                    GameCellEntry entry = GetGridCell(pos);
                    if (entry.m_PlayerType == type) {
                        // --- test cell as potential endpoint, look in every direction searching for longest run ---
                        foreach (var dirV in m_AllDirections) {
                            
                            Vector3Int openMove = Vector3Int.zero;
                            int runLength = GetRunLengthWithOpenInDirection(pos, dirV, type, ref openMove);

                            if (runLength > maxRunLength) {
                                maxRunLength = runLength;
                                openPos = openMove;
                            }
                        }
                        
                    }
                }
            }
        }        
        
        return maxRunLength;
    }

    private int CountRunLengthInDirection(Vector3Int startPos, Vector3Int dirV, 
        PlayerType type, ref List<Vector3Int> cellPosList)
    {
        // --- count maximum run length in direction, return run length > 0 ---

        int validRunLength = 1;

        int distance = 1;

        cellPosList.Add(startPos);
        
        while (true) {
            Vector3Int testPos = startPos + (dirV * distance);
            
            if (!IsCellCoordinateValid(testPos)) {
                break;
            }
           
            GameCellEntry testEntry = GetGridCell(testPos);
        
            if (testEntry.m_IsOccupied) { 
                // cell is occupied
                if (testEntry.m_PlayerType == type) {
                    // same type
                    validRunLength++;
                    cellPosList.Add(testPos);
                } else {
                    // opposite type, not a valid direction
                    break;
                }
            } else {
                // available cell 
                break;
            }

            // increment 
            distance++;
        }
       
        return validRunLength;
    }

    public Dictionary<PlayerType, List<Tuple<EnterCube, EnterCube>>> m_WinningLinesListPerPlayerMap =
        new Dictionary<PlayerType, List<Tuple<EnterCube, EnterCube>>>();
    
    public bool IsAWin(PlayerType type)
    {
        // --- loop through every element in Matrix that matches type ---

        bool isWin = false;
        
        List<Vector3Int> cellPosList = new List<Vector3Int>();

        for (int x = 0; x < 4; ++x) {
            for (int y = 0; y < 4; ++y) {
                for (int z = 0; z < 4; ++z) {
                    Vector3Int pos = new Vector3Int(x, y, z);
                    GameCellEntry entry = GetGridCell(pos);
                    if (entry.m_IsOccupied && entry.m_PlayerType == type) {
                        // --- test cell as potential endpoint, look in every direction searching for longest run ---
                        foreach (var dirV in m_AllDirections) {
                            Vector3Int openMove = Vector3Int.zero;
                            cellPosList.Clear();
                            int runLength = CountRunLengthInDirection(pos, dirV, type, ref cellPosList);
                            if (runLength == 4) {
                                isWin = true;
                                AddEndPointsToWinningList(
                                    type, cellPosList[0], cellPosList[^1]);
                            }
                        }
                    }
                }
            }
        }

        return isWin;
    }

    private void AddEndPointsToWinningList(PlayerType type, Vector3Int coord0, Vector3Int coordN)
    {
        // store winning points list
        if (!m_WinningLinesListPerPlayerMap.ContainsKey(type)) {
            m_WinningLinesListPerPlayerMap[type] = new List<Tuple<EnterCube, EnterCube>>();
        }

        GameCellEntry cellEntry0 = GetGridCell(coord0);
        GameCellEntry cellEntryN = GetGridCell(coordN);

        m_WinningLinesListPerPlayerMap[type].Add(new Tuple<EnterCube, EnterCube>(
            cellEntry0.m_EnterCube, cellEntryN.m_EnterCube));
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

        if (Singleton.Instance.isEasy)
        {
            m_AiDifficulty = AI.DifficultyLevel.Easy;
        } else if (Singleton.Instance.isNormal)
        {
            m_AiDifficulty = AI.DifficultyLevel.Normal;
        } else if (Singleton.Instance.isHard)
        {
            m_AiDifficulty = AI.DifficultyLevel.Hard;
        }
        
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
                StartCoroutine(WaitToPlace());
                
            }
        }

        // Activate the instantiated object
        instantiatedObj.SetActive(true);

        
    }

    public IEnumerator WaitToPlace()
    {
        yield return new WaitForSeconds(2);
        AiObject.PlaceO(instantiatedObj, m_AiDifficulty);
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