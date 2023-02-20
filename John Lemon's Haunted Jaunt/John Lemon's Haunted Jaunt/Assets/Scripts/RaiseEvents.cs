using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

public class RaiseEvents : MonoBehaviour
{
    public const byte BOMBTRIGGER = 1;
    public const byte BOMBHIT = 2;

    public delegate void OnExplode(string position);
    public static event OnExplode ExplodeEvent;

    public delegate void OnHit(int targetID);
    public static event OnHit HitEvent;

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if(eventCode == BOMBTRIGGER)
        {
            ExplodeEvent?.Invoke(photonEvent.CustomData.ToString());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
