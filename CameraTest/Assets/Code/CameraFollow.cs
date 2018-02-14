using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float m_fDistance = 5f;
    public float m_fSpeed = 5f;
    public float m_fRotationSpeed = 10f;
    public CharacterController m_tTarget;    

    private void Awake()
    {
        transform.position = m_tTarget.transform.position;
        transform.rotation = Quaternion.identity;
        transform.position -= transform.forward * m_fDistance;
    }

    private void Update()
    {        
        UpdatePosition();
    }

    private void UpdatePosition()
    {        
        Vector3 newPos = m_tTarget.transform.position - (transform.forward * m_fDistance);
        RaycastHit hit;        

        if (Physics.SphereCast(m_tTarget.transform.position, m_tTarget.height / 2, -transform.forward, out hit, m_fDistance))
        {                 
            newPos = hit.point + m_tTarget.transform.forward * 0.2f;            
        }        

        transform.position = Vector3.Slerp(transform.position, newPos, m_fSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, m_tTarget.transform.rotation, m_fRotationSpeed * Time.deltaTime);        
    }
}
