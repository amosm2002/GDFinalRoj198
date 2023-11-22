using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Toggle vsyncToggle;
    public Toggle fullscreenToggle;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider soundEffectsSlider;
    public AudioMixer audioMixer;

    private Resolution[] resolutions;

    private static SettingsManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        InitializeResolutions();
        LoadSettings();
    }

    private void InitializeResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionDropdown.options.Add(new Dropdown.OptionData(option));

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.value = currentResolutionIndex;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVsync(bool isVsync)
    {
        QualitySettings.vSyncCount = isVsync ? 1 : 0;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetMasterVolume(float volume)
    {
        float dbVolume = Mathf.Lerp(-80.0f, 0.0f, volume);
        audioMixer.SetFloat("MasterVolume", dbVolume);
    }

    public void SetMusicVolume(float volume)
    {
        float dbVolume = Mathf.Lerp(-80.0f, 0.0f, volume);
        audioMixer.SetFloat("MusicVolume", dbVolume);
    }

    public void SetSoundEffectsVolume(float volume)
    {
        float dbVolume = Mathf.Lerp(-80.0f, 0.0f, volume);
        audioMixer.SetFloat("SoundEffectsVolume", dbVolume);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("Vsync", vsyncToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SoundEffectsVolume", soundEffectsSlider.value);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", 0);
        vsyncToggle.isOn = PlayerPrefs.GetInt("Vsync", 0) == 1;
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 0) == 1;
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0);
        soundEffectsSlider.value = PlayerPrefs.GetFloat("SoundEffectsVolume", 0);
    
        ApplyLoadedSettings();
    }

    private void ApplyLoadedSettings()
    {
        SetResolution(resolutionDropdown.value);
        SetVsync(vsyncToggle.isOn);
        SetFullscreen(fullscreenToggle.isOn);
        SetMasterVolume(masterVolumeSlider.value);
        SetMusicVolume(musicVolumeSlider.value);
        SetSoundEffectsVolume(soundEffectsSlider.value);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu"); 
    }
}
