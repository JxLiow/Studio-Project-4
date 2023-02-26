using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerPickedUpPowerup : MonoBehaviour
{
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    PlayerAction playerAction;
    float speedUpDuration = 0.0f;
    bool pickedUpSpeed = false;
    GameObject gm;
    public AudioSource pickedPowerupAudio;
    PhotonView pv;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerAction = FindObjectOfType<PlayerAction>();

        gm = GameObject.FindWithTag("GM");
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pickedUpSpeed)
        {
            if (speedUpDuration > 0)
            {
                speedUpDuration -= Time.deltaTime;
            }
            else if (speedUpDuration <= 0)
            {
                playerMovement.speedModifier = 8;
                pickedUpSpeed = false;
            }
        }
        
        //Debug.Log("player movement: " + playerMovement.speedModifier);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Health")
        {
            if (playerHealth.health <= playerHealth.maxhealth - 40) //less than or equal to 60 health
            {
                playerHealth.Heal(40);
            }
            else if ((playerHealth.health > playerHealth.maxhealth - 40) && (playerHealth.health < playerHealth.maxhealth)) //between 60 - 100 health
            {
                playerHealth.health = playerHealth.maxhealth;
            }
            pickedPowerupAudio.Play();
        }
        else if(other.tag == "Speed")
        {
            speedUpDuration = 5.0f;
            playerMovement.speedModifier += 3; //increase speed
            pickedUpSpeed = true;
            pickedPowerupAudio.Play();
        }
        else if(other.tag == "Cooldown")
        {
            if (playerAction.isOnCooldown == true || playerAction.isActivated == true)
            {
                playerAction.abilityCooldown -= 3f;
            }
            pickedPowerupAudio.Play();
        }
        else if(other.tag == "Coin")
        {
            playerHealth.invincible = true;
            pickedPowerupAudio.Play();
        }

        if (PhotonNetwork.IsMasterClient)
        {
            gm.GetComponent<SpawnPowerups>().powerupCount--;
            gm.GetComponent<SpawnPowerups>().powerupTimer = 5f;
        }
    }
}
