using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevel : MonoBehaviour
{
    public void goToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void runModifier(string modifierName)
    {

    }

    public void generalSelect(string generalName)
    {

    }
}
