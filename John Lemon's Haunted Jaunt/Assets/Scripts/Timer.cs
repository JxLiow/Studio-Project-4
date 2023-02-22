using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    public GameObject timerTextObj;
    public GameObject playerinfocanvas;
    public GameObject abilitycanvas;
    public GameObject scoreboardcanvas;
    public GameObject scoretext;

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
        scoretext.SetActive(false);
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
            scoretext.SetActive(true);
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
            currentTime = 0;
        }
    }
}
