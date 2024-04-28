using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsLogic : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public bool OnMainMenu = false;
    [Header("Audio Settings")]
    public Slider volumeSlider;
    public Slider musicSlider;
    [Header("Game Settings")]
    //public TMP_Dropdown difficultyDropdown;
    [Header("Graphics Settings")]
    public Toggle fullscrenToggle;
    public Toggle vsyncToggle;
    public List<Vector2> resolutions = new List<Vector2>();
    private int selectedResolution;
    public TMP_Text resolutionLabel;

    void Start(){
        // Set Audio Settings
        volumeSlider.value = SettingsValues.gameVolume;
        musicSlider.value = SettingsValues.musicVolume;
        //difficultyDropdown.value = SettingsValues.difficulty;

        fullscrenToggle.isOn = Screen.fullScreen;
        if (QualitySettings.vSyncCount == 0) vsyncToggle.isOn = false;
        else vsyncToggle.isOn = true;

        bool foundRes = false;
        for(int i = 0; i < resolutions.Count; i++)
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
        SettingsValues.gameVolume = (int)volumeSlider.value;
        SettingsValues.musicVolume = (int)musicSlider.value;
        //SettingsValues.difficulty = difficultyDropdown.value;
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
        if(!OnMainMenu) {
            pauseMenu.ExitSettings();
        }
    }
}