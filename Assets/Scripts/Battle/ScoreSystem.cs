using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    private int totalScore;
    private static ScoreSystem _instance;
    public static ScoreSystem Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            _instance.totalScore = 0;
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
    }

    public void MinusScore(int score)
    {
        totalScore -= score;
    }

    public int TotalScore()
    {
        return totalScore;
    }
}

