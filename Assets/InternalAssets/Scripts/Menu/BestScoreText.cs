using System.Collections;
using UnityEngine;
using TMPro;

public class BestScoreText : MonoBehaviour
{

   [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        scoreText.text = "Best score: " + ScoreManager.Instance.BestScore.ToString();
    }
}