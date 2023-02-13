using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class DroppingFloor : MonoBehaviour
{
    private PhotonView photonView;

    bool contactTimer;
    int timer;

    public Material Material1;
    public GameObject Object;
    public int timeToDisappear = 500;

    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        contactTimer = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(contactTimer)
        {
            timer++;
            if(timer % timeToDisappear == 0)
            {
                Object.active = false;
            }
        }
    }

    [PunRPC]
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            contactTimer = true;
            Object.GetComponent<MeshRenderer>().material = Material1;
        }
    }
}
