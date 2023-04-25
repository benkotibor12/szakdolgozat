using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsController : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TextMeshProUGUI volumeTextValue;
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private TextMeshProUGUI verticalSensitivityTextValue;
    [SerializeField] private Slider verticalSensitivitySlider;
    [SerializeField] private TextMeshProUGUI horizontalSensitivityTextValue;
    [SerializeField] private Slider horizontalSensitivitySlider;

    [SerializeField] private TextMeshProUGUI brightnessTextValue;
    [SerializeField] private Slider brightnessSlider;

    [SerializeField] private TMP_Dropdown graphicsQualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private float audioVolume;
    private float horizontalSensitivity;
    private float verticalSensitivity;

    private int quailityValue;
    private float brightnessValue;
    private bool isFullScreen = true;
    private int resolution;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolution = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
    }

    public void ApplySettings()
    {
        DataPersistenceManager.Instance.SaveGame();
    }

    public void SetVerticalSensitivity(float sensitivity)
    {
        verticalSensitivity = sensitivity;
        verticalSensitivityTextValue.text = sensitivity.ToString("0");
    }

    public void SetHorizontalSensitivity(float sensitivity)
    {
        horizontalSensitivity = sensitivity;
        horizontalSensitivityTextValue.text = sensitivity.ToString("0");

    }

    public void SetVolume(float volume)
    {
        audioVolume = volume;
        volumeTextValue.text = volume.ToString("0");
        AudioListener.volume = audioVolume;
    }

    public void SetQuality(int qualityIndex)
    {
        quailityValue = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetBrightness(float brightness)
    {
        brightnessValue = brightness;
        brightnessTextValue.text = Mathf.Round(brightness*100).ToString("0");
    }

    public void SetFullScreen(bool fullscreen)
    {
        isFullScreen = fullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        resolution = resolutionIndex;
        Screen.SetResolution(resolutions[resolution].width, resolutions[resolution].height, isFullScreen);
    }

    public void LoadData(GameData gameData)
    {
        volumeTextValue.text = gameData.audioVolume.ToString("0");
        volumeSlider.value = gameData.audioVolume;
        verticalSensitivityTextValue.text = gameData.verticalSensitivity.ToString("0");
        verticalSensitivitySlider.value = gameData.verticalSensitivity;
        horizontalSensitivityTextValue.text = gameData.horizontalSensitivity.ToString("0");
        horizontalSensitivitySlider.value = gameData.horizontalSensitivity;
        brightnessTextValue.text = Mathf.Round(gameData.brightness * 100).ToString("0");
        brightnessSlider.value = gameData.brightness;
        resolutionDropdown.value = gameData.resolution;
        graphicsQualityDropdown.value = gameData.quaility;
        fullScreenToggle.isOn = gameData.isFullScreen;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.audioVolume = audioVolume;
        gameData.verticalSensitivity = verticalSensitivity;
        gameData.horizontalSensitivity = horizontalSensitivity;
        gameData.brightness = brightnessValue;
        gameData.isFullScreen = isFullScreen;
        gameData.quaility = quailityValue;
        gameData.resolution = resolution;
    }
}
