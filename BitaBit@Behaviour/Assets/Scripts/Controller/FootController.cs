using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FootController : MonoBehaviour
{
    private bool m_IsOnGround;
    private Rigidbody m_Rigidbody;
    
    public bool IsOnGround => m_IsOnGround;
    public Rigidbody Rigidbody => m_Rigidbody;
    
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
        //m_Anchors = m_ParentAnchor.GetComponentsInChildren<Transform>().ToList();
    }

    private void Update()
    {
        CheckGroundCollision();
    }

    private bool CheckGroundCollision()
    {
        if (Physics.CheckSphere(transform.position, transform.localScale.y+0.1f,LayerMask.GetMask("World")))
        {
            if (!m_IsOnGround)
            {
                Debug.Log("Touches");
                m_IsOnGround = true;
            }
        }
        else if(m_IsOnGround)
        {
            if (m_IsOnGround)
            {
                Debug.Log("Doesn't touch");
                m_IsOnGround = false;
            }
        }

        return m_IsOnGround;
    }
}
