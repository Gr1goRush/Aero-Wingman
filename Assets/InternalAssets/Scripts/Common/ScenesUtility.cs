using UnityEngine.SceneManagement;

public static class ScenesUtility
{
    public static void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}