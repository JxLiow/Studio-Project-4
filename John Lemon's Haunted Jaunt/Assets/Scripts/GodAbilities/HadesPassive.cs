using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesPassive : MonoBehaviour
{
    private float damageRate = 1f; // adjust this value to change the rate of damage
    private float timeSinceLastDamage = 0f;
    private float destroyTimer = 5f;
    // Start is called before the first frame update
    void Start()
    {

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
            if (this.gameObject.transform.parent == other.transform.parent)
            {
                var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth.getHealth() > 0)
                {
                    timeSinceLastDamage += Time.deltaTime;
                    if (timeSinceLastDamage >= damageRate)
                    {
                        playerHealth.TakeDamage(2);
                        timeSinceLastDamage = 0f;
                    }
                }
            }
        }
    }
}
