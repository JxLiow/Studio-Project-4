using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Score : MonoBehaviour
{
    public string playerName;
    public float playerScore;

    public Score(string playerName, float playerScore)
    {
        this.playerName = playerName;
        this.playerScore = playerScore;
    }
}
