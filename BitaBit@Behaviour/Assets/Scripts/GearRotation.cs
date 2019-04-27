using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotation : MonoBehaviour
{

    private bool m_IsTurning = false;

    private void Update()
    {
        if(m_IsTurning)
        {
            transform.Rotate(Vector3.forward);
        }
    }

    public void Activation()
    {
        m_IsTurning = true;
    }
}
