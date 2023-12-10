using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnector : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3[] m_LineVerts;
    public string whoami;
    
    void Start()
    {
        // Add a LineRenderer component if not already attached
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = 0;
        
        if (whoami == "X")
        {
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.blue;
        } else if (whoami == "O")
        {
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.yellow + Color.red;
            lineRenderer.endColor = Color.yellow + Color.red;
        }
    }
  
    public void SetVerts(Vector3[] verts)
    {
        m_LineVerts = verts;
       
        if (m_LineVerts != null)
        {
            lineRenderer.positionCount = m_LineVerts.Length;
         
            lineRenderer.SetPositions(m_LineVerts);
        }
    }
}
