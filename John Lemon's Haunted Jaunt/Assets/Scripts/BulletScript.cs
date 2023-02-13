using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class BulletScript : MonoBehaviour
{
    public float life = 3;

    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
            FindObjectOfType<EntityHPScript>().reduceHP(10);
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<EntityHPScript>().reduceHP(10);
        }
        Destroy(gameObject);
    }
}
