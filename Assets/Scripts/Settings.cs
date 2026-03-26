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
        aiDifficultySlider.value = GameSettings.AIDifficulty;

        brightnessSlider.minValue = 0f;
        brightnessSlider.maxValue = 1f;
        brightnessSlider.value = GameSettings.Brightness;

        soundToggle.isOn = GameSettings.SoundEnabled;

        ApplyBrightness(GameSettings.Brightness);

        aiDifficultySlider.onValueChanged.AddListener(v => GameSettings.AIDifficulty = Mathf.RoundToInt(v));
        brightnessSlider.onValueChanged.AddListener(v => { GameSettings.Brightness = v; ApplyBrightness(v); });
        soundToggle.onValueChanged.AddListener(v => { GameSettings.SoundEnabled = v; AudioListener.volume = v ? 1f : 0f; });
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
