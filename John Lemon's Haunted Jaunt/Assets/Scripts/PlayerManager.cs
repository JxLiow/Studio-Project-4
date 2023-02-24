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

	void Awake()
	{
		PV = GetComponent<PhotonView>();
	}

    void Update()
    {
		foreach (Player p in PhotonNetwork.PlayerList)
		{
			if(p.IsLocal)
            {
				p.Deaths = kills;
            }
		}
	}

    [PunRPC]
    public void getKill()
	{
		if(PV.IsMine)
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

	public static PlayerManager Find(Player player)
	{
		return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.PV.Owner == player);
	}
}
