using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void Play()
    {
        ScenesUtility.LoadGame();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
