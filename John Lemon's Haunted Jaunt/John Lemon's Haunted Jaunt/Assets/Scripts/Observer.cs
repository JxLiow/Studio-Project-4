using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Observer : MonoBehaviour
{
    //public Transform player;
    private Transform player;

    bool m_IsPlayerInRange;
    private PhotonView photonView;

    private void Start()
    {
        photonView = transform.parent.GetComponent<PhotonView>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.name == "JohnLemon(Clone)")
        {
            if(other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                player = other.transform;
                m_IsPlayerInRange = true;
            }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.name == "JohnLemon(Clone)")
        {
            if (other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                m_IsPlayerInRange = false;
            }
        }
    }

    void Update ()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            
            if (Physics.Raycast (ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    photonView.RPC("CallCaughtPlayer", RpcTarget.All);
                }
            }
        }
    }
}
