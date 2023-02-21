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

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
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
            //transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            //runTimer = true;
            //hasPowerUp = true;
            photonView.RPC("powerupPickedUp", RpcTarget.AllViaServer);
        }

    }

    void Update()
    {

        //if (runTimer)
        //{
        //    if (timeRemaining > 0)
        //    {
        //        timeRemaining -= Time.deltaTime;
        //    }
        //    else if (timeRemaining <= 0)
        //    {
        //        transform.position = new Vector3(-3.0f, 6.0f, -3.0f);
        //        runTimer = false;
        //        timeRemaining = 15;
        //    }

        //    if (hasPowerUp)
        //    {
        //        if (powerupDuration > 0)
        //        {
        //            powerupDuration -= Time.deltaTime;
        //        }
        //        else if (powerupDuration <= 0)
        //        {
        //            hasPowerUp = false;
        //            powerupDuration = 5;
        //        }
        //    }

        //}
    }

}
