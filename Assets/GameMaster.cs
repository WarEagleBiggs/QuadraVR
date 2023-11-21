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
    
    private const int Empty = 0;
    private const int PlayerX = 1; // Blue is X
    private const int PlayerO = 2; // Orange is O
    // The 4x4x4 board, initialized to some value indicating an empty cell
    private int[,,] board = new int[4, 4, 4];

    // Call this method after each move to check for a win
private bool CheckForWin(int lastX, int lastY, int lastZ)
{
    int player = board[lastX, lastY, lastZ];

    // Check along x, y, z axes
    for (int i = 0; i < 4; i++)
    {
        if (board[i, lastY, lastZ] != player || 
            board[lastX, i, lastZ] != player || 
            board[lastX, lastY, i] != player)
        {
            return false;
        }
    }

    // Check diagonal on the xy plane
    if (lastX == lastY)
    {
        for (int i = 0; i < 4; i++)
        {
            if (board[i, i, lastZ] != player)
            {
                return false;
            }
        }
    }

    // Check anti-diagonal on the xy plane
    if (lastX + lastY == 3)
    {
        for (int i = 0; i < 4; i++)
        {
            if (board[i, 3 - i, lastZ] != player)
            {
                return false;
            }
        }
    }

    // Check diagonal on the xz plane
    if (lastX == lastZ)
    {
        for (int i = 0; i < 4; i++)
        {
            if (board[i, lastY, i] != player)
            {
                return false;
            }
        }
    }

    // Check anti-diagonal on the xz plane
    if (lastX + lastZ == 3)
    {
        for (int i = 0; i < 4; i++)
        {
            if (board[i, lastY, 3 - i] != player)
            {
                return false;
            }
        }
    }

    // Check diagonal on the yz plane
    if (lastY == lastZ)
    {
        for (int i = 0; i < 4; i++)
        {
            if (board[lastX, i, i] != player)
            {
                return false;
            }
        }
    }

    // Check anti-diagonal on the yz plane
    if (lastY + lastZ == 3)
    {
        for (int i = 0; i < 4; i++)
        {
            if (board[lastX, i, 3 - i] != player)
            {
                return false;
            }
        }
    }

    // Check 3D diagonals
    if (lastX == lastY && lastY == lastZ)
    {
        for (int i = 0; i < 4; i++)
        {
            if (board[i, i, i] != player)
            {
                return false;
            }
        }
    }

    if (lastX == lastY && lastX + lastZ == 3)
    {
        for (int i = 0; i < 4; i++)
        {
            if (board[i, i, 3 - i] != player)
            {
                return false;
            }
        }
    }

    // If none of the above checks returned false, there's a win
    return true;
}
    private void Start()
    {
        
    }

    // Call this method when a turn is taken
    public bool TakeTurn(int layer, int column, int row)
    {
        // Correct the indices to match the 0-indexed array.
        int x = layer - 1;
        int y = column - 1;
        int z = row - 1;

        // First, check if the indices are within the bounds of the board array
        if (x < 0 || x >= 4 || y < 0 || y >= 4 || z < 0 || z >= 4)
        {
            Debug.LogError($"Attempted to TakeTurn with invalid indices: layer={layer}, column={column}, row={row}");
            return false; // Indices out of bounds, exit the method
        }

        // Update the board with the current player's move
        board[x, y, z] = turn == 0 ? PlayerX : PlayerO;

        // Check for a win after the move
        bool hasWon = CheckForWin(x, y, z);

        // Switch turn for the next player
        turn = 1 - turn;

        // Instantiate the object based on the turn and set its position
        instantiatedObj = Instantiate(turn == 0 ? obj_x : obj_o);
        instantiatedObj.transform.SetParent(turn == 0 ? parent_X.transform : parent_O.transform, false);
        instantiatedObj.transform.localScale = turn == 0 ? obj_x.transform.localScale : obj_o.transform.localScale;
        instantiatedObj.transform.localPosition = new Vector3(x, y, z); // Use the corrected 0-based indices here

        // Activate the instantiated object
        instantiatedObj.SetActive(true);
        Debug.Log("inst");

        // Return whether the current player has won
        return hasWon;
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