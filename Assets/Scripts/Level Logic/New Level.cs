using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NewLevel : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject[] towers;
    private bool generalSelected = false;
    public Button defaultButton;
    public string nameOfGen = null;
    public TMP_Dropdown difficultyDropdown;
    public Toggle HalfMoney;
    public Toggle DoubleMoney;
    public Toggle HardcoreMode;
    public Toggle HalfHealth;
    public Toggle DoubleHealth;

    public void Start()
    {
        playerData.activeModifier = Modifiers.None;
        difficultyDropdown.value = SettingsValues.difficulty;
        defaultButton.Select();
        defaultButton.onClick.Invoke();
        int count = 0;
        for (int i = 11; i < 18; i++)
        {
            playerData.TowerPool[count] = towers[i];
            count++;
        }
    }

    void Update()
    {
        SettingsValues.difficulty = difficultyDropdown.value;
    }
    public void goToScene(string sceneName)
    {
        if(!generalSelected) {
            Debug.Log("You shouldn't be seeing this message. Please select a general before continuing.");
        }
        else{
            playerData.Morale = 100;
            if(playerData.activeModifier.HasFlag(Modifiers.Morale)) {
                playerData.Morale = 1;
            }
            else if(playerData.activeModifier.HasFlag(Modifiers.HalfMorale)) {
                playerData.Morale = 50;
            }
            else if(playerData.activeModifier.HasFlag(Modifiers.DoubleMorale)) {
                playerData.Morale = 200;
            }
            playerData.EvolutionPoints = 20;
            if(playerData.activeModifier.HasFlag(Modifiers.Money)) {
                playerData.EvolutionPoints = 1000;
            }
            playerData.EnemiesKilled = 0;
            playerData.NumberOfWorldsCompleted = 0;
            //playerData.CurrentWorld = "0,3";
            playerData.CurrentWorld = "0,2";
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
        if(modifierName == "Money") { // Infinite Money
            if(playerData.activeModifier.HasFlag(Modifiers.Money)) {
                playerData.activeModifier ^= Modifiers.Money;
            }
            else {
                playerData.activeModifier |= Modifiers.Money;
            }
        } 
        else if(modifierName == "Morale"){ // Hardcore mode
            if(playerData.activeModifier.HasFlag(Modifiers.Morale)) {
                playerData.activeModifier ^= Modifiers.Morale;
            }
            else {
                playerData.activeModifier |= Modifiers.Morale;
                CheckForConflicts(Modifiers.Morale);
            }
        }
        else if(modifierName == "Range"){ // Not implemented
            if(playerData.activeModifier.HasFlag(Modifiers.Range)) {
                playerData.activeModifier ^= Modifiers.Range;
            }
            else {
                playerData.activeModifier |= Modifiers.Range;
            }
        }
        else if(modifierName == "Damage"){ // Not implemented
            if(playerData.activeModifier.HasFlag(Modifiers.Damage)) {
                playerData.activeModifier &= ~Modifiers.Damage;
            }
            else {
                playerData.activeModifier |= Modifiers.Damage;
            }
        }
        else if(modifierName == "Cooldown"){ // Not implemented
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
        else if(modifierName == "HalfHealth"){
            if(playerData.activeModifier.HasFlag(Modifiers.HalfMorale)) {
                playerData.activeModifier ^= Modifiers.HalfMorale;
            }
            else {
                playerData.activeModifier |= Modifiers.HalfMorale;
                CheckForConflicts(Modifiers.HalfMorale);
            }
        }
        else if(modifierName == "DoubleHealth"){
            if(playerData.activeModifier.HasFlag(Modifiers.DoubleMorale)) {
                playerData.activeModifier ^= Modifiers.DoubleMorale;
            }
            else {
                playerData.activeModifier |= Modifiers.DoubleMorale;
                CheckForConflicts(Modifiers.DoubleMorale);
            }
        }
        else if(modifierName == "HalfMoney"){
            if(playerData.activeModifier.HasFlag(Modifiers.HalfMoney)) {
                playerData.activeModifier ^= Modifiers.HalfMoney;
            }
            else {
                playerData.activeModifier |= Modifiers.HalfMoney;
                CheckForConflicts(Modifiers.HalfMoney);
            }
        }
        else if(modifierName == "DoubleMoney"){
            if(playerData.activeModifier.HasFlag(Modifiers.DoubleMoney)) {
                playerData.activeModifier ^= Modifiers.DoubleMoney;
            }
            else {
                playerData.activeModifier |= Modifiers.DoubleMoney;
                CheckForConflicts(Modifiers.DoubleMoney);
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
            playerData.Towers[8] = towers[16];
            playerData.Towers[9] = towers[17];
            playerData.TowersObtained = 10;
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

    public void CheckForConflicts(Modifiers modifiers)
    {
        if(modifiers == Modifiers.Morale) {
            //Debug.Log("Checking for conflicts with Hardcore Mode");
            if(playerData.activeModifier.HasFlag(Modifiers.HalfMorale)) {
                //playerData.activeModifier ^= Modifiers.HalfMorale;
                HalfHealth.isOn = false;
            }
            if(playerData.activeModifier.HasFlag(Modifiers.DoubleMorale)) {
                //playerData.activeModifier ^= Modifiers.DoubleMorale;
                DoubleHealth.isOn = false;
            }
        }
        else if(modifiers == Modifiers.HalfMorale) {
            //Debug.Log("Checking for conflicts with Half Health Mode");
            if(playerData.activeModifier.HasFlag(Modifiers.Morale)) {
                //playerData.activeModifier ^= Modifiers.Morale;
                HardcoreMode.isOn = false;
                //Debug.Log("Disabling Hardcore Mode");
            }
            if(playerData.activeModifier.HasFlag(Modifiers.DoubleMorale)) {
                //playerData.activeModifier ^= Modifiers.DoubleMorale;
                DoubleHealth.isOn = false;
                //Debug.Log("Disabling Double Health Mode");
            }
        }
        else if(modifiers == Modifiers.DoubleMorale) {
            //Debug.Log("Checking for conflicts with Double Health Mode");
            if(playerData.activeModifier.HasFlag(Modifiers.Morale)) {
                //playerData.activeModifier ^= Modifiers.Morale;
                HardcoreMode.isOn = false;
            }
            if(playerData.activeModifier.HasFlag(Modifiers.HalfMorale)) {
                //playerData.activeModifier ^= Modifiers.HalfMorale;
                HalfHealth.isOn = false;
            }
        }

        if(modifiers == Modifiers.HalfMoney) {
            if(playerData.activeModifier.HasFlag(Modifiers.DoubleMoney)) {
                DoubleMoney.isOn = false;
            }
        }
        else if(modifiers == Modifiers.DoubleMoney) {
            if(playerData.activeModifier.HasFlag(Modifiers.HalfMoney)) {
                HalfMoney.isOn = false;
            }
        }
    }
}
