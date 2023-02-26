using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using Photon.Pun;

public class PlayerHealth : MonoBehaviourPunCallbacks, IPunObservable
{
    public float maxhealth = 100;
    public float health = 10;
    Animator m_Animator;
    public GameObject spawnPoints;

    PlayerAction playerAction;

    CoinScript coinScript;
    float invincibleDuration = 0.5f;
    public bool invincible = false;

    public string pName;
    bool takeDmg;

    bool isTakingDamage;

    PhotonView photonView;

    PlayerManager playerManager;

    float aphroditeTimer = 5.0f;

    public GameObject HealthUp;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        m_Animator = GetComponent<Animator>();
        coinScript = FindObjectOfType<CoinScript>();
        playerAction = FindObjectOfType<PlayerAction>();
        spawnPoints = GameObject.FindWithTag("SpawnPoint");

        playerManager = GetComponent<PlayerManager>();
        takeDmg = true;

        isTakingDamage = false;

    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //sync health
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
        photonView.RPC(nameof(RPC_TakeDamage), photonView.Owner, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage, PhotonMessageInfo info)
    {
        if (invincible == false)
        {
            health -= damage;
            isTakingDamage = true;
        }
        if (health <= 0 && takeDmg)
        {
            PlayerManager.Find(info.Sender).getKill();
            takeDmg = false;
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
        transform.position = JLGameManager.spawnPositions[photonView.Owner.ActorNumber - 1];
        takeDmg = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && photonView.IsMine)
        {
            TakeDamage(20);
        }
        //invincible/coin powerup
        if (invincible)
        {
            if (invincibleDuration > 0)
            {
                invincibleDuration -= Time.deltaTime;
            }
            else if (invincibleDuration <= 0)
            {
                invincibleDuration = 0.5f;
                invincible = false;
            }
        }

        if (health <= 0)
        {
            m_Animator.SetBool("Death", true);
            StartCoroutine(Respawn());
        }

        //for aphrodite passive - heal when out of combat
        if(playerAction.godName == "Aphrodite")
        {
            aphroditeTimer -= Time.deltaTime;
            if(isTakingDamage)
            {
                aphroditeTimer = 5.0f;
            }
            if(aphroditeTimer <= 0)
            {
                if(health < 100)
                {
                    Heal(5 * Time.deltaTime);
                }
            }
        }
        isTakingDamage = false;


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
