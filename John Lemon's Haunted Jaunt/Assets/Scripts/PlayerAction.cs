using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PlayerAction : MonoBehaviour
{
    Animator m_Animator;

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
    public GameObject heartPrefab;
    public GameObject spearPrefab;
    public GameObject shieldPrefab;
    public GameObject tridentPrefab;
    public GameObject arrowPrefab;

    public int playerID;

    bool isGrounded;
    Vector3 Up;
    public float jumpforce = 1f;

    public float dashCooldown;
    Vector3 m_Movement;

    int temp = 1;

    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        playerID = photonView.ViewID;
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
            //look at
            RaycastHit _hit;
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit))
            {
                transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
            }

            m_Animator.SetBool("IsAttacking", true);
            if (m_Animator.GetBool("IsWalking") == true)
            {
                m_Animator.SetBool("IsWalking", false);
            }
            if (m_Animator.GetBool("IsWalking") == false)
            {
                Attacking();
            }

            if (temp == 1)
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
            else if (temp == 4)
            {
                photonView.RPC("shootBullet4", RpcTarget.AllViaServer, rigidbody.position);
            }
            else if (temp == 5)
            {
                photonView.RPC("shootBullet5", RpcTarget.AllViaServer, rigidbody.position);
            }
            else if (temp == 6)
            {
                photonView.RPC("shootBullet6", RpcTarget.AllViaServer, rigidbody.position);
            }
            else if (temp == 7)
            {
                photonView.RPC("shootBullet7", RpcTarget.AllViaServer, rigidbody.position);
            }
            else if (temp == 8)
            {
                photonView.RPC("shootBullet8", RpcTarget.AllViaServer, rigidbody.position);
            }
            else
            {
                photonView.RPC("shootBullet", RpcTarget.AllViaServer, rigidbody.position);
            }



        }

        if (Input.GetMouseButtonDown(1) && photonView.IsMine)
        {
            photonView.RPC("useAbility1", RpcTarget.AllViaServer, rigidbody.position);
        }

        //dashing
        if (Input.GetKey(KeyCode.Space) && photonView.IsMine && dashCooldown <= 0.0f)
        {
            Dashing();
            dashCooldown = 1.5f;
        }

        //dash cooldown
        if (dashCooldown > 0.0f)
            dashCooldown -= Time.deltaTime;


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

        //Ray ray = new Ray(bulletPrefab.position, bulletSpawnPoint.forward);
        //if(Physics.Raycast(ray, out RaycastHit hit, 100f))
        //{
        //    var enemyHealth = hit.collider.GetComponent<PlayerHealth>();
        //    if(enemyHealth)
        //    {
        //        enemyHealth.TakeDamage(10);
        //    }
        //}
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

    [PunRPC]
    public void shootBullet4(Vector3 position)
    {
        var bullet = Instantiate(heartPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }

    [PunRPC]
    public void shootBullet5(Vector3 position)
    {
        var bullet = Instantiate(shieldPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }

    [PunRPC]
    public void shootBullet6(Vector3 position)
    {
        var bullet = Instantiate(spearPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }

    [PunRPC]
    public void shootBullet7(Vector3 position)
    {
        var bullet = Instantiate(tridentPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }

    [PunRPC]
    public void shootBullet8(Vector3 position)
    {
        var bullet = Instantiate(arrowPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
    //ABILITIES
    [PunRPC]
    public void useAbility1(Vector3 position)
    {
        var ability = Instantiate(abilityPrefab, abilitySpawnPoint.position, abilitySpawnPoint.rotation);
        ability.GetComponent<Rigidbody>().velocity = abilitySpawnPoint.forward * abilitySpeed;
    }

    public PhotonView getView()
    {
        return photonView;
    }

    public int getID()
    {
        return playerID;
    }

    void Attacking()
    {
        StartCoroutine(Attack());
    }


    IEnumerator Attack()
    {
        
        m_Animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.1f);
        m_Animator.SetBool("IsAttacking", false);

    }

    void Dashing()
    {
        StartCoroutine(DashAnimation());
        Dash();
    }

    IEnumerator DashAnimation()
    {
        m_Animator.SetBool("IsDashing", true);
        yield return new WaitForSeconds(1.25f);
        m_Animator.SetBool("IsDashing", false);
    }
    void Dash()
    {
        rigidbody.AddForce(m_Movement * 10, ForceMode.Impulse);
    }


}