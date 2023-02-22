using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using Photon.Pun;

public class PlayerHealth : MonoBehaviourPunCallbacks, IPunObservable
{
    public float maxhealth = 100;
    public float health = 100;
    Animator m_Animator;

    CoinScript coinScript;
    float invincibleDuration = 3;
    public bool invincible = false;

    //private HealthUpScript healthUp;

    public GameObject HealthUp;
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        coinScript = FindObjectOfType<CoinScript>();

        //healthUp = HealthUp.GetComponent<HealthUpScript>();
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //sync health
        //throw new System.NotImplementedException();
        if(stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (float)stream.ReceiveNext();    
        }
    }

    public void TakeDamage(float damage)
    {
        if(invincible == false)
        {
            health -= damage;
        }
    }
    public void Heal(float healAmt)
    {
        health += healAmt;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5); //wait for 5 seconds
        health = maxhealth;
        m_Animator.SetBool("Death", false);
        transform.position = new Vector3(16, 1, 16);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //invincible/coin powerup
        if (invincible)
        {
            if (invincibleDuration > 0)
            {
                invincibleDuration -= Time.deltaTime;
            }
            else if (invincibleDuration <= 0)
            {
                invincibleDuration = 3;
                invincible = false;
            }
        }

        if (health <= 0)
        {
            m_Animator.SetBool("Death", true);
            StartCoroutine(Respawn());
        }
    }

    public float getHealth()
    {
        return health;
    }

    public float getMaxHealth()
    {
        return maxhealth;
    }
}
