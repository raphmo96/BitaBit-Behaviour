using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotation : MonoBehaviour
{
    [SerializeField]
    private int m_Index;
    private bool m_IsTurning = false;

    private void Update()
    {
        if(m_IsTurning && GameManager.Instance.Player.Spend)
        {
            transform.Rotate(Vector3.forward * m_Index);
        }
    }

    public void Activation()
    {
        m_IsTurning = true;
    }
}
