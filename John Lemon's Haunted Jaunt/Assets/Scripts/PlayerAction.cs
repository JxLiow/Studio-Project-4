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

    PlayerHealth playerHealth;

    public int playerID;

    bool isGrounded;
    Vector3 Up;
    public float jumpforce = 1f;

    public float dashCooldown;
    Vector3 m_Movement;
    

    bool shoot = false;
    public string godName;
    public float fRate;
    public float damage;
    float time = 0, pElapsedTime = 0;
    public int score = 0;
    public bool isPoisoned;

    // ability stuff
    [Header("Ability Stuff")]
    public bool isActivated;
    public bool isOnCooldown;
    public float abilityDuration;
    public float abilityCooldown;
    public float PoseidonAbilitySpeed;
    public AudioSource AresAbilityAudio;
    public AudioSource ZeusAbilityAudio;

    bool isPassiveZeus = false;
    public bool respawnZeusPassive = false;
    float respawnZeusPassiveTimer = 0f;
    Timer timer;
    int PoseidonPassiveCount;

    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        timer = FindObjectOfType<Timer>();
        playerHealth = GetComponent<PlayerHealth>();

        playerID = photonView.ViewID;
        Up = new Vector3(0, 1, 0);
        godName = PlayerPrefs.GetString("godname", "Zeus");
        Debug.Log("god = "+godName);
        name = PlayerPrefs.GetString("name", "");
        Debug.Log("name = "+name);
        fRate = PlayerPrefs.GetFloat("firerate", 1);
        //Debug.Log("firerate = "+fRate);
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

        PoseidonAbilitySpeed = 8f;
        PoseidonPassiveCount = 0;
    }

    

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && photonView.IsMine && playerHealth.getHealth() >= 0)
        {
            m_Animator.SetBool("IsAttacking", true);
            shoot = true;
        }
        else if(Input.GetMouseButtonUp(0) && photonView.IsMine )
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
                switch (godName)
                {
                    case "Zeus":
                        photonView.RPC("shootZeusBullet", RpcTarget.AllViaServer, rigidbody.position);
                        pElapsedTime = time;
                        break;
                    case "Aphrodite":
                        photonView.RPC("shootAphroditeBullet", RpcTarget.AllViaServer, rigidbody.position);
                        pElapsedTime = time;
                        break;
                    case "Hades":
                        photonView.RPC("shootHadesBullet", RpcTarget.AllViaServer, rigidbody.position);
                        pElapsedTime = time;
                        break;
                    case "Artemis":
                        photonView.RPC("shootArtemisBullet", RpcTarget.AllViaServer, rigidbody.position);
                        pElapsedTime = time;
                        break;
                    case "Hermes":
                        photonView.RPC("shootHermesBullet", RpcTarget.AllViaServer, rigidbody.position);
                        pElapsedTime = time;
                        break;
                    case "Poseidon":
                        photonView.RPC("shootPoseidonBullet", RpcTarget.AllViaServer, rigidbody.position);
                        pElapsedTime = time;
                        break;
                    case "Ares":
                        photonView.RPC("shootAresBullet", RpcTarget.AllViaServer, rigidbody.position);
                        pElapsedTime = time;
                        break;
                    case "Athena":
                        photonView.RPC("shootAthenaBullet", RpcTarget.AllViaServer, rigidbody.position);
                        pElapsedTime = time;
                        break;
                }
            }
        }
        //skills
        if (Input.GetMouseButtonDown(1) && photonView.IsMine && playerHealth.getHealth() >= 0)
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
        if (isPoisoned)
        {
          
                photonView.RPC("HadesPassive", RpcTarget.AllViaServer, rigidbody.position);
                isPoisoned = false;
            
        }
        if ((godName == "Poseidon") && (photonView.IsMine))
        {
            if (timer.currentTime < 299.5)
            {
                if (PoseidonPassiveCount < 3)
                {
                    PoseidonPassiveCount += 1;
                }
                else if (PoseidonPassiveCount == 3)
                {
                    photonView.RPC("usePoseidonPassive", RpcTarget.AllViaServer, rigidbody.position);
                    PoseidonPassiveCount = 0;
                }
            }
        }
        //skills
        if (Input.GetMouseButtonDown(1) && photonView.IsMine && playerHealth.getHealth() >= 0)
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
                        if (isActivated == false && isOnCooldown == false)
                        {
                            isActivated = true;
                            photonView.RPC("useZeusAbility", RpcTarget.AllViaServer, worldPoint);
                        }
                    }
                    ZeusAbilityAudio.Play();
                    break;
                case "Aphrodite":
                    if (isActivated == false && isOnCooldown == false)
                    {
                        isActivated = true;
                        photonView.RPC("useAphroditeAbility", RpcTarget.AllViaServer, rigidbody.position);
                    }
                    break;
                case "Hades":
                    if (isActivated == false && isOnCooldown == false)
                    {
                        isActivated = true;
                        photonView.RPC("useHadesAbility", RpcTarget.AllViaServer, rigidbody.position);
                    }
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

                    if (isActivated == false && isOnCooldown == false)
                    {
                        isActivated = true;
                        photonView.RPC("usePoseidonAbility", RpcTarget.AllViaServer, rigidbody.position);
                    }

                    break;
                case "Hermes":
                    isActivated = true;
                    photonView.RPC("useHermesAbility", RpcTarget.AllViaServer, rigidbody.position);
                    break;
                case "Ares":
                    isActivated = true;
                    photonView.RPC("useAresAbility", RpcTarget.AllViaServer, rigidbody.position);
                    AresAbilityAudio.Play();
                    break;
                case "Athena":

                    break;

            }
        }
        // reset hermes fire rate
        else if (abilityDuration <= 0f && fRate <= 0.125f)
        {
            fRate = 0.25f;
        }
        // reset ares damage
        else if (abilityDuration <= 0f && damage >= 15f)
        {
            damage = 10f;
        }
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    //PRIMARY FIRE
    [PunRPC]
    public void shootZeusBullet (Vector3 position)
    {
        var bullet = Instantiate(lightningPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
    [PunRPC]
    public void shootAphroditeBullet(Vector3 position)
    {
        var bullet = Instantiate(heartPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
    [PunRPC]
    public void shootHadesBullet(Vector3 position)
    {
        var bullet = Instantiate(skullPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
    [PunRPC]
    public void shootArtemisBullet(Vector3 position)
    {
        var bullet = Instantiate(arrowPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
    [PunRPC]
    public void shootHermesBullet(Vector3 position)
    {
        var bullet = Instantiate(featherPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
    [PunRPC]
    public void shootPoseidonBullet(Vector3 position)
    {
        var bullet = Instantiate(tridentPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
    [PunRPC]
    public void shootAresBullet(Vector3 position)
    {
        var bullet = Instantiate(swordPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }
    [PunRPC]
    public void shootAthenaBullet(Vector3 position)
    {
        var bullet = Instantiate(shieldPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
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
    [PunRPC] // Hades passive
    public void HadesPassive(Vector3 position)
    {
        position.y = position.y + 0.2f;
        if (photonView.IsMine && PlayerPrefs.GetString("godname") != "Hades")
        {
            GameObject hadesPassive = PhotonNetwork.Instantiate("HadesPassive", position, Quaternion.identity) as GameObject;
            hadesPassive.transform.parent = rigidbody.transform;
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
    public void useAphroditeAbility(Vector3 position)
    {
        if (isActivated == true)
        {
            position.y = position.y + 0.25f;
            if (photonView.IsMine)
            {
                GameObject aphroditeAbility = PhotonNetwork.Instantiate("AphroditeSkill", position, Quaternion.identity) as GameObject;
                aphroditeAbility.transform.parent = rigidbody.transform;
            }
        }
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
        if (isActivated == true)
        {
            damage *= 1.5f;
        }
    }

    [PunRPC]
    public void useHermesAbility(Vector3 position)
    {
        if (isActivated == true)
        {
            fRate /= 2f;
        }
    }

    public PhotonView getView()
    {
        return photonView;
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