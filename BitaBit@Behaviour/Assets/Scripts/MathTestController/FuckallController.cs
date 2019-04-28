using System.Collections;
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
        // PLAYER 0

        if (ControllerManager.Instance.GetPlayerDevice(0).RightStickX > 0.1f)
        {
            m_Forearms[0].AddForceAtPosition(-m_Forearms[0].transform.right * m_RotationForce * Time.deltaTime * 100, m_Forearms[0].transform.position - m_Forearms[0].transform.up * 0.5f);
            if(LegIsGrounded(0))
            {
                m_Forearms[0].AddForceAtPosition(m_Forearms[0].transform.up * m_RotationForce * Time.deltaTime * 50, m_Forearms[0].transform.position - m_Forearms[0].transform.up * 0.5f);
            }
            //m_Forearms[0].AddForce(-m_Forearms[0].transform.right * m_RotationForce * Time.deltaTime * 100);
        }
        if(ControllerManager.Instance.GetPlayerDevice(0).RightStickX < -0.1f)
        {
            m_Forearms[0].AddForceAtPosition(m_Forearms[0].transform.right * m_RotationForce * Time.deltaTime * 100, m_Forearms[0].transform.position - m_Forearms[0].transform.up * 0.5f);
            if (LegIsGrounded(0))
            {
                m_Forearms[0].AddForceAtPosition(m_Forearms[0].transform.up * m_RotationForce * Time.deltaTime * 50, m_Forearms[0].transform.position - m_Forearms[0].transform.up * 0.5f);
            }
            //m_Forearms[0].AddForce(m_Forearms[0].transform.right * m_RotationForce * Time.deltaTime * 100);
        }
        if (ControllerManager.Instance.GetPlayerDevice(0).RightStickY > 0.1f)
        {
            m_Forearms[0].AddForceAtPosition(m_Forearms[0].transform.forward * m_ElevationForce * Time.deltaTime * 100, m_Forearms[0].transform.position - m_Forearms[0].transform.up * 0.5f);
            RemoveSomeBodyWeightWhenStuckUnderBody(0);
        }
        if (ControllerManager.Instance.GetPlayerDevice(0).RightStickY < -0.1f)
        {
            m_Forearms[0].AddForceAtPosition(-m_Forearms[0].transform.forward * m_ElevationForce * Time.deltaTime * 100, m_Forearms[0].transform.position - m_Forearms[0].transform.up * 0.5f);
            RemoveSomeBodyWeight(0);
        }

        // PLAYER 1

        if (ControllerManager.Instance.GetPlayerDevice(SpiderController.EPlayer.Two).RightStickX > 0.1f)
        {
            m_Forearms[1].AddForce(m_Forearms[1].transform.right * m_RotationForce * Time.deltaTime * 100);
            if (LegIsGrounded(1))
            {
                m_Forearms[1].AddForceAtPosition(m_Forearms[1].transform.up * m_RotationForce * Time.deltaTime * 50, m_Forearms[1].transform.position - m_Forearms[1].transform.up * 0.5f);
            }

        }
        if (ControllerManager.Instance.GetPlayerDevice(SpiderController.EPlayer.Two).RightStickX < -0.1f)
        {
            m_Forearms[1].AddForce(-m_Forearms[1].transform.right * m_RotationForce * Time.deltaTime * 100);
            if (LegIsGrounded(1))
            {
                m_Forearms[1].AddForceAtPosition(m_Forearms[1].transform.up * m_RotationForce * Time.deltaTime * 50, m_Forearms[1].transform.position - m_Forearms[1].transform.up * 0.5f);
            }
        }
        if (ControllerManager.Instance.GetPlayerDevice(SpiderController.EPlayer.Two).RightStickY > 0.1f)
        {
            m_Forearms[1].AddForce(m_Forearms[1].transform.forward * m_ElevationForce * Time.deltaTime * 100);
            RemoveSomeBodyWeightWhenStuckUnderBody(1);
        }
        if (ControllerManager.Instance.GetPlayerDevice(SpiderController.EPlayer.Two).RightStickY < -0.1f)
        {
            m_Forearms[1].AddForce(-m_Forearms[1].transform.forward * m_ElevationForce * Time.deltaTime * 100);
            RemoveSomeBodyWeight(1);
        }

        // PLAYER 2

        if (ControllerManager.Instance.GetPlayerDevice(SpiderController.EPlayer.Three).RightStickX > 0.1f)
        {
            m_Forearms[2].AddForce(-m_Forearms[2].transform.right * m_RotationForce * Time.deltaTime * 100);
            if (LegIsGrounded(2))
            {
                m_Forearms[2].AddForceAtPosition(m_Forearms[2].transform.up * m_RotationForce * Time.deltaTime * 50, m_Forearms[2].transform.position - m_Forearms[2].transform.up * 0.5f);
            }
        }
        if (ControllerManager.Instance.GetPlayerDevice(SpiderController.EPlayer.Three).RightStickX < -0.1f)
        {
            m_Forearms[2].AddForce(m_Forearms[2].transform.right * m_RotationForce * Time.deltaTime * 100);
            if (LegIsGrounded(2))
            {
                m_Forearms[2].AddForceAtPosition(m_Forearms[2].transform.up * m_RotationForce * Time.deltaTime * 50, m_Forearms[2].transform.position - m_Forearms[2].transform.up * 0.5f);
            }
        }
        if (ControllerManager.Instance.GetPlayerDevice(SpiderController.EPlayer.Three).RightStickY > 0.1f)
        {
            m_Forearms[2].AddForce(m_Forearms[2].transform.forward * m_ElevationForce * Time.deltaTime * 100);
            RemoveSomeBodyWeightWhenStuckUnderBody(2);
        }
        if (ControllerManager.Instance.GetPlayerDevice(SpiderController.EPlayer.Three).RightStickY < -0.1f)
        {
            m_Forearms[2].AddForce(-m_Forearms[2].transform.forward * m_ElevationForce * Time.deltaTime * 100);
            RemoveSomeBodyWeight(2);
        }

        // PLAYER 3

        if (ControllerManager.Instance.GetPlayerDevice(SpiderController.EPlayer.Four).RightStickX > 0.1f)
        {
            m_Forearms[3].AddForce(m_Forearms[3].transform.right * m_RotationForce * Time.deltaTime * 100);
            if (LegIsGrounded(3))
            {
                m_Forearms[3].AddForceAtPosition(m_Forearms[3].transform.up * m_RotationForce * Time.deltaTime * 50, m_Forearms[3].transform.position - m_Forearms[3].transform.up * 0.5f);
            }
        }
        if (ControllerManager.Instance.GetPlayerDevice(SpiderController.EPlayer.Four).RightStickX < -0.1f)
        {
            m_Forearms[3].AddForce(-m_Forearms[3].transform.right * m_RotationForce * Time.deltaTime * 100);
            if (LegIsGrounded(3))
            {
                m_Forearms[3].AddForceAtPosition(m_Forearms[3].transform.up * m_RotationForce * Time.deltaTime * 50, m_Forearms[3].transform.position - m_Forearms[3].transform.up * 0.5f);
            }
        }
        if (ControllerManager.Instance.GetPlayerDevice(SpiderController.EPlayer.Four).RightStickY > 0.1f)
        {
            m_Forearms[3].AddForce(m_Forearms[3].transform.forward * m_ElevationForce * Time.deltaTime * 100);
            RemoveSomeBodyWeightWhenStuckUnderBody(3);
        }
        if (ControllerManager.Instance.GetPlayerDevice(SpiderController.EPlayer.Four).RightStickY < -0.1f)
        {
            m_Forearms[3].AddForce(-m_Forearms[3].transform.forward * m_ElevationForce * Time.deltaTime * 100);
            RemoveSomeBodyWeight(3);
        }

        //Stabilization

        for(int i = 0; i < m_Forearms.Count; i++)
        {
           AddTorqueToBeUpward(i);
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

            m_Forearms[a_Leg].AddTorque(m_Forearms[a_Leg].transform.right* m_TorqueToStabilize * Time.deltaTime *100f, ForceMode.Force);
        }
    }

    private bool LegIsGrounded(int a_Leg)
    {
        return Physics.Raycast(m_Forearms[a_Leg].transform.position, -m_Forearms[a_Leg].transform.up, 1.25f, LayerMask.GetMask("Ground"));
    }

}
