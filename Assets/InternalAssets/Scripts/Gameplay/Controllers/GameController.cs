using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public Plane Plane => plane;

    public bool GameIsActive { get; private set; }
    public int CurrentScore { get; private set; }

    public event Action OnScoreChanged;

    [SerializeField] private Plane plane;

    protected override void Awake()
    {
        base.Awake();

        GameIsActive = false;
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        GameIsActive = true;
        CurrentScore = 0;
        OnScoreChanged?.Invoke();

        UIController.Instance.ShowGamePanel();
        HoopsController.Instance.StartSpawning();
        FuelController.Instance.Activate();

        plane.StartFlying();
    }

    public void AddScore()
    {
        CurrentScore++;
        ScoreManager.Instance.TryUpdateBestScore(CurrentScore);

        OnScoreChanged?.Invoke();
    }

    public void Lose()
    {
        if (!GameIsActive)
        {
            return;
        }

        SoundsController.Instance.PlayFail();

        UIController.Instance.ShowLosePanel();
        GameIsActive = false;

        plane.StopFlying();
        SoundsController.Instance.StopAllSounds();
    }
}
