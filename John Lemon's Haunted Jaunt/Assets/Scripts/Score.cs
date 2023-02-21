using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI highscore;

    public int number;

    void Start()
    {
        score.text = PlayerPrefs.GetInt("Score", 0).ToString();
        highscore.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ResetScore();
        }
    }

    public void UpdateScore()
    {
        number = Random.Range(1, 10);
        score.text = number.ToString();

        if (number > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", number);
            highscore.text = number.ToString();
        }
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("Highscore");
        highscore.text = "0";
    }
}
