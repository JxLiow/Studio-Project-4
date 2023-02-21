using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownUpScript : MonoBehaviour
{
    private PhotonView photonView;
    public float timeRemaining = 25;
    bool runTimer = false;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
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

        //if (runTimer == true)
        //{
        //    if (timeRemaining > 0)
        //    {
        //        timeRemaining -= Time.deltaTime;
        //    }
        //    else if (timeRemaining <= 0)
        //    {
        //        transform.position = new Vector3(-3.0f, 6.0f, 3.0f);
        //        runTimer = false;
        //        timeRemaining = 25;
        //    }
        //}

    }

}
