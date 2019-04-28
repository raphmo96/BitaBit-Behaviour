using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShittyController : MonoBehaviour
{
    [SerializeField]
    private float m_MoveSpeed = 5f;

    [SerializeField]
    private float m_RotSpeed = 5f;

    private void Update()
    {
        Debug.Log(Input.GetAxis("Horizontal"));
        if(Input.GetKey(KeyCode.LeftArrow)) 
        {
            Rotate(-1);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            Rotate(1);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move();
        }
    }

    private void Rotate(int multiplier)
    {
        transform.Rotate(Vector3.up * multiplier, m_RotSpeed);
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * m_MoveSpeed);
    }
}
