using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Photon.Pun;
public class CoinScript : MonoBehaviour
{
    public float powerupDuration = 3;
    bool runTimer = false;
    private PhotonView photonView;
    PlayerHealth playerHealth;
    SpawnPowerups spawnPowerUp;


    void Start()
    {
        photonView = GetComponent<PhotonView>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        spawnPowerUp = FindObjectOfType<SpawnPowerups>();

    }

    [PunRPC]
    public void powerupPickedUp()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(gameObject);


        spawnPowerUp.CoinCount = 0;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            runTimer = true;
            photonView.RPC("powerupPickedUp", RpcTarget.AllViaServer);
        }

    }

    void Update()
    {

     
    }

}
