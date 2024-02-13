using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public bool SettingsStatus = false;
    public GameObject SettingsMenuUI;
    public Slider volumeSlider;

    void Start(){
        if(volumeSlider != null)
        {
            volumeSlider.value = SettingsValues.gameVolume;
        }
        else
        {
            Debug.Log("Volume Slider is null");
        }
    }

    void Update()
    {
        SettingsValues.gameVolume = (int)volumeSlider.value;
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
}
