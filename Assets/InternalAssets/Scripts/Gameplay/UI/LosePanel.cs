using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePanel : MonoBehaviour
{
    public void Restart()
    {
        ScenesUtility.LoadGame();
    }

    public void Menu()
    {
        ScenesUtility.LoadMenu();
    }
}
