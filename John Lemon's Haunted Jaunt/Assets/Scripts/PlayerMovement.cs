﻿using System.Collections;
using System.Collections.Generic;
    
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public int speedModifier = 4;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    private PhotonView photonView;

    void Awake ()
    {
        photonView = GetComponent<PhotonView>();
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();

        if (photonView.IsMine)
            GetComponent<AudioListener>().enabled = true;
    }

    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        m_Animator.SetBool ("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }


        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
           transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));

        //Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        //m_Rotation = Quaternion.LookRotation (desiredForward);
    }

    void OnAnimatorMove ()
    {
        if (!photonView.IsMine)
            return;
        
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * Time.deltaTime * 8);
        //m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude * speedModifier);
        //m_Rigidbody.MoveRotation (m_Rotation);
    }
}