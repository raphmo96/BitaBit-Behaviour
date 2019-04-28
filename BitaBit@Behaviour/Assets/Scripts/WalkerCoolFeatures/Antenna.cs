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
        if (m_ShowDestination)
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
        if (GameManager.Instance.Player.GetOutpostIndex() + 1 > GameManager.Instance.Player.m_Outposts.Count)
        {
            transform.LookAt(GameManager.Instance.Player.m_Outposts[GameManager.Instance.Player.GetOutpostIndex()].transform.position);

        }
    }

    public void ActivateDestination()
    {
        m_ShowDestination = true;
    }
}
