using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    public GameObject timerTextObj;
    public GameObject playerinfocanvas;
    public GameObject abilitycanvas;
    public GameObject scoreboardcanvas;
    public GameObject winnercanvas;
    public TextMeshProUGUI winner;

    [Header("Timer settings")]
    public float currentTime;
    public bool countDown;
    public bool isCountdownTimerExpired = false;
    bool done;
    string player;
    int count;
    string[] pName = new string[4];
    int[] pScore = new int[4] { 99, 99, 99, 99 };
    int lowest;

    void Start()
    {
        timerTextObj.SetActive(false);
        playerinfocanvas.SetActive(false);
        abilitycanvas.SetActive(false);
        scoreboardcanvas.SetActive(false);
        winnercanvas.SetActive(false);
        done = false;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountdownTimerExpired == true)
        {
            timerTextObj.SetActive(true);
            playerinfocanvas.SetActive(true);
            abilitycanvas.SetActive(true);
            scoreboardcanvas.SetActive(true);
            StartCountdown();
        }
        SetTimerText();
        count = 0;
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            pName[count] = p.NickName;
            pScore[count] = p.Score;
            Debug.Log(pScore[count]);
            count++;
        }

        lowest = Mathf.Max(pScore[0], pScore[1], pScore[2], pScore[3]);

        if (lowest == pScore[0])
            winner.SetText("Winner is " + pName[0]);
        else if (lowest == pScore[1])
            winner.SetText("Winner is " + pName[1]);
        else if (lowest == pScore[2])
            winner.SetText("Winner is " + pName[2]);
        else if (lowest == pScore[3])
            winner.SetText("Winner is " + pName[3]);
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("0.0");
    }

    public void StartCountdown()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        if (currentTime <= 0)
        {
            //currentTime = 0;
            //setWinner();
            winnercanvas.SetActive(true);
            timerTextObj.SetActive(false);
            playerinfocanvas.SetActive(false);
            abilitycanvas.SetActive(false);
            scoreboardcanvas.SetActive(false);
        }
        if(currentTime <= -10)
        {
            PhotonNetwork.Disconnect();
            UnityEngine.SceneManagement.SceneManager.LoadScene("DemoAsteroids-LobbyScene");
        }
    }
    public void setWinner()
    {
        
    }
}
