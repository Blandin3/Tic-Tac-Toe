using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    [Header("Platform UI")]
    public GameObject quitButton;
    public Text platformLabel;

    void Start()
    {
        // Quit button is irrelevant on mobile/WebGL — hide it
        #if UNITY_ANDROID || UNITY_IOS
        if (quitButton != null) quitButton.SetActive(false);
        if (platformLabel != null) platformLabel.text = "Mobile";
        #elif UNITY_WEBGL
        if (quitButton != null) quitButton.SetActive(false);
        if (platformLabel != null) platformLabel.text = "WebGL";
        #else
        if (platformLabel != null) platformLabel.text = "PC";
        #endif
    }

    public void OnStartClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnSettingsClicked()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void OnQuitClicked()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE
        Application.Quit();
        #endif
    }
}
