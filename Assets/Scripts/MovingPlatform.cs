using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [SerializeField] private float m_Speed = 2;
    [SerializeField] private float m_TimeToWait = 3;
    [SerializeField] private Vector3 m_FinalPos;

    private Transform m_Transform;
    private Vector3 m_StartPos;
    private bool m_IsArrived;
    private float m_TimeWaited;

    private void Awake()
    {
        m_Transform = GetComponent<Transform>();
    }

    private void Start()
    {
        m_StartPos = m_Transform.localPosition;
        m_IsArrived = true;
        m_TimeWaited = m_TimeToWait;
        StartCoroutine(MovementCoroutine());
    }

    IEnumerator MovementCoroutine()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if(m_IsArrived)
            {
                m_TimeWaited -= Time.fixedDeltaTime;
                if (m_TimeWaited <= 0)
                {
                    m_IsArrived = false;
                    m_TimeWaited = m_TimeToWait;
                }
            }
            else
            {
                m_Transform.localPosition = Vector3.MoveTowards(m_Transform.localPosition, m_FinalPos, m_Speed * Time.fixedDeltaTime);
                if (m_Transform.localPosition == m_FinalPos)
                {
                    m_IsArrived = true;
                    Vector3 waitVector = m_StartPos;
                    m_StartPos = m_FinalPos;
                    m_FinalPos = waitVector;
                }
            }
        }
    }
}
