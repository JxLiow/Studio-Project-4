using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusAbility : MonoBehaviour
{
    float destroyTimer = 0.15f;
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (this.gameObject.transform.parent != other)
            {
                var enemyHealth = other.gameObject.GetComponent<PlayerHealth>();
                if (enemyHealth.getHealth() > 0)
                { 
                    enemyHealth.TakeDamage(20);
                }
            }
        }
    }
}
