using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using Photon.Pun;

public class PlayerHealth : MonoBehaviourPunCallbacks, IPunObservable
{
    public int maxhealth = 100;
    public int health = 100;
    Animator m_Animator;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
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
            health = (int)stream.ReceiveNext();    
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    IEnumerator Respawn()
    {
        health = maxhealth;
        //GetComponent<CharacterController>().enabled = false;
        transform.position = new Vector3(16, 1, 16);
        yield return new WaitForSeconds(5); //wait for 5 seconds
        //GetComponent<CharacterController>().enabled = true;
        m_Animator.SetBool("Death", false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            m_Animator.SetBool("Death", true);
            StartCoroutine(Respawn());
           
        }
    }

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxhealth;
    }
}
