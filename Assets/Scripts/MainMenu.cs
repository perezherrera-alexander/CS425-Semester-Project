using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void goToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void quitApplication()
    {
        Application.Quit();
        Debug.Log("Application has quit");
    }
}
