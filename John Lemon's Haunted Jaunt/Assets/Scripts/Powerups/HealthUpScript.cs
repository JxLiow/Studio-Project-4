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
    
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        
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
