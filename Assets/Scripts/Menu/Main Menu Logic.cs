using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public bool SettingsStatus = false;
    public GameObject SettingsMenuUI;
    public Slider volumeSlider;
    public Slider musicSlider;
    public TMP_Dropdown difficultyDropdown;
    public PlayerData playerData;
    public SaveLoadManager saveLoadManager;
    public GameObject[] TutorialTowers;
    void Start(){
        if(volumeSlider != null) volumeSlider.value = SettingsValues.gameVolume;
        else Debug.Log("Volume Slider is null");
        if(musicSlider != null) musicSlider.value = SettingsValues.musicVolume;
        else Debug.Log("Music Slider is null");
        if(difficultyDropdown != null) difficultyDropdown.value = SettingsValues.difficulty;
        else Debug.Log("Difficulty Dropdown is null");
        playerData.InitializeTowersArray(0);
        playerData.InitializeTowersArray(20);
        playerData.InitializeTowerPoolArray(0);
        playerData.InitializeTowerPoolArray(20);
        playerData.InitializeTowerUnlockOrderArray(0);
        playerData.InitializeTowerUnlockOrderArray(6);
    }

    void Update()
    {
        SettingsValues.gameVolume = (int)volumeSlider.value;
        SettingsValues.musicVolume = (int)musicSlider.value;
        SettingsValues.difficulty = difficultyDropdown.value;
    }
    public void goToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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
        SceneManager.LoadScene("World Map Generation");
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
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to the event to avoid multiple calls
    }
}
