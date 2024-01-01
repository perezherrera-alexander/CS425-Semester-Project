using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevel : MonoBehaviour
{
    public PlayerData playerData;
    public void goToScene(string sceneName)
    {
        playerData.Morale = 100;
        playerData.EvolutionPoints = 20;
        playerData.EnemiesKilled = 0;
        playerData.NumberOfWorldsCompleted = 0;
        playerData.CurrentWorld = "World1";
        playerData.InitializeWorldsCompletedArray(100);
        SceneManager.LoadScene(sceneName);
    }

    public void runModifier(string modifierName)
    {

    }

    public void generalSelect(string generalName)
    {

    }
}
