using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    public bool SettingsStatus = false;
    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;

    SaveLoadManager saveLoadManager;

    void Start()
    {
        saveLoadManager = GameObject.FindObjectOfType<SaveLoadManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SettingsStatus == false)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Pause ()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadSettings()
    {
        Debug.Log("Loading Settings......");
        PauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(true);
        SettingsStatus = true;
    }

    public void ExitSettings()
    {
        Debug.Log("Exiting Settings......");
        PauseMenuUI.SetActive(true);
        SettingsMenuUI.SetActive(false);
        SettingsStatus = false;
    }

    public void QuittoMainMenu ()
    {
        Time.timeScale = 1.0f;
        Debug.Log("Quitting to Main Menu......");
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartLevel ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }

    public void SaveGame ()
    {
        saveLoadManager.Save();
    }

    public void LoadGame()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded");
        Time.timeScale = 1.0f;
        GameIsPaused = false;
        saveLoadManager.Load();
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to the event to avoid multiple calls
    }

    public void WorldMap()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("World Map");
    }
}