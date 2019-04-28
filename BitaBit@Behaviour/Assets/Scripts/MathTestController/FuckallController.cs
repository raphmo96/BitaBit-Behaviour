﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckallController : MonoBehaviour
{
    public Rigidbody m_MainBody;

    public List<Rigidbody> m_Forearms = new List<Rigidbody>();
    public List<Transform> m_Knees = new List<Transform>();

    public float m_RotationForce = 100f;
    public float m_ElevationForce = 30f;

    public float m_TorqueToStabilize = 100f;
    public float m_ForceToStabilize = 100f;

    private void Update()
    {

        if(Input.GetKey(KeyCode.Q))
        {
            m_Forearms[0].AddForceAtPosition(-m_Forearms[0].transform.right * m_RotationForce * Time.deltaTime * 100, m_Forearms[0].transform.position - m_Forearms[0].transform.up * 0.5f);
            //m_Forearms[0].AddForce(-m_Forearms[0].transform.right * m_RotationForce * Time.deltaTime * 100);
        }
        if(Input.GetKey(KeyCode.W))
        {
            m_Forearms[0].AddForceAtPosition(m_Forearms[0].transform.right * m_RotationForce * Time.deltaTime * 100, m_Forearms[0].transform.position - m_Forearms[0].transform.up * 0.5f);

            //m_Forearms[0].AddForce(m_Forearms[0].transform.right * m_RotationForce * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_Forearms[0].AddForceAtPosition(m_Forearms[0].transform.forward * m_ElevationForce * Time.deltaTime * 100, m_Forearms[0].transform.position - m_Forearms[0].transform.up * 0.5f);
            RemoveSomeBodyWeightWhenStuckUnderBody(0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Forearms[0].AddForceAtPosition(-m_Forearms[0].transform.forward * m_ElevationForce * Time.deltaTime * 100, m_Forearms[0].transform.position - m_Forearms[0].transform.up * 0.5f);
            RemoveSomeBodyWeight(0);
        }

        /*
         * No more needed I think
         * 
        if (Input.GetKey(KeyCode.Z))
        {
            m_Forearms[0].AddForce(m_Forearms[0].transform.up * m_ElevationForce * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.X))
        {
            m_Forearms[0].AddForce(-m_Forearms[0].transform.up * m_ElevationForce * Time.deltaTime * 100);
        }
        */


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
            RemoveSomeBodyWeightWhenStuckUnderBody(1);
        }
        if (Input.GetKey(KeyCode.F))
        {
            m_Forearms[1].AddForce(-m_Forearms[1].transform.forward * m_ElevationForce * Time.deltaTime * 100);
            RemoveSomeBodyWeight(1);
        }


        if (Input.GetKey(KeyCode.T))
        {
            m_Forearms[2].AddForce(-m_Forearms[2].transform.right * m_RotationForce * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.Y))
        {
            m_Forearms[2].AddForce(m_Forearms[2].transform.right * m_RotationForce * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.G))
        {
            m_Forearms[2].AddForce(m_Forearms[2].transform.forward * m_ElevationForce * Time.deltaTime * 100);
            RemoveSomeBodyWeightWhenStuckUnderBody(2);
        }
        if (Input.GetKey(KeyCode.H))
        {
            m_Forearms[2].AddForce(-m_Forearms[2].transform.forward * m_ElevationForce * Time.deltaTime * 100);
            RemoveSomeBodyWeight(2);
        }


        if (Input.GetKey(KeyCode.U))
        {
            m_Forearms[3].AddForce(-m_Forearms[3].transform.right * m_RotationForce * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.I))
        {
            m_Forearms[3].AddForce(m_Forearms[3].transform.right * m_RotationForce * Time.deltaTime * 100);
        }
        if (Input.GetKey(KeyCode.J))
        {
            m_Forearms[3].AddForce(m_Forearms[3].transform.forward * m_ElevationForce * Time.deltaTime * 100);
            RemoveSomeBodyWeightWhenStuckUnderBody(3);
        }
        if (Input.GetKey(KeyCode.K))
        {
            m_Forearms[3].AddForce(-m_Forearms[3].transform.forward * m_ElevationForce * Time.deltaTime * 100);
            RemoveSomeBodyWeight(3);
        }

        if (true)
        {
            for(int i = 0; i < m_Forearms.Count; i++)
            {
                AddTorqueToBeUpward(i);
            }
        }

        if(Input.GetKey(KeyCode.Space))
        {
            m_Forearms[0].drag = 5f;
        }
        else
        {
            m_Forearms[0].drag = 10f;

        }
    }

    private void AddTorqueToBeUpward(int a_Leg)
    {
        float torque = -90 + Vector3.Angle(Vector3.up, m_Forearms[a_Leg].transform.right);

        if (torque >= 10f || torque <= -10f)
        {
            if(torque < 0f)
            {
                torque *= -1f * torque;
            }
            else
            {
                torque *= torque;
            }
            m_Forearms[a_Leg].AddTorque(m_Forearms[a_Leg].transform.forward * torque * m_TorqueToStabilize * Time.deltaTime, ForceMode.Force);
        }
    }

    private void RemoveSomeBodyWeight(int a_Leg)
    {
        if (m_Knees[a_Leg].transform.position.y - m_MainBody.transform.position.y >= 0.38f)
        {
            if (Physics.Raycast(m_Forearms[a_Leg].transform.position, -m_Forearms[a_Leg].transform.up, 1.25f, LayerMask.GetMask("Ground")))
            {
                m_MainBody.AddForce(m_MainBody.transform.up * m_ForceToStabilize * Time.deltaTime * 100f);
            }
        }
    }

    private void RemoveSomeBodyWeightWhenStuckUnderBody(int a_Leg)
    {
        if (m_Knees[a_Leg].transform.position.y - m_MainBody.transform.position.y <= 0.3f)
        {
            Debug.Log("Rise");
            if (Physics.Raycast(m_Forearms[a_Leg].transform.position, -m_Forearms[a_Leg].transform.up, 1.25f, LayerMask.GetMask("Ground")))
            {
                m_MainBody.AddForce(m_MainBody.transform.up * m_ForceToStabilize * Time.deltaTime * 60f);
            }

            m_Forearms[a_Leg].AddTorque(m_Forearms[a_Leg].transform.right* m_TorqueToStabilize * Time.deltaTime, ForceMode.Force);
        }
    }
}
