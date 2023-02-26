using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    public GameObject timerTextObj;
    public GameObject playerinfocanvas;
    public GameObject abilitycanvas;
    public GameObject scoreboardcanvas;
    public GameObject winnercanvas;

    [Header("Timer settings")]
    public float currentTime;
    public bool countDown;
    public bool isCountdownTimerExpired = false;

    void Start()
    {
        timerTextObj.SetActive(false);
        playerinfocanvas.SetActive(false);
        abilitycanvas.SetActive(false);
        scoreboardcanvas.SetActive(false);
        winnercanvas.SetActive(false);
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
}
