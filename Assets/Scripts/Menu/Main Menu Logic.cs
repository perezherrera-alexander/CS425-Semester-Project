using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public bool SettingsStatus = false;
    public GameObject SettingsMenuUI;
    // public Slider volumeSlider;
    // public Slider musicSlider;
    // public TMP_Dropdown difficultyDropdown;
    public PlayerData playerData;
    public SaveLoadManager saveLoadManager;
    public GameObject[] TutorialTowers;
    private string SavePath => $"{Application.persistentDataPath}/RunSave.txt";
    public GameObject LoadSaveButton;
    public string temptstringname;

    public GameObject UI;

    void Start(){
        if (File.Exists(SavePath)) LoadSaveButton.SetActive(true);
        // if(volumeSlider != null) volumeSlider.value = SettingsValues.gameVolume;
        // else Debug.Log("Volume Slider is null");
        // if(musicSlider != null) musicSlider.value = SettingsValues.musicVolume;
        // else Debug.Log("Music Slider is null");
        // if(difficultyDropdown != null) difficultyDropdown.value = SettingsValues.difficulty;
        // else Debug.Log("Difficulty Dropdown is null");
        playerData.InitializeWorldsCompletedArray(0);
        playerData.InitializeWorldsCompletedArray(100);
        playerData.InitializeTowersArray(0);
        playerData.InitializeTowersArray(20);
        playerData.InitializeTowerPoolArray(0);
        playerData.InitializeTowerPoolArray(20);
        playerData.InitializeTowerUnlockOrderArray(0);
        playerData.InitializeTowerUnlockOrderArray(6);
        playerData.InitializeRoadPathTakenArray(0);
        playerData.InitializeRoadPathTakenArray(20);
        playerData.InitializeTowerUnlockArray(0);
        playerData.InitializeTowerUnlockArray(6);
        playerData.InitializeHealthUnlockArray(0);
        playerData.InitializeHealthUnlockArray(6);
        playerData.InitializeMoneyUnlockArray(0);
        playerData.InitializeMoneyUnlockArray(6);
        playerData.Saving = false;
        playerData.PathsVisited = new List<string>();
        playerData.LevelLoaded = false;
    }

    void Update()
    {
        // SettingsValues.gameVolume = (int)volumeSlider.value;
        // SettingsValues.musicVolume = (int)musicSlider.value;
        // SettingsValues.difficulty = difficultyDropdown.value;
    }
    public void goToScene(string sceneName)
    {
        temptstringname = sceneName;
        if (File.Exists(SavePath))
        {
            UI.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }

    }
    public void HideUI()
    {
        UI.SetActive (false);
    }

    public void DeleteOldSaveStartNewRun()
    {
        File.Delete(SavePath);
        goToScene(temptstringname);
    }

    public void quitApplication()
    {
        Application.Quit();
        Debug.Log("Application has quit");
    }

    public void LoadSettings()
    {
        Debug.Log("Loading Settings......");
        SettingsMenuUI.SetActive(true);
        SettingsStatus = true;
    }

    public void ExitSettings()
    {
        Debug.Log("Exiting Settings......");
        SettingsMenuUI.SetActive(false);
        SettingsStatus = false;
    }

    public void LoadWorldMapSave()
    {
        Debug.Log("Load World Map Save......");
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        SceneManager.LoadScene("Game View");
    }

    public void LoadTutorialWorld(){
        playerData.Towers[0] = TutorialTowers[0];
        playerData.Towers[1] = TutorialTowers[1];
        playerData.Towers[2] = TutorialTowers[2];
        playerData.TowersObtained = 3;
        playerData.activeGeneral = Generals.Bee;
        //generalSelected = true;
        //nameOfGen = generalName;
        SceneManager.LoadScene("Tutorial World");
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded");
        saveLoadManager.Load();
        playerData.LevelLoaded = true;
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to the event to avoid multiple calls
    }
}
