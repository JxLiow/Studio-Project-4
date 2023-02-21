using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpScript : MonoBehaviour
{
    public float timeRemaining = 15; //timer for item respawn
    public float powerupDuration = 5; //timer for duration of powerup when player pick it ups
    bool runTimer = false;
    public bool hasPowerUp = false;
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

        playerMovement.speedModifier += 3; //increase speed
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            runTimer = true;
            hasPowerUp = true;
            photonView.RPC("powerupPickedUp", RpcTarget.AllViaServer);
        }

    }

    void Update()
    {

        if (runTimer)
        {
            //if (timeRemaining > 0) //for spawning
            //{
            //    timeRemaining -= Time.deltaTime;
            //}
            //else if (timeRemaining <= 0)
            //{
            //    runTimer = false;
            //    timeRemaining = 15;
            //}

            if (hasPowerUp) //picked up powerup
            {
                if (powerupDuration > 0) //powerup in effect
                {
                    powerupDuration -= Time.deltaTime;
                }
                else if (powerupDuration <= 0) //powerup duration ended
                {
                    hasPowerUp = false;
                    playerMovement.speedModifier = 0; //return to normal
                    powerupDuration = 5;
                    runTimer = false;
                }
            }

        }
    }

}
