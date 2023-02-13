using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GhostRPC : MonoBehaviour
{
    public GameEnding gameEnding;

    // Start is called before the first frame update
    [PunRPC]
    public void CallCaughtPlayer()
    {
        if(!gameEnding)
            gameEnding = GameObject.Find("GameEnding").GetComponentInChildren<GameEnding>();
        gameEnding.CaughtPlayer();
    }
}
