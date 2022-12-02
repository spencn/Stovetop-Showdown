using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // ReSharper disable once InconsistentNaming
    public void startGame()
    {
        GameObject set = GameObject.Find("Settings");
        Settings settings = set.GetComponent<Settings>();

        int numActive = 0;

        for (int i = 0; i < 4; i++)
        {
            if (settings.charActivated[i])
            {
                numActive++;
            }
        }

        if (numActive > 1)
        {
            SceneManager.LoadScene("FoodArena");
        }
    }

    // ReSharper disable once InconsistentNaming
    public void quitGame()
    {
        Application.Quit();
    }

    public void exitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}