using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMover : MonoBehaviour
{
    private Renderer m_Renderer;
    private float m_Offset;
    [SerializeField]
    private float m_ScrollSpeed = 0.015f;

    private Vector2 m_ScrollPos = new Vector2();
    

    private void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }


    private void Update()
    {
        m_Offset = Time.time * m_ScrollSpeed;
        m_ScrollPos.x = m_Offset;
        m_Renderer.material.SetTextureOffset("_MainTex", m_ScrollPos);
    }
}
