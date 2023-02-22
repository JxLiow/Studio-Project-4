using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickedUpPowerup : MonoBehaviour
{
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;
    float speedUpDuration = 0.0f;
    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(speedUpDuration <= 0)
        {
            playerMovement.speedModifier = 8;
        }
        if (speedUpDuration > 0)
        {
            speedUpDuration -= Time.deltaTime;
        }
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
        }
        else if(other.tag == "Speed")
        {
            speedUpDuration = 5.0f;
            playerMovement.speedModifier += 3; //increase speed
        }
        else if(other.tag == "Cooldown")
        {

        }
    }
}
