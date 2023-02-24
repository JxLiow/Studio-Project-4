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
        if(HealthCount == 0)
        {
            if (HealthTimer > 0)
            {
                HealthTimer -= Time.deltaTime;
            }
            else if (HealthTimer <= 0)
            {
                HealthTimer = 10f;
            }
        }

        if (SpeedCount == 0)
        {
            if (SpeedTimer > 0)
            {
                SpeedTimer -= Time.deltaTime;
            }
            else if (SpeedTimer <= 0)
            {
                SpeedTimer = 15f;
            }
        }

        if (CooldownCount == 0)
        {
            if (CooldownTimer > 0)
            {
                CooldownTimer -= Time.deltaTime;
            }
            else if (CooldownTimer <= 0)
            {
                CooldownTimer = 20f;
            }
        }

        if (CoinCount == 0)
        {
            if (CoinTimer > 0)
            {
                CoinTimer -= Time.deltaTime;
            }
            else if (CoinTimer <= 0)
            {
                CoinTimer = 30f;
            }
        }

        if (timer.currentTime < 280)
        {
            if((HealthCount == 0) && (HealthTimer <= 0))
            {
                var position = new Vector3(3.0f, 6.0f, -3.0f);
                if (PhotonNetwork.IsMasterClient)
                    photonView.RPC("SpawnHealthPowerup", RpcTarget.AllViaServer, position);
                HealthCount++;
            }
        }
        if (timer.currentTime < 275)
        {
            if ((SpeedCount == 0) && (SpeedTimer <= 0))
            {
                var position = new Vector3(-3.0f, 6.0f, 3.0f);
                if (PhotonNetwork.IsMasterClient)
                    photonView.RPC("SpawnSpeedPowerup", RpcTarget.AllViaServer, position);
                SpeedCount++;
            }
        }
        if (timer.currentTime < 300)
        {
            if ((CooldownCount == 0) && (CooldownTimer <= 0))
            {
                var position = new Vector3(-3.0f, 6.0f, -3.0f);
                if (PhotonNetwork.IsMasterClient)
                    photonView.RPC("SpawnCooldownPowerup", RpcTarget.AllViaServer, position);
                CooldownCount++;
            }
        }
        if (timer.currentTime < 260)
        {
            if ((CoinCount == 0) && (CoinTimer <= 0))
            {
                var position = new Vector3(3.0f, 6.0f, 3.0f);
                if (PhotonNetwork.IsMasterClient)
                    photonView.RPC("SpawnInvincibilityPowerup", RpcTarget.AllViaServer, position);
                CoinCount++;
            }
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
