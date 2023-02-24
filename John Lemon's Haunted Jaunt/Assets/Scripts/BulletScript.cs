using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class BulletScript : MonoBehaviour
{
    public float life = 3;
    float damage;
    PlayerAction pAction;
    PlayerHealth playerHealth;
    Score score;
    string owner;

    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, life);
        damage = PlayerPrefs.GetFloat("damage", 10);
        //pAction = GetComponent<PlayerAction>();
        //pHealth = GetComponent<PlayerHealth>();
        //owner = pHealth.getName();
        score = GetComponent<Score>();
    }

    void OnCollisionEnter(Collision collision)
    {
        var enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
        
        if (enemyHealth)
        {
            if (PlayerPrefs.GetString("godname", "") == "Ares")
            {
                if (playerHealth.getHealth() <= 25)
                {
                    enemyHealth.TakeDamage(damage * 2);
                }
            }
            else
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
            //if (enemyHealth.getHealth() <= 0)
            //    score.addScore(10);
        }
        Destroy(gameObject);
    }
}
