using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpScript : MonoBehaviour
{
    public float timeRemaining = 30;
    bool runTimer = false;
    private PhotonView photonView;

    public bool pickedUpHealth = false;
    PlayerHealth playerHealth;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    void Start()
    {
        //transform.position = new Vector3(3.0f, 6.0f, -3.0f);
    }
    [PunRPC]
    public void powerupPickedUp()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(gameObject);

        if (playerHealth.health <= playerHealth.maxhealth - 40) //less than or equal to 60 health
        {
            playerHealth.Heal(40);
        }
        else if((playerHealth.health > playerHealth.maxhealth - 40) && (playerHealth.health < playerHealth.maxhealth)) //between 60 - 100 health
        {
            playerHealth.health = playerHealth.maxhealth;
        }
      

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
