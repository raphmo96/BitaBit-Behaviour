using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class SpideyController : MonoBehaviour
{
    private class Player
    {
        public SpiderController.EPlayer m_PlayerID;
        public SpiderController.ELeg m_Leg;
        public float m_HeightInput;
        public float m_DefaultAngle;
        public float m_Timer;

        public Player(SpiderController.EPlayer aPlayerID, SpiderController.ELeg aLegId)
        {
            m_PlayerID = aPlayerID;
            m_Leg = aLegId;
            m_HeightInput = 0f;
            m_Timer = 0f;
        }
    }

    [Serializable]
    public class LEG2
    {
        public Transform m_Base;
        public Quaternion m_Default;
    }

    [SerializeField] private float m_TurnPerSeconds=1;
    [SerializeField] private float m_OscillationMagnitude=30;
    [SerializeField] private List<LEG2> m_LegList;
    
    private Dictionary<SpiderController.EPlayer, Player> m_Players = new Dictionary<SpiderController.EPlayer, Player>();
    private float m_Timer;
    private Quaternion m_DefaultAngle;
    private Vector3 m_TargetRot;

    void Start()
    {
        m_Players[SpiderController.EPlayer.One] = new Player(SpiderController.EPlayer.One, SpiderController.ELeg.FrontRight);
        m_Players[SpiderController.EPlayer.Two] = new Player(SpiderController.EPlayer.One, SpiderController.ELeg.FrontLeft);
        m_Players[SpiderController.EPlayer.Three] = new Player(SpiderController.EPlayer.One, SpiderController.ELeg.BackRight);
        m_Players[SpiderController.EPlayer.Four] = new Player(SpiderController.EPlayer.One, SpiderController.ELeg.BackLeft);
        
        foreach (LEG2 Leg in m_LegList)
        {
            Leg.m_Default = Leg.m_Base.rotation;

        }
    }

    // Update is called once per frame
    void Update()
    {
        {
            if (ControllerManager.Instance.IsReady)
                foreach (SpiderController.EPlayer playerID in Enum.GetValues(typeof(SpiderController.EPlayer)))
                {
                    GetControllerInput(playerID);
                    UpdateLeg(playerID);
                }
        }
    }

    private void UpdateLeg(SpiderController.EPlayer aPlayer)
    {
        m_Players[aPlayer].m_Timer += Time.deltaTime * m_Players[aPlayer].m_HeightInput*(180/m_TurnPerSeconds);
        if (m_Players[aPlayer].m_Timer >= 180)
        {
            m_Players[aPlayer].m_Timer = 0;
        }

        m_TargetRot.y = Mathf.Sin(m_Players[aPlayer].m_Timer) * m_OscillationMagnitude - m_OscillationMagnitude / 2;
        m_TargetRot.z = Mathf.Cos(m_Players[aPlayer].m_Timer) * m_OscillationMagnitude - m_OscillationMagnitude / 2;

        m_LegList[(int)m_Players[aPlayer].m_Leg].m_Base.rotation = m_LegList[(int)m_Players[aPlayer].m_Leg].m_Default * Quaternion.Euler(m_TargetRot);
    }

    private void GetControllerInput(SpiderController.EPlayer aPlayer)
    {
        m_Players[aPlayer].m_HeightInput = ControllerManager.Instance.GetPlayerDevice(aPlayer).RightStickY;
    }
}
