using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class scoreboard : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboardItemPrefab;
    public TextMeshProUGUI winner;

    Score[] item = new Score[4];
    int count;
    public string[] pName = new string[4];
    public int[] pScore = new int[4] { -1, -1, -1, -1 };
    public int highest;
    public int highestValue = -1;

    private void Start()
    {
        count = 0;
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            AddScoreboardItem(player);
        }
        
        for (int x = 0; x < count; ++x)
        {
            pName[x] = item[x].ReturnName();
        }
    }

    void AddScoreboardItem(Player player)
    {
        item[count] = Instantiate(scoreboardItemPrefab, container).GetComponent<Score>();
        item[count].Initialize(player);
        count++;
    }

    void Update()
    {
        for(int x = 0; x < count; ++x)
        {
            pScore[x] = item[x].ReturnKills();
            if(pScore[x] > highestValue)
            {
                highestValue = pScore[x];
                winner.SetText("Winner is " + pName[x]);
            }
        }
    }
}