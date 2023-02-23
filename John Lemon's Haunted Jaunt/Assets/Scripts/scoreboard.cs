using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class scoreboard : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPrefab;
    [SerializeField] GameObject playerManagerPrefab;

    private void Start()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            AddScoreboardItem(player);
        }
    }

    void AddScoreboardItem(Player player)
    {
        Score item = Instantiate(scoreboardItemPrefab, container).GetComponent<Score>();
        item.Initialize(player);
    }
}