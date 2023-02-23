using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpScript : MonoBehaviour
{
    public float timeRemaining = 15; //timer for item respawn
    public float powerupDuration = 0; //timer for duration of powerup when player pick it ups
    bool runTimer = false;
    //public bool hasPowerUp = false;
    private PhotonView photonView;

    PlayerMovement playerMovement;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        playerMovement = FindObjectOfType<PlayerMovement>();

    }
    void Start()
    {
        //transform.position = new Vector3(-3.0f, 6.0f, -3.0f);
    }
    [PunRPC]
    public void powerupPickedUp()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(gameObject);

        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            runTimer = true;
            powerupDuration = 5;
            photonView.RPC("powerupPickedUp", RpcTarget.AllViaServer);
        }

    }

    void Update()
    {


            

        
    }

}
