using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownUpScript : MonoBehaviour
{
    private PhotonView photonView;
    SpawnPowerups spawnPowerUp;


    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        spawnPowerUp = FindObjectOfType<SpawnPowerups>();

    }
    void Start()
    {
        //transform.position = new Vector3(-3.0f, 6.0f, 3.0f);
    }
    [PunRPC]
    public void powerupPickedUp()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(gameObject);

        spawnPowerUp.CooldownCount = 0;

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
