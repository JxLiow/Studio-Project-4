using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpScript : MonoBehaviour
{
    bool runTimer = false;
    private PhotonView photonView;

    public bool pickedUpHealth = false;
    
    SpawnPowerups spawnPowerUp;


    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        
    }
    void Start()
    {

    }
    [PunRPC]
    public void powerupPickedUp()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(gameObject);

        spawnPowerUp.HealthCount = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            photonView.RPC("powerupPickedUp", RpcTarget.AllViaServer);

        }

    }

    void Update()
    {

   

    }

}
