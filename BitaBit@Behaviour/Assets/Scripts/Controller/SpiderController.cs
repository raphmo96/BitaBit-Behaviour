using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class SpiderController : MonoBehaviour
{

    [Serializable]
    public struct Leg
    {
        public FootController m_Foot;
        public Transform m_AnchorPoint;
    }

    public class Player
    {
        public EPlayer m_PlayerID;
        public ELeg m_Leg;
        public float m_HeightInput;
        public float m_XInput;
        public float m_YInput;

        public Player(EPlayer aPlayerID, ELeg aLegId)
        {
            m_PlayerID = aPlayerID;
            m_Leg = aLegId;
            m_HeightInput = 0f;
            m_XInput = 0f;
            m_YInput = 0f;
        }
    }

    public enum ELeg
    {
        FrontRight,
        FrontLeft,
        BackRight,
        BackLeft,
    }

    public enum EPlayer
    {
        One,Two,Three,Four
    }

    [SerializeField] private float m_MaxDeviationAngle = 20f;
    [SerializeField] private float m_MaxReach = 3f;
    [SerializeField] private float m_MaxHeight = 1f;
    [SerializeField] private float m_MinHeight = -2f;
    [SerializeField] private float m_Force = 10;
    [SerializeField] private LegDictionary m_LegDictionary;
    [SerializeField] private Rigidbody m_Rigidbody;

    private Dictionary<EPlayer, Player> m_Players = new Dictionary<EPlayer, Player>();
    

    // Start is called before the first frame update
    void Start()
    {
        m_Players[EPlayer.One] = new Player(EPlayer.One,ELeg.FrontRight);
        m_Players[EPlayer.Two] = new Player(EPlayer.One,ELeg.FrontLeft);
        m_Players[EPlayer.Three] = new Player(EPlayer.One,ELeg.BackRight);
        m_Players[EPlayer.Four] = new Player(EPlayer.One,ELeg.BackLeft);
    }

    // Update is called once per frame
    void Update()
    {
        GetControllerInput(EPlayer.Two);
        UpdateLeg(EPlayer.Two);
        UpdateLeg(ELeg.FrontRight);
        UpdateLeg(ELeg.BackLeft);
        UpdateLeg(ELeg.BackRight);
    }

    private void GetControllerInput(EPlayer aPlayer)
    {
        m_Players[aPlayer].m_HeightInput = -Input.GetAxis("joystick 1 analog 5");
        m_Players[aPlayer].m_XInput = -Input.GetAxis("joystick 1 analog 0");
        m_Players[aPlayer].m_YInput = Input.GetAxis("joystick 1 analog 1");

        if (m_Players[aPlayer].m_HeightInput != 0)
        {
            Debug.Log($"Input Height:{m_Players[aPlayer].m_HeightInput}");
        }
        if (m_Players[aPlayer].m_XInput != 0)
        {
            Debug.Log($"Input X:{m_Players[aPlayer].m_XInput}");
        }
        if (m_Players[aPlayer].m_YInput != 0)
        {
            Debug.Log($"Input Y:{m_Players[aPlayer].m_YInput}");
        }
    }
    
    private void UpdateLeg(ELeg aLeg)
    {
        Player player = m_Players[EPlayer.Two];
        FootController foot = m_LegDictionary[aLeg].m_Foot;

        if (player.m_HeightInput != 0)
        {
            Vector3 ForcetoAdd = transform.up * player.m_HeightInput * m_Force;
            foot.Rigidbody.AddForce(ForcetoAdd);

            if (m_LegDictionary[aLeg].m_Foot.IsOnGround)
            {
                m_Rigidbody.AddForceAtPosition(
                    -ForcetoAdd,
                    m_LegDictionary[aLeg].m_AnchorPoint.transform.position);
            }
        }

        if (player.m_XInput != 0)
        {
            Vector3 ForcetoAdd = transform.forward * player.m_XInput * m_Force;
            foot.Rigidbody.AddForce(ForcetoAdd);

            if (m_LegDictionary[aLeg].m_Foot.IsOnGround)
            {
                m_Rigidbody.AddForceAtPosition(
                    -ForcetoAdd,
                    m_LegDictionary[aLeg].m_AnchorPoint.transform.position);
            }
        }
        
        if (player.m_YInput != 0)
        {
            Vector3 ForcetoAdd = transform.right * player.m_YInput * m_Force;
            foot.Rigidbody.AddForce(ForcetoAdd);

            if (m_LegDictionary[aLeg].m_Foot.IsOnGround)
            {
                m_Rigidbody.AddForceAtPosition(
                    -ForcetoAdd,
                    m_LegDictionary[aLeg].m_AnchorPoint.transform.position);
            }
        }
    }

    private void UpdateLeg(EPlayer aPlayer)
    {
        Player player = m_Players[aPlayer];
        FootController foot = m_LegDictionary[player.m_Leg].m_Foot;

        if (player.m_HeightInput != 0)
        {
            Vector3 ForcetoAdd = transform.up * player.m_HeightInput * m_Force;
            foot.Rigidbody.AddForce(ForcetoAdd);

            if (m_LegDictionary[player.m_Leg].m_Foot.IsOnGround)
            {
                m_Rigidbody.AddForceAtPosition(
                    -ForcetoAdd,
                    m_LegDictionary[player.m_Leg].m_AnchorPoint.transform.position);
            }
        }

        if (player.m_XInput != 0)
        {
            Vector3 ForcetoAdd = transform.forward * player.m_XInput * m_Force;
            foot.Rigidbody.AddForce(ForcetoAdd);

            if (m_LegDictionary[player.m_Leg].m_Foot.IsOnGround)
            {
                m_Rigidbody.AddForceAtPosition(
                    -ForcetoAdd,
                    m_LegDictionary[player.m_Leg].m_AnchorPoint.transform.position);
            }
        }
        
        if (player.m_YInput != 0)
        {
            Vector3 ForcetoAdd = transform.right * player.m_YInput * m_Force;
            foot.Rigidbody.AddForce(ForcetoAdd);

            if (m_LegDictionary[player.m_Leg].m_Foot.IsOnGround)
            {
                m_Rigidbody.AddForceAtPosition(
                    -ForcetoAdd,
                    m_LegDictionary[player.m_Leg].m_AnchorPoint.transform.position);
            }
        }
    }
}
