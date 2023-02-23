using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpScript : MonoBehaviour
{
    public float powerupDuration = 5; //timer for duration of powerup when player pick it ups
    public float timeRemaining = 15; //timer for item respawn
    bool runTimer = false;
    //public bool hasPowerUp = false;
    private PhotonView photonView;

    PlayerMovement playerMovement;
    SpawnPowerups spawnPowerUp;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        spawnPowerUp = FindObjectOfType<SpawnPowerups>();

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

        spawnPowerUp.SpeedCount = 0;
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
