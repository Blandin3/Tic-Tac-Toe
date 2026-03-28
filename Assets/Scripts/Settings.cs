using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider aiDifficultySlider;
    public Slider brightnessSlider;
    public Toggle soundToggle;
    public Image brightnessOverlay;

    void Start()
    {
        aiDifficultySlider.minValue = 1;
        aiDifficultySlider.maxValue = 3;
        aiDifficultySlider.wholeNumbers = true;

        // Remove listeners before setting values to avoid firing events during init
        aiDifficultySlider.onValueChanged.RemoveAllListeners();
        brightnessSlider.onValueChanged.RemoveAllListeners();
        soundToggle.onValueChanged.RemoveAllListeners();

        aiDifficultySlider.value = GameSettings.AIDifficulty;
        brightnessSlider.value = GameSettings.Brightness;
        soundToggle.isOn = GameSettings.SoundEnabled;
        AudioListener.volume = GameSettings.SoundEnabled ? 1f : 0f;
        ApplyBrightness(GameSettings.Brightness);

        // Add listeners after values are set
        aiDifficultySlider.onValueChanged.AddListener(v => GameSettings.AIDifficulty = Mathf.RoundToInt(v));
        brightnessSlider.onValueChanged.AddListener(v => { GameSettings.Brightness = v; ApplyBrightness(v); });
        soundToggle.onValueChanged.AddListener(OnSoundToggled);
    }

    void OnSoundToggled(bool isOn)
    {
        GameSettings.SoundEnabled = isOn;
        AudioListener.volume = isOn ? 1f : 0f;
    }

    void ApplyBrightness(float value)
    {
        if (brightnessOverlay == null) return;
        Color c = brightnessOverlay.color;
        c.a = 1f - value;
        brightnessOverlay.color = c;
    }

    public void OnBackClicked()
    {
        SceneManager.LoadScene("HomeScreen");
    }

}
