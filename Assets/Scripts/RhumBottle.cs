using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhumBottle : MonoBehaviour {

    [SerializeField] protected float m_SpeedRotation = 25f;
    [SerializeField] protected int m_Score = 1;

    protected bool m_AlreadyHit;
    private Transform m_Transform;

    void Awake()
    {
        m_Transform = GetComponent<Transform>();
    }

    private void Start()
    {
        m_AlreadyHit = false;
    }

    public virtual void PlayerCollide()
    {
        if(!m_AlreadyHit)
        {
            ScoreScript.scoreValue += m_Score;
            m_AlreadyHit = true;
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        m_Transform.Rotate(Vector3.up * m_SpeedRotation * Time.fixedDeltaTime);
    }
}
