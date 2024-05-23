using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Masks");
    }

    public void OpenSettings()
    {
        Debug.Log("Settings button clicked");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
