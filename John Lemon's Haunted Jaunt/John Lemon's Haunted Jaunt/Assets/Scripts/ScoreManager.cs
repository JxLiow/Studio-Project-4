using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public List<Score> playerScores;
    public ScoreManager scoreData;

    void Awake()
    {
        var json = PlayerPrefs.GetString("scores", "{}");
        scoreData = JsonUtility.FromJson<ScoreManager>(json);
    }

    public IEnumerable<Score> GetHighScores()
    {
        return scoreData.playerScores.OrderByDescending(x => x.playerScore);
    }

    public void AddScore(Score score)
    {
        scoreData.playerScores.Add(score);
    }

    private void OnDestroy()
    {
        SaveScore();
    }

    public void SaveScore()
    {
        var json = JsonUtility.ToJson(scoreData);
        PlayerPrefs.SetString("scores", json);
    }

    public ScoreManager()
    {
        playerScores = new List<Score>();
    }
}
