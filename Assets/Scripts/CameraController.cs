using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Transform m_Target;
    [SerializeField] private float m_RotationSpeed;

    private Vector3 m_OldCoordinates;
    private Transform m_Transform;
    private  int m_CameraDirection=1;
    private float m_CameraSensitivity = 1;

    public void ChangeDirection()
    {
        m_CameraDirection= m_CameraDirection * (-1);
        PlayerPrefs.SetInt("Camera_Direction", m_CameraDirection);
    }
    public void ChangeSensitivity(float Sensitivity)
    {
        m_CameraSensitivity = Sensitivity;
        PlayerPrefs.SetFloat("Camera_Sensitivity", m_CameraSensitivity);
    }

    void Start () {
        m_OldCoordinates = m_Target.position;
        m_Transform = GetComponent<Transform>();
    }
	
	void Update () {

        m_CameraDirection = PlayerPrefs.GetInt("Camera_Direction", m_CameraDirection);
        m_CameraSensitivity = PlayerPrefs.GetFloat("Camera_Sensitivity", m_CameraSensitivity);
        m_Transform.position += m_Target.position - m_OldCoordinates;
        m_OldCoordinates = m_Target.position;

        float rotateX = Input.GetAxis("Camera X") * Time.deltaTime * m_RotationSpeed * m_CameraDirection *  m_CameraSensitivity;
        float rotateY = Input.GetAxis("Camera Y") * Time.deltaTime * m_RotationSpeed * m_CameraDirection * m_CameraSensitivity;
        m_Transform.RotateAround(m_Target.position, Vector3.up, rotateX);
        m_Transform.RotateAround(m_Target.position, m_Transform.right, rotateY);
    }
}
