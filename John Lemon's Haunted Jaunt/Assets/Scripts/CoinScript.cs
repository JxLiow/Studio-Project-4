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

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        //transform.position = new Vector3(3.0f, 6.0f, 3.0f);
    }

    [PunRPC]
    public void powerupPickedUp()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Destroy(gameObject);

        playerHealth.invincible = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            runTimer = true;
        }

        photonView.RPC("powerupPickedUp", RpcTarget.AllViaServer);

    }

    void Update()
    {

        if (runTimer == true)
        {
            if (powerupDuration > 0)
            {
                powerupDuration -= Time.deltaTime;
            }
            else if (powerupDuration <= 0)
            {
                playerHealth.invincible = false;
                runTimer = false;
                powerupDuration = 3;
            }
        }

    }

}
