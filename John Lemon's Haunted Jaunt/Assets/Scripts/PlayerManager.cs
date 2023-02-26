using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerManager : MonoBehaviour
{
	PhotonView PV;

	int kills;
	string name;

	void Awake()
	{
		PV = GetComponent<PhotonView>();
		kills = 0;
		name = PlayerPrefs.GetString("name", "");
		//Debug.Log("This PlayerManager belongs to " + name);
	}

    void Update()
    {
		PV.RPC("UpdateKills", RpcTarget.AllViaServer);
	}

    public void getKill()
	{
		PV.RPC(nameof(RPC_getKill), PV.Owner);
	}

	[PunRPC]
	void RPC_getKill()
	{
		kills++;

		Hashtable hash = new Hashtable();
		hash.Add("kills", kills);
		PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
	}

	[PunRPC]
	void UpdateKills()
    {
		foreach (Player p in PhotonNetwork.PlayerList)
		{
			if (p.NickName == name)
			{
				p.Score = kills;
				//Debug.Log(p.Score+" belongs to "+name);
			}
		}
    }

	public int showKills()
    {
		return kills;
    }

	public string getName()
    {
		return name;
    }

	public static PlayerManager Find(Player player)
	{
		return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.PV.Owner == player);
	}
}
