using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SettingsLogic : MonoBehaviour, ISaveable
{
    public SaveLoadManager SaveLoadManager;
    public PauseMenu pauseMenu;
    public bool OnMainMenu = false;
    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;
    public Slider volumeSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    [Header("Game Settings")]
    //public TMP_Dropdown difficultyDropdown;
    [Header("Graphics Settings")]
    public Toggle fullscrenToggle;
    public Toggle vsyncToggle;
    public List<Vector2> resolutions = new List<Vector2>();
    private int selectedResolution;
    public TMP_Text resolutionLabel;

    void Start()
    {
        // Set Audio Settings
        volumeSlider.value = SettingsValues.gameVolume;
        musicSlider.value = SettingsValues.musicVolume;
        sfxSlider.value = SettingsValues.sfxVolume;

        fullscrenToggle.isOn = Screen.fullScreen;
        if (QualitySettings.vSyncCount == 0) vsyncToggle.isOn = false;
        else vsyncToggle.isOn = true;

        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == (int)resolutions[i].x && Screen.height == (int)resolutions[i].y)
            {
                foundRes = true;
                selectedResolution = i;
                UpdateResolutionLabel();
            }
        }

        if (!foundRes)
        {
            resolutions.Add(new Vector2(Screen.width, Screen.height));
            selectedResolution = resolutions.Count - 1;
            UpdateResolutionLabel();
        }
    }

    void Update()
    {
        //Debug.Log("slider value: " + volumeSlider.value);
        SettingsValues.gameVolume = (int)volumeSlider.value;
        SettingsValues.musicVolume = (int)musicSlider.value;
        SettingsValues.sfxVolume = (int)sfxSlider.value;

        if (SettingsValues.musicVolume == -20)
        {
            audioMixer.SetFloat("Music", -80);
        }
        else
        {
            audioMixer.SetFloat("Music", SettingsValues.musicVolume);
        }
        if (SettingsValues.gameVolume == -20)
        {
            audioMixer.SetFloat("Master", -80);
        }
        else
        {
            audioMixer.SetFloat("Master", SettingsValues.gameVolume);
        }
        if (SettingsValues.sfxVolume == -20)
        {
            audioMixer.SetFloat("SFX", -80);
        }
        else
        {
            audioMixer.SetFloat("SFX", SettingsValues.sfxVolume);
        }

    }
    public void SetMusicVolume(float volume)
    {
        //audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }


    public void ResLeft()
    {
        selectedResolution--;
        if (selectedResolution < 0) selectedResolution = resolutions.Count - 1;

        UpdateResolutionLabel();
    }

    public void ResRight()
    {
        Debug.Log("ResRight");
        selectedResolution++;
        if (selectedResolution >= resolutions.Count) selectedResolution = 0;

        UpdateResolutionLabel();
    }

    public void UpdateResolutionLabel()
    {
        Debug.Log("UpdateResolutionLabel");
        resolutionLabel.text = resolutions[selectedResolution].x.ToString() + "x" + resolutions[selectedResolution].y.ToString();
    }

    public void ApplyGraphicsSettings()
    {
        //Screen.fullScreen = fullscrenToggle.isOn;
        if (vsyncToggle.isOn) QualitySettings.vSyncCount = 1;
        else QualitySettings.vSyncCount = 0;

        Screen.SetResolution((int)resolutions[selectedResolution].x, (int)resolutions[selectedResolution].y, fullscrenToggle.isOn);
    }

    public void ExitSettings()
    {
        this.gameObject.SetActive(false);
        if (!OnMainMenu)
        {
            pauseMenu.ExitSettings();
        }
    }

    public object CaptureState()
    {
        return new SaveData
        {
            volumeSlider = volumeSlider,
            musicSlider = musicSlider,
            sfxSlider = sfxSlider,
            fullscrenToggle = fullscrenToggle,
            vsyncToggle = vsyncToggle
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        volumeSlider = saveData.volumeSlider;
        musicSlider = saveData.musicSlider;
        sfxSlider = saveData.sfxSlider;
        fullscrenToggle = saveData.fullscrenToggle;
        vsyncToggle = saveData.vsyncToggle;
    }

    [Serializable]
    private struct SaveData
    {
        public Slider volumeSlider;
        public Slider musicSlider;
        public Slider sfxSlider;
        public Toggle fullscrenToggle;
        public Toggle vsyncToggle;
    }
}