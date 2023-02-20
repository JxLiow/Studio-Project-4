using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPowerups : MonoBehaviour
{
    public PhotonView photonView;
    public JLGameManager _JLGameManager;
    public Timer timer;
    public GameObject healthPowerup;
    public GameObject cooldownPowerup;
    public GameObject speedPowerup;
    int powerupCount = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.currentTime < 280)
        {
            if(powerupCount < 1)
            {
                var position = new Vector3(3.0f, 6.0f, -3.0f);
                if (PhotonNetwork.IsMasterClient)
                    photonView.RPC("SpawnHealthPowerup", RpcTarget.AllViaServer, position);
                powerupCount++;
            }
        }
        if (timer.currentTime < 275)
        {
            if (powerupCount < 2)
            {
                var position = new Vector3(-3.0f, 6.0f, 3.0f);
                if (PhotonNetwork.IsMasterClient)
                    photonView.RPC("SpawnCooldownPowerup", RpcTarget.AllViaServer, position);
                powerupCount++;
            }
        }
        if (timer.currentTime < 270)
        {
            if (powerupCount < 3)
            {
                var position = new Vector3(-3.0f, 6.0f, -3.0f);
                if (PhotonNetwork.IsMasterClient)
                    photonView.RPC("SpawnSpeedPowerup", RpcTarget.AllViaServer, position);
                powerupCount++;
            }
        }
    }
    [PunRPC]
    public void SpawnHealthPowerup(Vector3 pos)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject healthPowerup = PhotonNetwork.Instantiate("Health", pos, Quaternion.identity) as GameObject;
        }
    }
    [PunRPC]
    public void SpawnCooldownPowerup(Vector3 pos)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject cooldownPowerup = PhotonNetwork.Instantiate("CooldownUp", pos, Quaternion.identity) as GameObject;
        }
    }
    [PunRPC]
    public void SpawnSpeedPowerup(Vector3 pos)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject healthPowerup = PhotonNetwork.Instantiate("speedUp", pos, Quaternion.identity) as GameObject;
        }
    }
}
