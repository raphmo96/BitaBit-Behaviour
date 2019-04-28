using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody))]
public class FootController : MonoBehaviour
{
    
    public bool IsOnGround => m_IsOnGround;

    public bool IsOutOfBounds => m_IsOutOfBounds;

    public Rigidbody Rigidbody => m_Rigidbody;

    public bool IsLocked
    {
        get => m_IsLocked;
        set => m_IsLocked = value;
    }

    private bool m_IsOnGround;
    private bool m_IsOutOfBounds;
    private bool m_IsLocked;
    private float m_Reach;
    private float m_Height;
    private float m_ReachSqrd;

    private Vector3 m_TargetPos;
    private Vector3 m_Offset;
    private Transform m_Anchor;
    private Rigidbody m_Rigidbody;
    private Quaternion m_InitialAngle;


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_InitialAngle = transform.rotation;
    }

    public void Init(float aReach, float aHeight, Transform aAnchor, Vector3 aOffset)
    {
        m_Reach = aReach;
        m_Height = aHeight;
        m_Offset = aOffset+Vector3.down*0.5f;
        m_Anchor = aAnchor;
        m_ReachSqrd = m_Reach * m_Reach;
    }

    private void Update()
    {
        m_IsOnGround = CheckGroundCollision();
        m_IsOutOfBounds = CheckBoundsLimit();    
        
        transform.rotation = m_Anchor.rotation * m_InitialAngle;
          
        if (m_IsOutOfBounds)
        {
            Vector3 offset = m_Anchor.forward * m_Offset.z;
            offset += m_Anchor.right * m_Offset.x;
            offset += m_Anchor.up * m_Offset.y;
            
            m_Rigidbody.MovePosition((m_Anchor.position+offset) + m_TargetPos);
        }
    }

    private bool CheckBoundsLimit()
    {
        Vector3 offset = m_Anchor.forward * m_Offset.z;
        offset += m_Anchor.right * m_Offset.x;
        offset += m_Anchor.up * m_Offset.y;
        
        Vector3 delta = transform.position - (m_Anchor.position + offset);
            
        if (Vector3.SqrMagnitude(Vector3.Project(delta, m_Anchor.up)) > m_ReachSqrd && !m_IsLocked)
        {
            m_TargetPos = Vector3.zero;
            m_TargetPos.y = Mathf.Clamp(delta.y, -m_Reach, m_Reach);
            delta.y = 0;
            m_TargetPos += Vector3.ClampMagnitude(delta, m_Reach);
            return true;
        }

        return false;
    }

    private bool CheckGroundCollision()
    {
        return Physics.CheckSphere(transform.position, 0.5f,LayerMask.GetMask("World"));
    }
}
