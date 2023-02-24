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

    //primary fire
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f;

    public GameObject lightningPrefab;
    public GameObject skullPrefab;
    public GameObject heartPrefab;
    public GameObject arrowPrefab;
    public GameObject featherPrefab;
    public GameObject tridentPrefab;
    public GameObject swordPrefab;
    public GameObject shieldPrefab;
    List<GameObject> projectileList = new List<GameObject>();
    //ability
    public Transform abilitySpawnPoint;
    public Transform PoseidonAbilitySpawnPoint;
    public Transform PoseidonPassiveSpawnPoint;
    public GameObject abilityPrefab;

    public GameObject PoseidonAbilityPrefab;
    public GameObject PoseidonPassivePrefab;

    public GameObject zeusImage;
    public GameObject hadesImage;
    public GameObject aphroditeImage;
    public GameObject artemisImage;
    public GameObject hermesImage;
    public GameObject poseidonImage;
    public GameObject aresImage;
    public GameObject athenaImage;

    public int playerID;

    bool isGrounded;
    Vector3 Up;
    public float jumpforce = 1f;

    public float dashCooldown;
    Vector3 m_Movement;
    

    bool shoot = false;
    public string godName;
    float fRate;
    float damage;
    float time = 0, pElapsedTime = 0;
    public int score = 0;

    // ability stuff
    public bool isActivated;
    public bool isOnCooldown;
    public float abilityDuration;
    public float abilityCooldown;
    public float PoseidonAbilitySpeed;

    bool isPassiveZeus = false;
    public bool respawnZeusPassive = false;
    float respawnZeusPassiveTimer = 0f;
    Timer timer;

    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        timer = FindObjectOfType<Timer>();

        playerID = photonView.ViewID;
        Up = new Vector3(0, 1, 0);
        godName = PlayerPrefs.GetString("godname", "Zeus");
        Debug.Log("god = "+godName);
        name = PlayerPrefs.GetString("name", "");
        Debug.Log("name = "+name);
        fRate = PlayerPrefs.GetFloat("firerate", 1);
        //Debug.Log("firerate = "+fRate);
        bulletSpeed = 15f;
        projectileList.Add(lightningPrefab);
        projectileList.Add(heartPrefab);
        projectileList.Add(skullPrefab);
        projectileList.Add(arrowPrefab);
        projectileList.Add(featherPrefab);
        projectileList.Add(tridentPrefab);
        projectileList.Add(swordPrefab);
        projectileList.Add(shieldPrefab);

        abilityDuration = 5f;
        abilityCooldown = 10f;
        isActivated = false;
        isOnCooldown = false;
        damage = PlayerPrefs.GetFloat("damage", 1);
    
        if (PlayerPrefs.GetString("godname") == "Zeus")
        {
            if(photonView.IsMine)
                isPassiveZeus = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && photonView.IsMine)
        {
            m_Animator.SetBool("IsAttacking", true);
            shoot = true;
        }
        else if(Input.GetMouseButtonUp(0) && photonView.IsMine)
        {
            shoot = false;
        }
        
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
       
       
        if((godName == "Poseidon") && (photonView.IsMine))
        {
            if(timer.currentTime < 299.5)
            {
                photonView.RPC("usePoseidonPassive", RpcTarget.AllViaServer, rigidbody.position);
            }       
        }

        //skills
        if (Input.GetMouseButtonDown(1) && photonView.IsMine)
        {
            //attack animation
            m_Animator.SetBool("IsAttacking", true);
            if (m_Animator.GetBool("IsWalking") == true)
            {
                m_Animator.SetBool("IsWalking", false);
            }
            if (m_Animator.GetBool("IsWalking") == false)
            {
                Attacking();
            }


        }
        //if (Input.GetMouseButtonDown(1) && photonView.IsMine)
        //{
        //    photonView.RPC("useAbility1", RpcTarget.AllViaServer, rigidbody.position);
        //}

        if (isActivated == true)
        {
            abilityDuration -= Time.deltaTime;
        }

        if (abilityDuration <= 0f)
        {
            isActivated = false;
            isOnCooldown = true;
        
            if (isOnCooldown == true)
            {
                abilityCooldown -= Time.deltaTime;
            }
        }

        if (abilityCooldown <= 0f)
        {
            abilityDuration = 5f;
            isOnCooldown = false;
            abilityCooldown = 10f;
        }
        //dash cooldown
        if (dashCooldown > 0.0f)
            dashCooldown -= Time.deltaTime;
        //PASSIVE
        if (respawnZeusPassive)
            respawnZeusPassiveTimer += Time.deltaTime;
        if (respawnZeusPassiveTimer > 5f)
        {
            isPassiveZeus = true;
            respawnZeusPassive = false;
            respawnZeusPassiveTimer = 0f;
        }

        if(isPassiveZeus)
        {
            if (photonView.IsMine)
            {

                photonView.RPC("ZeusPassive", RpcTarget.AllViaServer, rigidbody.position);
                isPassiveZeus = false;
            }
        }
        //skills
        if (Input.GetMouseButtonDown(1) && photonView.IsMine)
        {

            switch (godName)
            {
                case "Zeus":
                    //Vector3 mousePosition = Input.mousePosition;
                    //mousePosition.z = transform.position.z; // Set the z coordinate to the camera's z position
                    //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        Vector3 worldPoint = hit.point;
                        photonView.RPC("useZeusAbility", RpcTarget.AllViaServer, worldPoint);
                    }
                    break;
                case "Aphrodite":

                    break;
                case "Hades":
                    
                    photonView.RPC("useHadesAbility", RpcTarget.AllViaServer, rigidbody.position);
                    break;
                case "Artemis":

                    break;
                case "Poseidon":
                    //look at
                    RaycastHit _hit;
                    Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(_ray, out _hit))
                    {
                        transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
                    }

                    photonView.RPC("usePoseidonAbility", RpcTarget.AllViaServer, rigidbody.position);
                    break;
                case "Hermes":
                    photonView.RPC("useHermesAbility", RpcTarget.AllViaServer, rigidbody.position);
                    break;
                case "Ares":
                    photonView.RPC("useAresAbility", RpcTarget.AllViaServer, rigidbody.position);
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
        switch (godName)
        {
            default:
                var bullet = Instantiate(projectileList[0], bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                break;
            case "Zeus":
                bullet = Instantiate(projectileList[0], bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                break;
            case "Aphrodite":
                bullet = Instantiate(projectileList[1], bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                break;
            case "Hades":
                bullet = Instantiate(projectileList[2], bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;

                break;
            case "Artemis":
                bullet = Instantiate(projectileList[3], bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                break;
            case "Poseidon":
                bullet = Instantiate(projectileList[4], bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                break;
            case "Hermes":
                bullet = Instantiate(projectileList[5], bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                break;
            case "Ares":
                bullet = Instantiate(projectileList[6], bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                break;
            case "Athena":
                bullet = Instantiate(projectileList[7], bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
                break;

        }
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
    [PunRPC]
    public void useZeusAbility(Vector3 position)
    {
        //position.y = rigidbody.position.y;
        if (photonView.IsMine)
        {
            GameObject zeusAbility = PhotonNetwork.Instantiate("ZeusSkill", position, Quaternion.identity) as GameObject;
            zeusAbility.transform.SetParent(rigidbody.transform, true);
        }
    }
    [PunRPC] // Zeus passive
    public void ZeusPassive(Vector3 position)
    {
      
            position.y = position.y + 0.2f;
            GameObject zeusPassive = PhotonNetwork.Instantiate("ZeusPassive", position, Quaternion.identity) as GameObject;
            zeusPassive.transform.parent = rigidbody.transform;
        
    }

    [PunRPC]
    public void usePoseidonAbility(Vector3 position)
    {
        var PoseidonAbility = Instantiate(PoseidonAbilityPrefab, PoseidonAbilitySpawnPoint.position, PoseidonAbilitySpawnPoint.rotation);
        PoseidonAbility.GetComponent<Rigidbody>().velocity = PoseidonAbilitySpawnPoint.forward * PoseidonAbilitySpeed;

    }

    [PunRPC]
    public void usePoseidonPassive(Vector3 position)
    {
        var PoseidonPassive = Instantiate(PoseidonPassivePrefab, PoseidonPassiveSpawnPoint.position, PoseidonPassiveSpawnPoint.rotation);

    }

    [PunRPC]
    public void useAresAbility(Vector3 position)
    {
        damage *= 1.5f;
    }

    [PunRPC]
    public void useHermesAbility(Vector3 position)
    {
        fRate /= 2f;
    }

    public void changePIconZeus()
    {
        zeusImage.SetActive(true);
        hadesImage.SetActive(false);
        aphroditeImage.SetActive(false);
        artemisImage.SetActive(false);
        hermesImage.SetActive(false);
        poseidonImage.SetActive(false);
        aresImage.SetActive(false);
        athenaImage.SetActive(false);
    }
    public void changePIconHades()
    {
        zeusImage.SetActive(false);
        hadesImage.SetActive(true);
        aphroditeImage.SetActive(false);
        artemisImage.SetActive(false);
        hermesImage.SetActive(false);
        poseidonImage.SetActive(false);
        aresImage.SetActive(false);
        athenaImage.SetActive(false);
    }

    public void changePIconAphrodite()
    {
        zeusImage.SetActive(false);
        hadesImage.SetActive(false);
        aphroditeImage.SetActive(true);
        artemisImage.SetActive(false);
        hermesImage.SetActive(false);
        poseidonImage.SetActive(false);
        aresImage.SetActive(false);
        athenaImage.SetActive(false);
    }

    public void changePIconArtemis()
    {
        zeusImage.SetActive(false);
        hadesImage.SetActive(false);
        aphroditeImage.SetActive(false);
        artemisImage.SetActive(true);
        hermesImage.SetActive(false);
        poseidonImage.SetActive(false);
        aresImage.SetActive(false);
        athenaImage.SetActive(false);
    }

    public void changePIconHermes()
    {
        zeusImage.SetActive(false);
        hadesImage.SetActive(false);
        aphroditeImage.SetActive(false);
        artemisImage.SetActive(false);
        hermesImage.SetActive(true);
        poseidonImage.SetActive(false);
        aresImage.SetActive(false);
        athenaImage.SetActive(false);
    }

    public void changePIconPoseidon()
    {
        zeusImage.SetActive(false);
        hadesImage.SetActive(false);
        aphroditeImage.SetActive(false);
        artemisImage.SetActive(false);
        hermesImage.SetActive(false);
        poseidonImage.SetActive(true);
        aresImage.SetActive(false);
        athenaImage.SetActive(false);
    }

    public void changePIconAres()
    {
        zeusImage.SetActive(false);
        hadesImage.SetActive(false);
        aphroditeImage.SetActive(false);
        artemisImage.SetActive(false);
        hermesImage.SetActive(false);
        poseidonImage.SetActive(false);
        aresImage.SetActive(true);
        athenaImage.SetActive(false);
    }

    public void changePIconAthena()
    {
        zeusImage.SetActive(false);
        hadesImage.SetActive(false);
        aphroditeImage.SetActive(false);
        artemisImage.SetActive(false);
        hermesImage.SetActive(false);
        poseidonImage.SetActive(false);
        aresImage.SetActive(false);
        athenaImage.SetActive(true);
    }

    void Attacking()
    {
        StartCoroutine(Attack());
    }


    IEnumerator Attack()
    {
        
        m_Animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(1f);
        m_Animator.SetBool("IsAttacking", false);

    }




}