using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public int BestScore { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        BestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    public void TryUpdateBestScore(int _score)
    {
        if (_score > BestScore)
        {
            BestScore = _score;
            PlayerPrefs.SetInt("BestScore", BestScore);
        }
    }
}
