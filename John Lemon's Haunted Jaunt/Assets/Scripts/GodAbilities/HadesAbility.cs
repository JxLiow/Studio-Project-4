using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HadesAbility : MonoBehaviour
{
    PhotonView photonView;
    bool takeDamage;
    private float damageRate = 0.5f; // adjust this value to change the rate of damage
    private float timeSinceLastDamage = 0f;
    private float destroyTimer = 5f;
    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            destroyTimer -= Time.deltaTime;
        }
        if (destroyTimer < 0f)
            Destroy(this.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.gameObject.transform.parent != other.transform.parent)
            {
                var enemyHealth = other.gameObject.GetComponent<PlayerHealth>();
                if (enemyHealth.getHealth() > 0)
                {
                    timeSinceLastDamage += Time.deltaTime;
                    if (timeSinceLastDamage >= damageRate)
                    {
                        enemyHealth.TakeDamage(5);
                        timeSinceLastDamage = 0f;
                    }
                }
            }
        }      
    }
}
