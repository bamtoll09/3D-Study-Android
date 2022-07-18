using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementDirection { LEFT, UP, DOWN, RIGHT }
public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;

    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    int m_Horizontal;
    int m_Vertical;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = m_Horizontal;
        float vertical = m_Vertical;

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;

        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }
        
        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }

    public void Goto(MovementDirection md)
    {
        m_Horizontal = 0;
        m_Vertical = 0;

        switch (md)
        {
            case MovementDirection.LEFT: m_Horizontal = -1; break;
            case MovementDirection.RIGHT: m_Horizontal = 1; break;
            case MovementDirection.UP: m_Vertical = 1; break;
            case MovementDirection.DOWN: m_Vertical = -1; break;
        }
    }

    public void Stop()
    {
        m_Horizontal = 0;
        m_Vertical = 0;
    }
}
