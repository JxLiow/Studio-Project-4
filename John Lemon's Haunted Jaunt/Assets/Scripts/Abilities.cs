using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    PlayerAction playerAction;


    [Header("Abilities")]
    public Image abilityImage;
    public float cooldown;
    bool isCooldown = false;
    public KeyCode ability;
    public AudioSource abilityAudio;

    public Score score;

    void Start()
    {
        abilityImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<PlayerAction>())
        {
            return;
        }
        else
        {
            playerAction = FindObjectOfType<PlayerAction>();
        }
        cooldown = (playerAction.abilityDuration + playerAction.abilityCooldown);
        

        Ability();
    }

    void Ability()
    {
        if (Input.GetKey(ability) && isCooldown == false)
        {
            isCooldown = true;
            abilityImage.fillAmount = 1;
            PlayAbilitySound();
        }

        if (isCooldown)
        {
            abilityImage.fillAmount -= 1 / 15f * Time.deltaTime;

            if (abilityImage.fillAmount <= 0)
            {
                abilityImage.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    public void PlayAbilitySound()
    {
        abilityAudio.Play();
    }

    public bool GetCooldown()
    {
        return isCooldown;
    }
        
}
