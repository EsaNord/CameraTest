using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    public float m_fJumpSpeed = 8f;
    public float m_fMoveSpeed = 6f;
    public float m_fRotationSpeed = 5f;
    public float m_fGravity = 20f;

    private CharacterController charCont;
    private Vector3 m_vMovement;

    private void Start()
    {
        charCont = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (charCont.isGrounded)
        {
            m_vMovement = new Vector3(0, 0, Input.GetAxis("Vertical"));
            m_vMovement = transform.TransformDirection(m_vMovement);
            m_vMovement *= m_fMoveSpeed;

            if (Input.GetButton("Jump"))
            {
                m_vMovement.y = m_fJumpSpeed;
            }            
        }

        m_vMovement.y -= m_fGravity * Time.deltaTime;
        charCont.Move(m_vMovement * Time.deltaTime);

        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * m_fRotationSpeed * Time.deltaTime);
    }
}