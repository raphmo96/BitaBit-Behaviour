﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotation : MonoBehaviour
{
    [SerializeField]
    private int m_Index = 1;
    private bool m_IsTurning = true;

    private void Update()
    {
        if(m_IsTurning)
        {
            transform.Rotate(Vector3.forward * m_Index);
        }
    }

    public void Activation()
    {
        m_IsTurning = true;
    }
}
