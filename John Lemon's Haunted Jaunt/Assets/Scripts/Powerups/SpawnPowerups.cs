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
    public GameObject invincibilityPowerup;

    public int HealthCount = 0;
    public int SpeedCount = 0;
    public int CooldownCount = 0;
    public int CoinCount = 0;

    public float HealthTimer = 20f;
    public float SpeedTimer = 15f;
    public float CooldownTimer = 20f;
    public float CoinTimer = 30f;

    int randomNumber;
    public bool spawnPowerup = false;
    Vector3 powerupPosition = new Vector3(0f, 6f, 0f);
    public float powerupTimer = 5f;
    public int powerupCount = 0;

    //public bool HealthPickedUp = false;
    //public bool SpeedPickedUp = false;
    //public bool CooldownPickedUp = false;
    //public bool CoinPickedUp = false;


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
        if (PhotonNetwork.IsMasterClient)
        {
            if (spawnPowerup)
            {
                randomNumber = Random.Range(1, 5);
                switch (randomNumber)
                {
                    case 1:
                        if (PhotonNetwork.IsMasterClient)
                            photonView.RPC("SpawnHealthPowerup", RpcTarget.AllViaServer, powerupPosition);
                        break;
                    case 2:
                        if (PhotonNetwork.IsMasterClient)
                            photonView.RPC("SpawnSpeedPowerup", RpcTarget.AllViaServer, powerupPosition);
                        break;
                    case 3:
                        if (PhotonNetwork.IsMasterClient)
                            photonView.RPC("SpawnCooldownPowerup", RpcTarget.AllViaServer, powerupPosition);
                        break;
                    case 4:
                        if (PhotonNetwork.IsMasterClient)
                            photonView.RPC("SpawnInvincibilityPowerup", RpcTarget.AllViaServer, powerupPosition);
                        break;

                }
                spawnPowerup = false;
                powerupCount++;
            }
            if (powerupTimer > 0 && PhotonNetwork.IsMasterClient)
                powerupTimer -= Time.deltaTime;
            if (powerupTimer <= 0 && powerupCount < 1 && PhotonNetwork.IsMasterClient)
                spawnPowerup = true;
            Debug.Log(powerupTimer);
        }
    }

    [PunRPC]
    public void SpawnInvincibilityPowerup(Vector3 pos) //the coin 
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject invincibilityPowerup = PhotonNetwork.Instantiate("Coin", pos, Quaternion.identity) as GameObject;
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
            GameObject speedPowerup = PhotonNetwork.Instantiate("speedUp", pos, Quaternion.identity) as GameObject;
        }
    }
}
