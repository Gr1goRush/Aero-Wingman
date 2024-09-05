using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    [SerializeField] private GameObject gamePanel, losePanel;

    public void ShowGamePanel()
    {
        gamePanel.SetActive(true);
    }

    public void ShowLosePanel()
    {
        gamePanel.SetActive(false);
        losePanel.SetActive(true);
    }

    public void Menu()
    {
        ScenesUtility.LoadMenu();
    }
}
