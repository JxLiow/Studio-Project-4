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
    int randomNumber = 101;
    PlayerAction playerAction;
    private PhotonView photonView;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, life);
        damage = PlayerPrefs.GetFloat("damage", 10);
        //pAction = GetComponent<PlayerAction>();
        //pHealth = GetComponent<PlayerHealth>();
        //owner = pHealth.getName();
        photonView = GetComponent<PhotonView>();
        score = GetComponent<Score>();
        if (PlayerPrefs.GetString("godname") == "Hades")
        {
            randomNumber = Random.Range(0, 100);
           // playerAction
        }
    }
    private void Update()
    {
        Debug.Log("rand: " + randomNumber);
    }
    void OnCollisionEnter(Collision collision)
    {
        var enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
        
        if (enemyHealth)
        {
            enemyHealth.TakeDamage(damage);
            if (PlayerPrefs.GetString("godname", "") == "Ares")
            {
                if (playerHealth.getHealth() <= 25)
                {
                    enemyHealth.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
            else
            {
                if (randomNumber < 100 && PlayerPrefs.GetString("godname") == "Hades")
                {
                    var enemyAction = collision.gameObject.GetComponent<PlayerAction>();
                    enemyAction.isPoisoned = true; 
                }
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
            //if (enemyHealth.getHealth() <= 0)
            //    score.addScore(10);
        }
        Destroy(gameObject);
    }
}
