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
    public float bulletSpeed;

    public Transform abilitySpawnPoint;
    public GameObject abilityPrefab;
    public float abilitySpeed = 0.1f;

    public GameObject lightningPrefab;
    public GameObject skullPrefab;
    public GameObject heartPrefab;
    public GameObject arrowPrefab;
    public GameObject featherPrefab;
    public GameObject tridentPrefab;
    public GameObject swordPrefab;
    public GameObject shieldPrefab;
    List<GameObject> projectileList = new List<GameObject>();
    public int playerID;

    bool isGrounded;
    Vector3 Up;
    public float jumpforce = 1f;

    public float dashCooldown;
    Vector3 m_Movement;
    
    int temp = 1;

    bool shoot = false;
    string godName;
    float fRate;
    float time = 0, pElapsedTime = 0;

    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        playerID = photonView.ViewID;
        Up = new Vector3(0, 1, 0);
        godName = PlayerPrefs.GetString("godname", "Zeus");
        Debug.Log("god = "+godName);
        fRate = PlayerPrefs.GetFloat("firerate", 1);
        Debug.Log("firerate = "+fRate);
        bulletSpeed = 15f;
        projectileList.Add(lightningPrefab);
        projectileList.Add(heartPrefab);
        projectileList.Add(skullPrefab);
        projectileList.Add(arrowPrefab);
        projectileList.Add(featherPrefab);
        projectileList.Add(tridentPrefab);
        projectileList.Add(swordPrefab);
        projectileList.Add(shieldPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && photonView.IsMine)
            shoot = true;
        else if(Input.GetMouseButtonUp(0) && photonView.IsMine)
            shoot = false;
        
        if(shoot)
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
            float diff = time - pElapsedTime;
            if (diff > fRate)
            {
                photonView.RPC("shootBullet", RpcTarget.AllViaServer, rigidbody.position);
                pElapsedTime = time;
            }



        }

        //if (Input.GetMouseButtonDown(1) && photonView.IsMine)
        //{
        //    photonView.RPC("useAbility1", RpcTarget.AllViaServer, rigidbody.position);
        //}

        //dashing
        if (Input.GetKey(KeyCode.Space) && photonView.IsMine && dashCooldown <= 0.0f)
        {
            Dashing();
            dashCooldown = 1.5f;
        }

        //dash cooldown
        if (dashCooldown > 0.0f)
            dashCooldown -= Time.deltaTime;

        //skills
        if (Input.GetMouseButtonDown(1) && photonView.IsMine)
        {
            switch (godName)
            {
                case "Zeus":

                    break;
                case "Aphrodite":

                    break;
                case "Hades":
                    
                    photonView.RPC("useHadesAbility", RpcTarget.AllViaServer, rigidbody.position);
                    break;
                case "Artemis":

                    break;
                case "Poseidon":

                    break;
                case "Hermes":

                    break;
                case "Ares":

                    break;
                case "Athena":

                    break;

            }
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
        var bullet = Instantiate(projectileList[PlayerPrefs.GetInt("id")], bulletSpawnPoint.position, bulletSpawnPoint.rotation);
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
    public void useHadesAbility(Vector3 position)
    {
        position.y = position.y + 0.25f;
        if (photonView.IsMine)
        {
        GameObject hadesAbility = PhotonNetwork.Instantiate("HadesSkill", position, Quaternion.identity) as GameObject;
        hadesAbility.transform.parent = rigidbody.transform;
        }
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