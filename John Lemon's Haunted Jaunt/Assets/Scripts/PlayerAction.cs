using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PlayerAction : MonoBehaviour
{
    private Rigidbody rigidbody;
    private PhotonView photonView;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    bool isGrounded;
    Vector3 Up;
    public float jumpforce = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        rigidbody = GetComponent<Rigidbody>();

        Up = new Vector3(0, 1, 0);    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && photonView.IsMine)
        {
            photonView.RPC("shootBullet", RpcTarget.AllViaServer, rigidbody.position);
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

    [PunRPC]
    public void shootBullet (Vector3 position)
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
    }

 
}