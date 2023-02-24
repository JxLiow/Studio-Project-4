using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityHPScript : MonoBehaviour
{

    float health;
    float maxHealth;

    PlayerHealth pHealth;

    [SerializeField]
    public Image image;

    void Start()
    {
        //pHealth = GameObject.Find("JohnLemon").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            return;
        }
        else
        {
            pHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            health = pHealth.getHealth();
            maxHealth = pHealth.getMaxHealth();

            image.fillAmount = CalculateHealth();

            if (health > maxHealth)
                health = maxHealth;
        }
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    public void reduceHP(int num)
    {
        health -= num;
    }
}
