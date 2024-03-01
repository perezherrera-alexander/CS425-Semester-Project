using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewLevel : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject[] towers;
    private bool generalSelected = false;
    public void goToScene(string sceneName)
    {
        if(!generalSelected) {
            Debug.Log("You shouldn't be seeing this message. Please select a general before continuing.");
        }
        else{
            playerData.Morale = 100;
            playerData.EvolutionPoints = 20;
            playerData.EnemiesKilled = 0;
            playerData.NumberOfWorldsCompleted = 0;
            playerData.CurrentWorld = "0,3";
            playerData.InitializeWorldsCompletedArray(100);
            SceneManager.LoadScene(sceneName);
        }

    }

    public void runModifier(string modifierName)
    {
        if(modifierName == "Money") {
            playerData.activeModifier = Modifiers.Money;
        } 
        else if(modifierName == "Morale"){
            playerData.activeModifier = Modifiers.Morale;
        }
        else if(modifierName == "Range"){
            playerData.activeModifier = Modifiers.Range;
        }
        else if(modifierName == "Damage"){
            playerData.activeModifier = Modifiers.Damage;
        }
        else if(modifierName == "Cooldown"){
            playerData.activeModifier = Modifiers.Cooldown;
        }
        else {
            Debug.Log("Modifier not found or not yet implemented.");
            playerData.activeModifier = Modifiers.None;
        }
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
    }
}
