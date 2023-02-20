using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer settings")]
    public float currentTime;
    public bool countDown;
    public bool isCountdownTimerExpired = false;

    void Start()
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountdownTimerExpired == true)
        {
            StartCountdown();   
        }
        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("0.00");
    }

    public void StartCountdown()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
    }
}
