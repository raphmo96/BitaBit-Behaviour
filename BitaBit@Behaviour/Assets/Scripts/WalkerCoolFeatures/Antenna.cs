using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antenna : MonoBehaviour
{
    private bool m_ShowDestination = false;

    private Vector3 m_Destination = new Vector3();
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

    }

}
