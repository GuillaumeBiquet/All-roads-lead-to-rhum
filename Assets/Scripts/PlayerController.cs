using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SDD.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_Speed = 10;
    [SerializeField] private float m_MaxSpeed = 100;
    [SerializeField] private float m_StartRunningSpeed = 10;
    [SerializeField] private int m_CollisionPrecision = 5;

    private Transform m_Transform;
    private Transform m_Platform;
    private Vector3 m_Velocity;
    private bool m_IsGrounded;
    private Vector3 m_ObjectDemiSize;
    private Animator m_Animation;
    
    private AudioSource m_DrinkSound;

    void Awake()
    {
        m_Transform = GetComponent<Transform>();
        m_Animation = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        m_IsGrounded = false;
        m_ObjectDemiSize = new Vector3(GetComponentInChildren<Collider>().bounds.size.x / 2, GetComponentInChildren<Collider>().bounds.size.y / 2, GetComponentInChildren<Collider>().bounds.size.z / 2);
        m_DrinkSound = GetComponentInChildren<AudioSource>();
    }

    void FixedUpdate()
    {
        m_Velocity += Physics.gravity * Time.fixedDeltaTime;
        if (m_IsGrounded)
        {
            m_Velocity += Time.fixedDeltaTime * new Vector3(-m_Platform.rotation.z * 10, 0, m_Platform.rotation.x * 10) * m_Speed;
            if (m_Velocity.sqrMagnitude > m_MaxSpeed)
            {
                m_Velocity = Vector3.ClampMagnitude(m_Velocity, m_MaxSpeed);
            }
            if (Input.GetAxis("Vertical") == 0f && Input.GetAxis("Horizontal") == 0f)
            {
                m_Velocity.x *= 0.85f;
                m_Velocity.z *= 0.85f;
            }
        }

        Vector3 move = m_Velocity * Time.fixedDeltaTime;
        Movement(move);

        if (m_Platform != null)
        {
            if (Mathf.Sign(-m_Platform.rotation.z) != Mathf.Sign(m_Velocity.x))
            {
                m_Velocity.x *= 0.85f;
            }
            if (Mathf.Sign(m_Platform.rotation.x) != Mathf.Sign(m_Velocity.z))
            {
                m_Velocity.z *= 0.85f;
            }
            Vector3 platformDirection = new Vector3(-m_Platform.rotation.z, 0, m_Platform.rotation.x);
            if (platformDirection != Vector3.zero)
            {
                m_Transform.rotation = Quaternion.LookRotation(platformDirection);
            }
        }
        Animate();
    }

    private void Animate()
    {
        bool grounded = Physics.Raycast(m_Transform.position, Vector3.down, 2f);
        if(grounded)
        {
            m_Animation.SetBool("isFalling", false);
        }
        else
        {
            m_Animation.SetBool("isFalling", true);
        }

        float rotationSpeed = new Vector3(m_Velocity.x, 0, m_Velocity.z).magnitude;
        m_Transform.Find("barrel").Rotate(0, -rotationSpeed / 2, 0);
        if (rotationSpeed > 0.5 && rotationSpeed < m_StartRunningSpeed)
        {
            m_Animation.SetBool("isWalking", true);
            m_Animation.SetBool("isRunning", false);
        }
        else if (rotationSpeed >= m_StartRunningSpeed)
        {
            m_Animation.SetBool("isRunning", true);
            m_Animation.SetBool("isWalking", false);
        }
        else if (rotationSpeed < 0.5)
        {
            m_Animation.SetBool("isWalking", false);
            m_Animation.SetBool("isRunning", false);
        }
    }

    private void Movement(Vector3 move)
    {
        bool collisionHappened = false;
        CheckYCollision(ref move, ref collisionHappened);
        CheckCollisionAround(ref move, ref collisionHappened);
        if (!collisionHappened)
        {
            m_IsGrounded = false;
        }
        m_Transform.position += move;
    }

    private void CheckCollisionAround(ref Vector3 move, ref bool collisionHappened)
    {
        RaycastHit hitInfos;
        for (int i = 0; i < 360; i += m_CollisionPrecision)
        {
            Vector3 testDirection = Quaternion.Euler(0, i, 0) * new Vector3(0, 0, m_ObjectDemiSize.x);
            bool collision = Physics.Raycast(m_Transform.position, move + testDirection, out hitInfos, (move + testDirection).magnitude);
            if (collision)
            {
                if (!hitInfos.collider.isTrigger)
                {
                    if (i > 315 || i <= 45)
                    {
                        move.z = Mathf.Min(0, move.z);
                        m_Velocity.z = Mathf.Min(0, m_Velocity.z);
                    }
                    else if (i > 45 && i <= 135)
                    {
                        move.x = Mathf.Min(0, move.x);
                        m_Velocity.x = Mathf.Min(0, m_Velocity.x);
                    }
                    else if (i > 135 && i <= 225)
                    {
                        move.z = Mathf.Max(0, move.z);
                        m_Velocity.z = Mathf.Max(0, m_Velocity.z);
                    }
                    else if (i > 225 && i <= 315)
                    {
                        move.x = Mathf.Max(0, move.x);
                        m_Velocity.x = Mathf.Max(0, m_Velocity.x);
                    }
                    m_Transform.position = hitInfos.point - testDirection;
                    collisionHappened = true;
                }
                else if (hitInfos.collider.isTrigger && hitInfos.collider.CompareTag("Bottle"))
                {
                    hitInfos.collider.GetComponent<RhumBottle>().PlayerCollide();
                    m_DrinkSound.Play();
                }
                else if (hitInfos.collider.isTrigger && hitInfos.collider.CompareTag("Sea"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                else if (hitInfos.collider.isTrigger && hitInfos.collider.CompareTag("Flag") && hitInfos.collider.GetComponentInParent<Flag>().VictoryOK)
                {
                    EventManager.Instance.Raise(new VictoryObtainedEvent());
                }
            }
        }

    }

    private void CheckYCollision(ref Vector3 move, ref bool collisionHappened)
    {
        RaycastHit hitInfos;
        Vector3 testDirection = new Vector3(0, m_ObjectDemiSize.y);
        bool minusYCollision = Physics.Raycast(m_Transform.position, move - testDirection, out hitInfos, (move - testDirection).magnitude);
        if (minusYCollision && !hitInfos.collider.isTrigger)
        {
            move.y = 0;
            m_Velocity.y = 0;
            if (m_Platform != hitInfos.collider.transform.parent)
            {
                m_Platform = hitInfos.collider.transform.parent;
                m_Transform.parent = m_Platform;
            }
            m_IsGrounded = true;
            m_Transform.position = hitInfos.point + new Vector3(0, m_ObjectDemiSize.y);
            collisionHappened = true;
            return;
        }
    }
}

