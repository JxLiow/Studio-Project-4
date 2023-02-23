using System.Collections;
using System.Collections.Generic;
    
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    PlayerAction playerAction;

    Animator m_Animator;
    public Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    public Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    private PhotonView photonView;

    public float dashCooldown;
    public float dashBoost;

    SpeedUpScript speedUpScript;
    public float speedModifier = 8;
    float speedUpDuration = 5;
    public bool slowed = false;
    void Awake ()
    {
        photonView = GetComponent<PhotonView>();
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();



        speedUpScript = FindObjectOfType<SpeedUpScript>();
        playerAction = FindObjectOfType<PlayerAction>();

        if (photonView.IsMine)
            GetComponent<AudioListener>().enabled = true;
        transform.position = JLGameManager.spawnPositions[photonView.Owner.ActorNumber - 1];
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

        if(m_Animator.GetBool("IsDashing") == true)
        {
            m_Animator.SetBool("IsWalking", false);
            m_Animator.SetBool("IsAttacking", false);
        }

        //attack at mouse position
        if (Input.GetMouseButtonDown(0) && photonView.IsMine)
        {
            if (Physics.Raycast(_ray, out _hit))
            {
                transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
            }

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

        //movement
        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);


        if ((playerAction.godName == "Poseidon") && (photonView.IsMine)) //slow dont affect poseidon
        {
            speedModifier = 8;
        }


        //speedup timer
        if (speedModifier != 8)
        {
            if(speedUpDuration > 0)
            {
                speedUpDuration -= Time.deltaTime;
            }
            else if(speedUpDuration <= 0)
            {
                speedUpDuration = 5;
                speedModifier = 8;
            }
        }

        //dashing
        if (Input.GetKey(KeyCode.Space) && photonView.IsMine && dashCooldown <= 0.0f)
        {
            Dashing();
            dashCooldown = 1.5f;
        }

        //dash cooldown
        if (dashCooldown > 0.0f)
            dashCooldown -= Time.deltaTime;



    }

    void OnAnimatorMove ()
    {
        if (!photonView.IsMine)
            return;
        
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * Time.deltaTime * speedModifier);
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
        GetComponent<Rigidbody>().AddForce(m_Movement * 10, ForceMode.Impulse);
    }
}