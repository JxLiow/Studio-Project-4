using System.Collections;
using System.Collections.Generic;
    
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    private PhotonView photonView;

    public float dashCooldown;
    public float dashBoost;
    void Awake ()
    {
        photonView = GetComponent<PhotonView>();
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();

        if (photonView.IsMine)
            GetComponent<AudioListener>().enabled = true;
    }

    void Update ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //attack animation
        if (m_Animator.GetBool("IsAttacking") == true)
        {
            if (Physics.Raycast(_ray, out _hit))
                transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
            return;
        }
        else if(m_Animator.GetBool("IsAttacking") == false)
        {
            m_Animator.SetBool("IsWalking", isWalking);
        }


        //footstep audio
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

        //dashing animation
        if (Input.GetKey(KeyCode.Space) && photonView.IsMine && dashCooldown <= 0.0f)
        {
            Dashing();
            dashCooldown = 1.5f;
        }

        if (Input.GetMouseButtonDown(0) && photonView.IsMine)
        {
            if (Physics.Raycast(_ray, out _hit))
            {
                transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
            }

        }
        if (dashCooldown > 0.0f)
            dashCooldown -= Time.deltaTime;
        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);

    }

    void OnAnimatorMove ()
    {
        if (!photonView.IsMine)
            return;

        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * Time.deltaTime * 8);
        //m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude * speedModifier);
        m_Rigidbody.MoveRotation (m_Rotation); 
    }

    void Dashing()
    {
        StartCoroutine(DashAnimation());
        Dash();
    }
  
    IEnumerator DashAnimation()
    {
        m_Animator.SetBool("IsDashing", true);
        yield return new WaitForSeconds(1.25f);
        m_Animator.SetBool("IsDashing", false);
    }
    void Dash()
    {
        m_Rigidbody.AddForce(m_Movement * 10, ForceMode.Impulse);
    }
}