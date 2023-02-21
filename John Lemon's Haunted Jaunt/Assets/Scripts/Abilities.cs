using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [Header("Abilities")]
    public Image abilityImage;
    public float cooldown = 5.0f;
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
        Ability();
    }

    void Ability()
    {
        if (Input.GetKey(ability) && isCooldown == false)
        {
            isCooldown = true;
            abilityImage.fillAmount = 1;
            PlayAbilitySound();
            score.UpdateScore();
        }

        if (isCooldown)
        {
            abilityImage.fillAmount -= 1 / cooldown * Time.deltaTime;

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
}
