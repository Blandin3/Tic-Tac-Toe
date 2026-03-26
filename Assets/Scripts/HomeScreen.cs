using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
    public void OnStartClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnSettingsClicked()
    {
        SceneManager.LoadScene("Settings");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
