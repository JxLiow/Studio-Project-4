using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Cinemachine;
using System.Collections.Generic;

public class JLGameManager : MonoBehaviourPunCallbacks
{
    public static JLGameManager Instance = null;
    public Text InfoText;
    public GameObject wayPoints;
    public GameObject enemies;
    public GameEnding endingScript;
    public CinemachineVirtualCamera virtualCam;
    public CinemachineFreeLook freeCamera;
    public BasePlayerScript newPlayer;
    public string playerGodName;
    public float playerDamage;
    public float playerFireRate;
    public float playerSpeed;
    public GameObject spawnPoints;
    public static List<Vector3> spawnPositions = new List<Vector3>();
    public void Awake()
    {
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        playerGodName = PlayerPrefs.GetString("godname");
        playerDamage = PlayerPrefs.GetFloat("damage");
        playerSpeed = PlayerPrefs.GetFloat("speed");
        playerFireRate = PlayerPrefs.GetFloat("firerate");
        CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;
    }

    // Start is called before the first frame update
    public void Start()
    {
        Hashtable props = new Hashtable
            {
                {JLGame.PLAYER_LOADED_LEVEL, true}
            };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        spawnPoints = GameObject.FindGameObjectWithTag("SpawnPoint");

        for (int i = 0; i < spawnPoints.transform.childCount; i++)
        {
            spawnPositions.Add(spawnPoints.transform.GetChild(i).transform.position);
            Debug.Log("spawn pos:" + spawnPositions[i]);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();

        CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
    }

    #region PUN CALLBACKS

    public override void OnDisconnected(DisconnectCause cause)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DemoAsteroids-LobbyScene");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
        {
            
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        CheckEndOfGame();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey(JLGame.PLAYER_LIVES))
        {
            CheckEndOfGame();
            return;
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }


        // if there was no countdown yet, the master client (this one) waits until everyone loaded the level and sets a timer start
        int startTimestamp;
        bool startTimeIsSet = CountdownTimer.TryGetStartTime(out startTimestamp);

        if (changedProps.ContainsKey(JLGame.PLAYER_LOADED_LEVEL))
        {
            if (CheckAllPlayerLoadedLevel())
            {
                if (!startTimeIsSet)
                {
                    CountdownTimer.SetStartTime();
                }
            }
            else
            {
                // not all players loaded yet. wait:
                Debug.Log("setting text waiting for players! ", this.InfoText);
                InfoText.text = "Waiting for other players...";
            }
        }

    }

    #endregion

    private bool CheckAllPlayerLoadedLevel()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object playerLoadedLevel;

            if (p.CustomProperties.TryGetValue(JLGame.PLAYER_LOADED_LEVEL, out playerLoadedLevel))
            {
                if ((bool)playerLoadedLevel)
                {
                    continue;
                }
            }

            return false;
        }

        return true;
    }
    private void StartGame()
    {
        Debug.Log("StartGame!");

        Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
        //Vector3 position = new Vector3(-9.8f, 1.0f, 3.0f);
        //Vector3 position = new Vector3(19.0f, 19.0f, 16.0f);
        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        GameObject player = PhotonNetwork.Instantiate("JohnLemon", position, rotation, 0);

        if (player.GetComponent<PhotonView>().IsMine)
        {
            virtualCam.Follow = player.transform;
            virtualCam.LookAt = player.transform;
        }

        //if (PhotonNetwork.IsMasterClient)
        //{
        //    SpawnGhosts();
        //}
    }

    private void SpawnGhosts()
    {

        //Positions taken from the existing ghosts
        Vector3[] positionArray = new[] { new Vector3(-5.3f, 0f, -3.1f),
                                            new Vector3(1.5f, 0f, 4f),
                                            new Vector3(3.2f, 0f, 6.5f),
                                            new Vector3(7.4f, 0f, -3f)};

        //loop through the array position
        for (int i = 0; i < positionArray.Length; ++i)
        {
            // Create a ghost prefab
            GameObject obj = PhotonNetwork.InstantiateRoomObject("Ghost", positionArray[i], Quaternion.identity);
            obj.transform.parent = enemies.transform;

            //get the waypoint component
            WaypointPatrol patrolPoints = obj.GetComponent<WaypointPatrol>();

            //assign the values
            patrolPoints.waypoints.Add(wayPoints.transform.GetChild(i * 2));
            patrolPoints.waypoints.Add(wayPoints.transform.GetChild(i * 2 + 1));

            patrolPoints.StartAI();

            obj.GetComponentInChildren<GhostRPC>().gameEnding = endingScript;
        }
    }
    private void CheckEndOfGame()
    {
    }

    private void OnCountdownTimerIsExpired()
    {
        StartGame();
    }
    // Update is called once per frame
    void Update()
    {
       
        //Debug.Log("player class: " + playerGodName);
        //Debug.Log("player damage: " + playerDamage);
        //Debug.Log("player fire rate: " + playerFireRate);
        //Debug.Log("player speed: " + playerSpeed);
    }
}
