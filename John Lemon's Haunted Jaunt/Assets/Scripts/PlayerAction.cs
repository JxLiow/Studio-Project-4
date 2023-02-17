using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PlayerAction : MonoBehaviour
{
    Animator m_Animator;
    bool isAttacking = false;

    private Rigidbody rigidbody;
    private PhotonView photonView;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    public Transform abilitySpawnPoint;
    public GameObject abilityPrefab;
    public float abilitySpeed = 0.1f;

    public GameObject lightningPrefab;
    public GameObject skullPrefab;
    public GameObject featherPrefab;

    bool isGrounded;
    Vector3 Up;
    public float jumpforce = 1f;

    int temp = 1;

    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();

        Up = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && photonView.IsMine)
        {
            temp += 1;
        }
        if (Input.GetKeyDown(KeyCode.O) && photonView.IsMine)
        {
            temp -= 1;
        }

        
        if (Input.GetMouseButtonDown(0) && photonView.IsMine)
        {
            if(temp == 1)
            {
                photonView.RPC("shootBullet1", RpcTarget.AllViaServer, rigidbody.position);

            }
            else if (temp == 2)
            {
                photonView.RPC("shootBullet2", RpcTarget.AllViaServer, rigidbody.position);

            }
            else if (temp == 3)
            {

                photonView.RPC("shootBullet3", RpcTarget.AllViaServer, rigidbody.position);
            }
            else
            {
                photonView.RPC("shootBullet", RpcTarget.AllViaServer, rigidbody.position);
            }

            m_Animator.SetInteger("condition", 2);
           
        }
        if (Input.GetMouseButtonDown(1) && photonView.IsMine)
        {
            photonView.RPC("useAbility1", RpcTarget.AllViaServer, rigidbody.position);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && photonView.IsMine)
        {
            rigidbody.AddForce(Up * jumpforce, ForceMode.Impulse);
            isGrounded = false;
        }

        
        
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    //PRIMARY FIRE
    [PunRPC]
    public void shootBullet (Vector3 position)
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }

    [PunRPC]
    public void shootBullet1(Vector3 position)
    {
        var bullet = Instantiate(lightningPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }

    [PunRPC]
    public void shootBullet2(Vector3 position)
    {
        var bullet = Instantiate(skullPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }

    [PunRPC]
    public void shootBullet3(Vector3 position)
    {
        var bullet = Instantiate(featherPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }


    //ABILITIES
    [PunRPC]
    public void useAbility1(Vector3 position)
    {
        var ability = Instantiate(abilityPrefab, abilitySpawnPoint.position, abilitySpawnPoint.rotation);
        ability.GetComponent<Rigidbody>().velocity = abilitySpawnPoint.forward * abilitySpeed;
    }
}