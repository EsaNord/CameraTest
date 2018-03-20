using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float m_fDistance = 5f;
    public float m_fSpeed = 5f;
    public float m_fRotationSpeed = 10f;
    public float m_fGroundDist = 1.6f;
    public CharacterController m_tTarget;    

    private void Awake()
    {
        transform.position = m_tTarget.transform.position;
        transform.rotation = Quaternion.identity;
        transform.position -= m_tTarget.transform.forward * m_fDistance;
    }

    private void LateUpdate()
    {        
        UpdatePosition();        
    }

    private void UpdatePosition()
    {        
        Vector3 newPos = m_tTarget.transform.position - (m_tTarget.transform.forward * m_fDistance);        
        Quaternion slopeRotation = Quaternion.identity;
        RaycastHit hit;
        RaycastHit rayHit;

        if (Physics.SphereCast(m_tTarget.transform.position, m_tTarget.height / 2, -m_tTarget.transform.forward, out hit, m_fDistance)
            && transform.position.y > m_tTarget.transform.position.y)
        {
            if (Physics.Raycast(transform.position, -Vector3.up, out rayHit, 2f))
            {
                if(rayHit.transform.rotation != Quaternion.identity)
                {
                    slopeRotation.x = rayHit.transform.rotation.z;                   
                }
            }
            if (hit.transform.rotation != Quaternion.identity)
            {
                slopeRotation.x = hit.transform.rotation.z;                           
            }
            else
            {
                newPos = hit.point + m_tTarget.transform.forward * 0.2f;
            }            
        }

        if (Physics.Raycast(transform.position, -Vector3.up, out rayHit, 2f))
        {
            if (Vector3.Distance(rayHit.point, transform.position) < m_fGroundDist)
            {                
                newPos.y += (m_fGroundDist - Vector3.Distance(rayHit.point, transform.position));
            }
        }

        transform.position = Vector3.Slerp(transform.position, newPos, m_fSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, m_tTarget.transform.rotation * slopeRotation, m_fRotationSpeed * Time.deltaTime);        
    }
}