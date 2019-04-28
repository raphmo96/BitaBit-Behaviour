using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckallController : MonoBehaviour
{
    public List<Rigidbody> m_Forearms = new List<Rigidbody>();
    public float m_RotationForce = 100f;
    public float m_ElevationForce = 30f;

    private void Update()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            m_Forearms[0].AddForce(-m_Forearms[0].transform.right * m_RotationForce * Time.deltaTime * 100);
        }
        if(Input.GetKey(KeyCode.W))
        {
            m_Forearms[0].AddForce(m_Forearms[0].transform.right * m_RotationForce * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_Forearms[0].AddForce(m_Forearms[0].transform.forward * m_ElevationForce * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Forearms[0].AddForce(-m_Forearms[0].transform.forward * m_ElevationForce * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            m_Forearms[0].AddForce(m_Forearms[0].transform.up * m_ElevationForce * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.X))
        {
            m_Forearms[0].AddForce(-m_Forearms[0].transform.up * m_ElevationForce * Time.deltaTime * 100);
        }


        if (Input.GetKey(KeyCode.E))
        {
            m_Forearms[1].AddForce(-m_Forearms[1].transform.right * m_RotationForce * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.R))
        {
            m_Forearms[1].AddForce(m_Forearms[1].transform.right * m_RotationForce * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_Forearms[1].AddForce(m_Forearms[1].transform.forward * m_ElevationForce * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.F))
        {
            m_Forearms[1].AddForce(-m_Forearms[1].transform.forward * m_ElevationForce * Time.deltaTime * 100);
        }
    }
}
