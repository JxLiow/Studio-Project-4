using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusPassive : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.gameObject.transform.parent != other.transform.parent)
            {
                var enemyHealth = other.gameObject.GetComponent<PlayerHealth>();
                if (enemyHealth.getHealth() > 0)
                {
                    enemyHealth.TakeDamage(10);
                }
                this.gameObject.transform.parent.GetComponent<PlayerAction>().respawnZeusPassive = true;
            }
                Destroy(this.gameObject);
        }
    }
}
