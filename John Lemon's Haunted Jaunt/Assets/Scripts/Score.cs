using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Score : MonoBehaviourPunCallbacks
{
    public TMP_Text username;
    public TMP_Text score;

    Player player;

    public void Initialize(Player player)
    {
        this.player = player;

        username.text = player.NickName;
        UpdateStats();
    }

    void UpdateStats()
    {
        if(player.CustomProperties.TryGetValue("kills", out object kills))
        {
            score.text = kills.ToString();
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if(targetPlayer == player)
        {
            if(changedProps.ContainsKey("kills"))
            {
                UpdateStats();
            }
        }
    }
}
