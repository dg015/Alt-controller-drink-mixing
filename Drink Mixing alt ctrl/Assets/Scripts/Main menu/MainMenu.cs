using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void loadGameButton()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void loadSettingsButton()
    {
        Debug.LogWarning("Settings scene not setup yet");
        //SceneManager.LoadScene("Main Game");
    }

    public void loadCreditsButton()
    {
        //Debug.LogWarning("Credits scene not setup yet");
        SceneManager.LoadScene("Credits");
    }


    public void quitGameButton()
    {
        Application.Quit();
    }

    public void mainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");

    }
}
