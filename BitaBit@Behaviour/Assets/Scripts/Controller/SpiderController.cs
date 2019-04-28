using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using InControl;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class SpiderController : MonoBehaviour
{

    [Serializable]
    public struct Leg
    {
        public FootController m_Foot;
        public Transform m_AnchorPoint;
        public Quaternion m_Quaternion;
    }

    private class Player
    {
        public bool m_Lock;
        public bool m_IsLocked;
        public float m_HeightInput;
        public float m_XInput;
        public float m_YInput;
        public EPlayer m_PlayerID;
        public ELeg m_Leg;
        public FixedJoint m_Joint;
        public Vector3 m_DeltaLockPos;

        public Player(EPlayer aPlayerID, ELeg aLegId)
        {
            m_PlayerID = aPlayerID;
            m_Leg = aLegId;
            m_HeightInput = 0f;
            m_XInput = 0f;
            m_YInput = 0f;
            m_Lock = false;
            m_IsLocked = false;
            m_DeltaLockPos = Vector3.zero;
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

    [SerializeField] private float m_MaxReach = 3f;
    [SerializeField] private float m_Height = 1f;
    [SerializeField] private float m_Force = 10;
    [SerializeField] private float m_LegSpeed = 2;
    [SerializeField] private LegDictionary m_LegDictionary;
    [SerializeField] private Rigidbody m_Rigidbody;

    private float m_SqrdMaxReach;
    private Dictionary<EPlayer, Player> m_Players = new Dictionary<EPlayer, Player>();
    
    private void Start()
    {
        m_SqrdMaxReach = m_MaxReach * m_MaxReach;
        m_Players[EPlayer.One] = new Player(EPlayer.One,ELeg.FrontRight);
        m_Players[EPlayer.Two] = new Player(EPlayer.One,ELeg.FrontLeft);
        m_Players[EPlayer.Three] = new Player(EPlayer.One,ELeg.BackRight);
        m_Players[EPlayer.Four] = new Player(EPlayer.One,ELeg.BackLeft);

        Vector3 offset;
        foreach (KeyValuePair<ELeg, Leg> Leg in m_LegDictionary)
        {
            offset = GetOffsetVector(Leg.Key);
            Leg.Value.m_Foot.Init(m_MaxReach, m_Height, Leg.Value.m_AnchorPoint, offset*m_MaxReach);
        }
    }

    private void Update()
    {
        if(ControllerManager.Instance.IsReady)
        foreach (EPlayer playerID in Enum.GetValues(typeof(EPlayer)))
        {
            GetControllerInput(playerID);
            UpdateLeg(playerID);
        }
    }

    private void GetControllerInput(EPlayer aPlayer)
    {
        m_Players[aPlayer].m_HeightInput = ControllerManager.Instance.GetPlayerDevice(aPlayer).RightStickY;
        m_Players[aPlayer].m_XInput = -ControllerManager.Instance.GetPlayerDevice(aPlayer).LeftStickX;
        m_Players[aPlayer].m_YInput = ControllerManager.Instance.GetPlayerDevice(aPlayer).LeftStickY;
        m_Players[aPlayer].m_Lock = ControllerManager.Instance.GetPlayerDevice(aPlayer).RightBumper;
    }

    private void UpdateLeg(EPlayer aPlayer)
    {
        Player player = m_Players[aPlayer];
        FootController foot = m_LegDictionary[player.m_Leg].m_Foot;

        float powerRatio = Mathf.Clamp(1-Vector3.SqrMagnitude(foot.Rigidbody.position - m_LegDictionary[player.m_Leg].m_AnchorPoint.position)/m_SqrdMaxReach,0.4f,1f);
        float lifeSpent = 0;

        if (!player.m_Lock)
        {
            if (player.m_IsLocked)
            {
                m_LegDictionary[player.m_Leg].m_Foot.Rigidbody.isKinematic = false;
                player.m_IsLocked = false;
                m_LegDictionary[player.m_Leg].m_Foot.IsLocked = false;
            }

            if (player.m_HeightInput != 0)
            {
                lifeSpent += player.m_HeightInput;
                
                Vector3 ForcetoAdd = player.m_HeightInput * m_Rigidbody.transform.up * m_Force * (powerRatio);
                foot.Rigidbody.velocity += player.m_HeightInput * transform.up*m_LegSpeed;

                if (m_LegDictionary[player.m_Leg].m_Foot.IsOnGround && !m_LegDictionary[player.m_Leg].m_Foot.IsOutOfBounds)
                {
                    m_Rigidbody.AddForceAtPosition(
                        ForcetoAdd.y < 0 ? -ForcetoAdd*2 : -ForcetoAdd*0.1f,
                        m_LegDictionary[player.m_Leg].m_AnchorPoint.transform.position);
                    m_Rigidbody.AddForce(ForcetoAdd.y < 0 ? -ForcetoAdd*2 : Vector3.zero);
                }
            }

            if (player.m_XInput != 0)
            {
                lifeSpent += player.m_XInput;

                
                Vector3 ForcetoAdd = player.m_XInput * -m_Rigidbody.transform.right;

                if (m_LegDictionary[player.m_Leg].m_Foot.IsOnGround&& !m_LegDictionary[player.m_Leg].m_Foot.IsOutOfBounds)
                {
                    m_Rigidbody.AddForceAtPosition(
                        -ForcetoAdd * m_Force * (powerRatio)*0.2f,
                        m_LegDictionary[player.m_Leg].m_AnchorPoint.transform.position);
                    m_Rigidbody.AddForce(-ForcetoAdd * m_Force * powerRatio*4f);
                }
                else
                {
                    foot.Rigidbody.velocity += ForcetoAdd * m_LegSpeed;
                }
            }

            if (player.m_YInput != 0)
            {
                lifeSpent += player.m_YInput;
                
                Vector3 ForcetoAdd = player.m_YInput * m_Rigidbody.transform.forward;

                if (m_LegDictionary[player.m_Leg].m_Foot.IsOnGround&& !m_LegDictionary[player.m_Leg].m_Foot.IsOutOfBounds)
                {
                    m_Rigidbody.AddForceAtPosition(
                        -ForcetoAdd * m_Force * (powerRatio) *0.2f,
                        m_LegDictionary[player.m_Leg].m_AnchorPoint.transform.position);
                    m_Rigidbody.AddForce(-ForcetoAdd * m_Force * (powerRatio)*4f);
                }
                else
                {
                    foot.Rigidbody.velocity += ForcetoAdd * m_LegSpeed;
                }
            }
            
            GameManager.Instance.Player.LoseLife(Mathf.Abs(lifeSpent)*0.001f);
        }
        else
        {
            if (!player.m_IsLocked)
            {
                m_LegDictionary[player.m_Leg].m_Foot.Rigidbody.isKinematic = true;
                player.m_DeltaLockPos = m_LegDictionary[player.m_Leg].m_AnchorPoint.position - m_LegDictionary[player.m_Leg].m_Foot.transform.position;
                player.m_IsLocked = true;
                m_LegDictionary[player.m_Leg].m_Foot.IsLocked = true;
            }

            Vector3 LockForce = (player.m_DeltaLockPos + m_LegDictionary[player.m_Leg].m_Foot.transform.position) - m_LegDictionary[player.m_Leg].m_AnchorPoint.position;

            if (m_LegDictionary[player.m_Leg].m_Foot.IsOnGround)
            {
                m_Rigidbody.AddForceAtPosition(
                    LockForce * 4f,
                    m_LegDictionary[player.m_Leg].m_AnchorPoint.transform.position);
            }
            else
            {
                foot.Rigidbody.MovePosition(m_LegDictionary[player.m_Leg].m_AnchorPoint.transform.position+player.m_DeltaLockPos);
            }          
        }
    }  
    
    private Vector3 GetOffsetVector(ELeg aLeg)
    {
        switch (aLeg)
        {
            case ELeg.FrontRight:
                return Vector3.forward + Vector3.right;
            case ELeg.FrontLeft:
                return Vector3.forward + Vector3.left;
            case ELeg.BackRight:
                return Vector3.back + Vector3.right;
            case ELeg.BackLeft:
                return Vector3.back + Vector3.left;
            default:
                return Vector3.zero;
        }
    }
}
