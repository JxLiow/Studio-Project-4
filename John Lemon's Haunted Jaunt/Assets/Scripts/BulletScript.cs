using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class BulletScript : MonoBehaviour
{
    public float life = 3;
    float damage;

    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, life);
        damage = PlayerPrefs.GetFloat("damage", 10);
    }

    void OnCollisionEnter(Collision collision)
    {
        var enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
        
        if (enemyHealth)
        {
            enemyHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
