using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

	[SerializeField] private float m_MaxAngle;
    [SerializeField] private Transform m_PlayerCamera;
    [SerializeField] private Transform m_Player;

    [SerializeField] private float m_QuaternionSlerpCoef = 6f;

    private Rigidbody m_Rigidbody;

    private Quaternion m_InitOrientation;
    private Quaternion m_TargetOrientation;

    private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    IEnumerator Start () {
		m_InitOrientation = m_Rigidbody.rotation;
        yield break;
	}

	// Update is called once per frame
	void FixedUpdate () {

        float rotateX = m_MaxAngle * Input.GetAxis("Vertical");
        float rotateZ = -m_MaxAngle * Input.GetAxis("Horizontal");

		m_TargetOrientation = Quaternion.AngleAxis(rotateX, m_PlayerCamera.right) * Quaternion.AngleAxis(rotateZ, m_PlayerCamera.forward) * m_InitOrientation;

		Quaternion nextOrientation = Quaternion.Slerp(m_Rigidbody.rotation, m_TargetOrientation,Time.fixedDeltaTime* m_QuaternionSlerpCoef);
        m_Rigidbody.MoveRotation(nextOrientation);
    }
}
