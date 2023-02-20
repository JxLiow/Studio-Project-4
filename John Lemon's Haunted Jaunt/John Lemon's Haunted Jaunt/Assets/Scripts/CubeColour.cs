using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class CubeColour : MonoBehaviour
{
    private PhotonView photonView;
    // Start is called before the first frame update
    public void Awake()
    {
        photonView = transform.parent.GetComponent<PhotonView>();
    }
    void Start()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.color = JLGame.GetColor(photonView.Owner.ActorNumber);
        }
        // gameObject.GetComponent<Material>().color = cubeColour;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
