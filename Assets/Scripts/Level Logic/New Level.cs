using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewLevel : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject[] towers;
    private bool generalSelected = false;
    public Button defaultButton;
    public string nameOfGen = null;

    public void Start()
    {
        playerData.activeModifier = Modifiers.None;
        defaultButton.Select();
        defaultButton.onClick.Invoke();

        for (int i = 0; i < 7; i++)
        {
            playerData.TowerPool[i] = towers[i];
        }
    }
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
            playerData.InitializeTowerUnlockArray(6);
            SceneManager.LoadScene(sceneName);
        }

    }

    public void runModifier(string modifierName)
    {
        // modifierName is passed from the checkboxes
        // Use that name to determine which modifier to apply
        // Modifiers is a bitwise enum, so we use bitwise operations to apply or remove them
        if(modifierName == "Money") {
            if(playerData.activeModifier.HasFlag(Modifiers.Money)) {
                playerData.activeModifier ^= Modifiers.Money;
            }
            else {
                playerData.activeModifier |= Modifiers.Money;
            }
        } 
        else if(modifierName == "Morale"){
            if(playerData.activeModifier.HasFlag(Modifiers.Morale)) {
                playerData.activeModifier ^= Modifiers.Morale;
            }
            else {
                playerData.activeModifier |= Modifiers.Morale;
            }
        }
        else if(modifierName == "Range"){
            if(playerData.activeModifier.HasFlag(Modifiers.Range)) {
                playerData.activeModifier ^= Modifiers.Range;
            }
            else {
                playerData.activeModifier |= Modifiers.Range;
            }
        }
        else if(modifierName == "Damage"){
            if(playerData.activeModifier.HasFlag(Modifiers.Damage)) {
                playerData.activeModifier &= ~Modifiers.Damage;
            }
            else {
                playerData.activeModifier |= Modifiers.Damage;
            }
        }
        else if(modifierName == "Cooldown"){
            if(playerData.activeModifier.HasFlag(Modifiers.Cooldown)) {
                playerData.activeModifier ^= Modifiers.Cooldown;
            }
            else {
                playerData.activeModifier |= Modifiers.Cooldown;
            }
        }
        else if(modifierName == "Developer"){
            if(playerData.activeModifier.HasFlag(Modifiers.Developer)) {
                playerData.activeModifier ^= Modifiers.Developer;
            }
            else {
                playerData.activeModifier |= Modifiers.Developer;
            }
        }
        else {
            Debug.Log("The " + modifierName + " modifier is not found or not yet implemented.");
        }

        //Debug.Log("Active Modifiers: " + playerData.activeModifier.ToString());
    }


    public void generalSelect(string generalName)
    {
        if(generalName == "Bee") {
            playerData.Towers[0] = towers[3];
            playerData.Towers[1] = towers[4];
            playerData.Towers[2] = towers[5];
            playerData.TowersObtained = 3;
            playerData.activeGeneral = Generals.Bee;
            generalSelected = true;
            nameOfGen = generalName;

        } 
        else if(generalName == "Ant"){
            playerData.Towers[0] = towers[6];
            playerData.Towers[1] = towers[7];
            playerData.Towers[2] = towers[8];
            playerData.TowersObtained = 3;
            playerData.activeGeneral = Generals.Ant;
            generalSelected = true;
            nameOfGen = generalName;
        }
        else if(generalName == "Wasp"){
            playerData.Towers[0] = towers[9];
            playerData.Towers[1] = towers[10];
            playerData.TowersObtained = 2;
            playerData.activeGeneral = Generals.Wasp;
            generalSelected = true;
            nameOfGen = generalName;
        }
        else if(generalName == "Dev"){
            playerData.Towers[0] = towers[0];
            playerData.Towers[1] = towers[1];
            playerData.Towers[2] = towers[2];
            playerData.Towers[3] = towers[11]; 
            playerData.Towers[4] = towers[12];
            playerData.Towers[5] = towers[13];
            playerData.Towers[6] = towers[14];
            playerData.Towers[7] = towers[15];
            playerData.TowersObtained = 8;
            playerData.activeGeneral = Generals.Dev;
            generalSelected = true;
            nameOfGen = generalName;
        }
        else {
            Debug.Log("The " + generalName + " general is not found or not yet implemented. Defaulting to Bee general.");
            playerData.Towers[0] = towers[0];
            playerData.Towers[1] = towers[1];
            playerData.Towers[2] = towers[2];
            playerData.TowersObtained = 3;
            playerData.activeGeneral = Generals.Bee;
            generalSelected = true;
        }
    }

    public string getGeneralName()
    {
        return nameOfGen;
    }
}
