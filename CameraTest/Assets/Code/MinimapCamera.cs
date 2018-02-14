using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour {

    public GameObject m_goPlayer;
    public float m_fHeight = 30f;
	// Update is called once per frame
	void Update ()
    {
        transform.position = m_goPlayer.transform.position;
        float y = transform.position.y;
        y += m_fHeight;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
