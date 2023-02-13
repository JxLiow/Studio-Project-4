using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PressurePlate : MonoBehaviour
{
    private PhotonView photonView;

    public GameObject obj1, obj2, obj3;
    bool active;

    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();

        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            obj1.active = !obj1.active;
            obj3.active = !obj3.active;
            obj2.active = !obj2.active;
            active = false;
        }
    }

    [PunRPC]
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            active = true;
        }
    }
}
