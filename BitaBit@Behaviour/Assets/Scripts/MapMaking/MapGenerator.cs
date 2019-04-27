using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Texture2D m_MapNoise;
    public int m_Width;

    public Mesh m_Mesh;
    public MeshFilter m_MeshFilter;
    public MeshRenderer m_MeshRenderer;

    private Vector3[] m_Vertices;
    private int[] m_Triangles;


    private void Start()
    {
        SetupMesh();

        GenerateMap();
    }

    private void SetupMesh()
    {
        m_Mesh = new Mesh();

        m_MeshFilter = gameObject.AddComponent<MeshFilter>();
        m_MeshFilter.mesh = m_Mesh;

        m_MeshRenderer = GetComponent<MeshRenderer>();

        m_Vertices = new Vector3[(m_Width + 1) * (m_Width + 1)];
        m_Triangles = new int[m_Width * m_Width * 6];
    }

    private void GenerateMap()
    {
        if (m_Width <= 0)
        {
            return;
        }

        GeneratePlane();
    }

    private void GeneratePlane()
    {
        SetVertices();
        SetTriangles();
        m_Mesh.RecalculateNormals();
    }

    private void ApplyNoiseToMesh()
    {

    }

    private void SetVertices()
    {
        int vertice = 0;

        //Set all the vertices positions.
        for (int y = 0; y <= m_Width; y++)
        {
            for (int x = 0; x <= m_Width; x++)
            {
                m_Vertices[vertice] = new Vector3(x, 0 , y);
                vertice++;
            }
        }

        //Apply the vertices to the mesh.
        m_Mesh.vertices = m_Vertices;
    }

    private void SetTriangles()
    {
        int vertice = 0;
        int tris = 0;

        //Generate squares for each height division
        for (int y = 0; y < m_Width; y++)
        {
            //Generate two triangle to form a square for each width division
            for (int x = 0; x < m_Width; x++)
            {
                //First triangle
                m_Triangles[tris] = vertice;
                m_Triangles[tris + 1] = vertice + m_Width + 1;
                m_Triangles[tris + 2] = vertice + 1;

                //Second triangle
                m_Triangles[tris + 3] = vertice + 1;
                m_Triangles[tris + 4] = vertice + m_Width + 1;
                m_Triangles[tris + 5] = vertice + m_Width + 1 + 1;

                tris += 6;
                vertice++;
            }
            vertice++;
        }

        //Apply the triangles to the mesh.
        m_Mesh.triangles = m_Triangles;
    }
}
