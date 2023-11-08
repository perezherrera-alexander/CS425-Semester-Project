using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void LoadSettings ()
    {
        Debug.Log("Loading Settings......");
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame ()
    {
        Debug.Log("Quitting game......");
        Application.Quit();
    }

    public void ExitSettings ()
    {
        Debug.Log("Loading Pause Menu......");
        SceneManager.LoadScene("Main");
    }
}
