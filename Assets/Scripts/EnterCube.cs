using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.HandGrab;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class EnterCube : MonoBehaviour
{
    public MeshRenderer mRend;

    public AudioSource Snap;
    public GameMaster gm;

    public HandGrabInteractor HGIR;
    public HandGrabInteractor HGIL;

    public bool isTaken;
    public bool isX;
    public bool isO;
    [FormerlySerializedAs("m_GridCoordinate")] public Vector3Int m_GridCoord;

    private void Start()
    {
        mRend = GetComponent<MeshRenderer>();
    }

    public GameObject PresetParent;
    private void OnTriggerStay(Collider other)
    {
        CubeRaycaster cubeRc = other.gameObject.GetComponent<CubeRaycaster>();
        
        if (other.tag == "O" || other.tag == "X")
        {
            XOchecker test = other.gameObject.GetComponent<XOchecker>();
            test.canRun = false;
            if(!isTaken)
            {
                mRend.enabled = true;
            }

            
            if (!HGIL.IsGrabbing && !HGIR.IsGrabbing && isTaken == false)
            {
                // --- player has released the piece, snap-to cell center --- 
                
                Snap.Play();
                other.gameObject.transform.parent = PresetParent.transform;
                
                test.amInSpot = true;
                
                if (other.tag == "O")
                {
                    isO = true;
                } else if (other.tag == "X")
                {
                    isX = true;
                }
                
                //snap
                isTaken = true;

                // store piece information into grid matrix
                GameCellEntry cell = gm.GetGridCell(m_GridCoord);
                if (cell != null)
                {
                    cell.m_IsOccupied = true;
                    cell.m_PlayerType = isX ? PlayerType.X : PlayerType.O;
                }

                HandGrabInteractable grab = other.gameObject.GetComponent<HandGrabInteractable>();
                grab.enabled = false;

                other.gameObject.transform.position = this.transform.position;
                
                mRend.enabled = false;
                
                if (gm.IsAWin(PlayerType.X)) {
                    // Fanfare.Play();
                    // BlueFireworks.SetActive(true);
                    
                    gm.EndUI.SetActive(true);
                    gm.isGamePlaying = false;
                    gm.background_O.SetActive(false);
                    gm.background_X.SetActive(true);
                    gm.Text_Top.SetText("X Wins!");

                    // List<List<Vector3Int>> lineList = gm.m_WinningLinesListPerPlayerMap[PlayerType.X];
                    // foreach (var line in lineList) {
                    //     Debug.LogError("winning line: ");
                    //     foreach (var cellCoord in line) {
                    //         GameCellEntry cellEntry = gm.GetGridCell(cellCoord);
                    //         Debug.LogError(cellEntry.m_EnterCube.transform.position + " ");
                    //     }
                    // }
                }

                if (gm.IsAWin(PlayerType.O)) {
                    // Fanfare.Play();
                    // OrangeFireworks.SetActive(true);
                    
                    gm.EndUI.SetActive(true);
                    gm.isGamePlaying = false;
                    gm.background_O.SetActive(true);
                    gm.background_X.SetActive(false);
                    gm.Text_Top.SetText("O Wins!");
                    
                    // List<List<Vector3Int>> lineList = gm.m_WinningLinesListPerPlayerMap[PlayerType.X];
                    // foreach (var line in lineList) {
                    //     Debug.LogError("winning line: ");
                    //     foreach (var cellCoord in line) {
                    //         GameCellEntry cellEntry = gm.GetGridCell(cellCoord);
                    //         Debug.LogError(cellEntry.m_EnterCube.transform.position + " ");
                    //     }
                    // }

                }
                //
                //cubeRc.RayCastCall();
                
                gm.TakeTurn();
            }
            test.Move();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "O" || other.tag == "X")
        {
            XOchecker test = other.gameObject.GetComponent<XOchecker>();
            test.canRun = true;
            mRend.enabled = false;
        }
    }
}
