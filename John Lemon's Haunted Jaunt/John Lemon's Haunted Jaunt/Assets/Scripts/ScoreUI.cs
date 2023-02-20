using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public ScoreRowUI rowUI;
    public ScoreManager scoreManager;

    void Start()
    {
        scoreManager.AddScore(new Score("nuts", 6));
        scoreManager.AddScore(new Score("ndsdad", 26));
        scoreManager.AddScore(new Score("balls", 66));

        var scores = scoreManager.GetHighScores().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUI, transform).GetComponent<ScoreRowUI>();
            row.playerRank.text = (i + 1).ToString();
            row.playerName.text = scores[i].playerName;
            row.playerScore.text = scores[i].ToString();
        }
    }
}
