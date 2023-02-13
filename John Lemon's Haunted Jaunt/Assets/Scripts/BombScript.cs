using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

public class BombScript : MonoBehaviour
{
    public bool isMine = false;
    public GameObject enemies;
    float time;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        time = 5.0f;
        enemies = GameObject.Find("Enemies");
    }

    private void OnEnable()
    {
        RaiseEvents.ExplodeEvent += Explode;
    }

    private void OnDisable()
    {
        RaiseEvents.ExplodeEvent -= Explode;
    }

    public void Explode(string data)
    {
        if(data == ""+transform.position)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMine)
            return;

        time -= Time.deltaTime;
        if(time <= 0.0f)
        {
            string dataSent = "" + transform.position;

            // You would have to set the Receivers to All in order to receive this event on the local client as well
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(RaiseEvents.BOMBTRIGGER, dataSent, raiseEventOptions, SendOptions.SendReliable);
            pos = transform.position;
        }
    }
}
