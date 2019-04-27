using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Texture2D m_MapNoise;
    public int m_Width;

    public Mesh m_Mesh;
    public MeshFilter m_MeshFilter;
    public MeshRenderer m_MeshRenderer;


    private void Start()
    {
        SetupMesh();


    }

    private void SetupMesh()
    {
        m_Mesh = new Mesh();

        m_MeshFilter = gameObject.AddComponent<MeshFilter>();
        m_MeshFilter.mesh = m_Mesh;

        m_MeshRenderer = gameObject.AddComponent<MeshRenderer>();
    }

    private void GenerateMap()
    {
        if(m_MapNoise == null || m_Width <= 0)
        {
            return;
        }



    }
}
