using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class WaveScript : MonoBehaviour
{
    //Rigidbody m_Rigidbody;
    private PhotonView photonView;

    PlayerMovement playerMovement;
    public float life = 3;

    void Awake()
    {
        Destroy(gameObject, life);
        photonView = GetComponent<PhotonView>();
        //m_Rigidbody = GetComponent<Rigidbody>();

        playerMovement = FindObjectOfType<PlayerMovement>();

    }

    void OnCollisionEnter(Collision collision)
    {
       
    }
}
