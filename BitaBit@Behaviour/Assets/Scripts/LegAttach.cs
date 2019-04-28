using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegAttach : MonoBehaviour
{
    [SerializeField]
    private Transform m_Anchor;
    [SerializeField]
    private GameObject m_Field;
    private Vector3 m_Scale = Vector3.one;

    private void Start()
    {
        m_Scale.x = m_Field.transform.localScale.x;
        m_Scale.y = m_Field.transform.localScale.y;
    }

    private void Update()
    {
        SetPosition();
        SetRotation();
        SetScale();
    }

    //Go in the center point between the player and the camera
    private void SetPosition()
    {
        m_Field.transform.position = 0.5f * (m_Anchor.position + transform.position);
    }

    //Ajust the scale in Z to take all the space between the camera and the player
    private void SetScale()
    {
        m_Scale.z = Vector3.Distance(transform.position, m_Anchor.position);
        m_Field.transform.localScale = m_Scale;
    }

    //Rotate to allign the forward to point at the player
    private void SetRotation()
    {
        m_Field.transform.LookAt(m_Anchor.position,transform.up);
    }
}