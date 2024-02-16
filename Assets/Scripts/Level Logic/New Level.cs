using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevel : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject[] towers;
    public void goToScene(string sceneName)
    {
        playerData.Morale = 100;
        playerData.EvolutionPoints = 20;
        playerData.EnemiesKilled = 0;
        playerData.NumberOfWorldsCompleted = 0;
        playerData.CurrentWorld = "0,3";
        playerData.InitializeWorldsCompletedArray(100);
        SceneManager.LoadScene(sceneName);
    }

    public void runModifier(string modifierName)
    {

    }

    public void generalSelect(string generalName)
    {
        if(generalName == "Bee") {
            playerData.Towers[0] = towers[0];
            playerData.Towers[1] = towers[1];
            playerData.Towers[2] = towers[2];
        } 
        else if(generalName == "Wasp"){
            playerData.Towers[0] = towers[3];
            playerData.Towers[1] = towers[4];
            playerData.Towers[2] = towers[5];
        }
        else {
            Debug.Log("General not found or not yet implemented. Falling back to default towers.");
            playerData.Towers[0] = towers[0];
            playerData.Towers[1] = towers[1];
            playerData.Towers[2] = towers[2];

        }
    }
}
