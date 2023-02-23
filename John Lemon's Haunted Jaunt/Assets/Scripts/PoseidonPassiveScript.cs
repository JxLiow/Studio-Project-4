using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PoseidonPassiveScript : MonoBehaviour
{
    private PhotonView photonView;

    PlayerMovement playerMovement;

    public float life = 2;

    void Awake()
    {
        Destroy(gameObject, life);
        photonView = GetComponent<PhotonView>();
        playerMovement = FindObjectOfType<PlayerMovement>();

    }

    void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<PlayerMovement>();

        if (other.gameObject.tag == "Player")
        {
            
            enemy.speedModifier = 2;
        }
    }
}
