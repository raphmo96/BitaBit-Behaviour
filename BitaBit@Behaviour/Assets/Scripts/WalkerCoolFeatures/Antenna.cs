using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antenna : MonoBehaviour
{
    private bool m_ShowDestination = false;
    
    [SerializeField]
    private float m_RotSpeed = 1f;

    private void Update()
    {
        if(m_ShowDestination)
        {
            ShowDestination();
        }
        else
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, m_RotSpeed);
    }

    private void ShowDestination()
    {
        transform.LookAt(PlayerManager.Instance.m_Outposts[PlayerManager.Instance.GetOutpostIndex()].transform.position);
    }

    public void ActivateDestination()
    {
        m_ShowDestination = true;
    }
}
