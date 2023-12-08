using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotTracker : MonoBehaviour
{
    private char[,,] board;
    private const int Size = 4; // Size of the board (4x4x4)

    void Start()
    {
        // Initialize the game board
        board = new char[Size,Size,Size];
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                for (int z = 0; z < Size; z++)
                {
                    board[x,y,z] = ' '; // Empty space
                }
            }
        }
    }

    // Method to place a mark (X or O) at a given position
    public bool PlaceMark(char mark, int x, int y, int z)
    {
        if (x < 0 || x >= Size || y < 0 || y >= Size || z < 0 || z >= Size)
            return false; // Invalid position

        if (board[x,y,z] == ' ') // Check if the spot is empty
        {
            board[x,y,z] = mark; // Place the mark
            return true;
        }
 
        return false; // Spot already filled
    }

    // Method to check the status of a spot
    public char GetSpotStatus(int x, int y, int z)
    {
        if (x < 0 || x >= Size || y < 0 || y >= Size || z < 0 || z >= Size)
            return '?'; // Invalid position

        return board[x,y,z];
    }
}
