using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectTowerToUpgrade : MonoBehaviour
{
    public GameObject OldUpgradeWindow;
    public GameObject NewUpgradeWindow;
    public GameObject ButtonHolder;

    public GameObject SelectTowerUpgradePanel;
    public GameObject TowerUpgradeDataWindow;
    public GameObject UniqueUpgradeButton;
    public string TowerUpgradeName = "";

    public GameObject TowerUpgradeButton;
    public GameObject UpgradePickedParent;

    public GameObject StartRunButton;

    public TMP_Text TokenCounter;

    public DataPanelPopulator Populator;
    public StoreTowerUpgradeData storeTowerUpgradeData;
    public PlayerData playerData;

    public bool OpenClosed = false;
    public string ButtonNameHolder;

    private void Start()
    {
        if(playerData.activeGeneral == Generals.Bee)
        {
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(true);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(2).gameObject.SetActive(true);

            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(3).gameObject.SetActive(false);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(4).gameObject.SetActive(false);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(5).gameObject.SetActive(false);

            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(6).gameObject.SetActive(false);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(7).gameObject.SetActive(false);


        }
        else if(playerData.activeGeneral == Generals.Ant)
        {
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(2).gameObject.SetActive(false);

            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(3).gameObject.SetActive(true);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(4).gameObject.SetActive(true);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(5).gameObject.SetActive(true);

            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(6).gameObject.SetActive(false);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(7).gameObject.SetActive(false);
        }
        else if(playerData.activeGeneral == Generals.Wasp)
        {
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(2).gameObject.SetActive(false);

            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(3).gameObject.SetActive(false);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(4).gameObject.SetActive(false);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(5).gameObject.SetActive(false);

            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(6).gameObject.SetActive(true);
            transform.GetChild(2).GetChild(2).GetChild(1).GetChild(7).gameObject.SetActive(true);
        }
    }
    public void Update()
    {
        TokenCounter.text = "Tokens Left: " + storeTowerUpgradeData.TokensObtained.ToString();
    }
    public void OpenDataPanel(string ButtonName)
    {
        if (OpenClosed == false)
        {
            NewUpgradeWindow.SetActive(true);
            TowerUpgradeName = ButtonName;
            OpenClosed = true;
            ButtonNameHolder = ButtonName;
            OldUpgradeWindow = NewUpgradeWindow;
            StartRunButton.SetActive(false);
            return;
        }

        else if (ButtonNameHolder != ButtonName)
        {
            OldUpgradeWindow.SetActive(false);

            NewUpgradeWindow.SetActive(true);
            TowerUpgradeName = ButtonName;
            OpenClosed = true;
            ButtonNameHolder = ButtonName;
            OldUpgradeWindow = NewUpgradeWindow;
            StartRunButton.SetActive(false);
            return;
        }

        else if (OpenClosed)
        {
            NewUpgradeWindow.SetActive(false);
            OpenClosed = false;
            StartRunButton.SetActive(true);
            return;
        }
    }

    public void Panelset(GameObject panel)
    {
        NewUpgradeWindow = panel;
    }

    public void UpdateTokens()
    {
        if (storeTowerUpgradeData.TokensObtained > 0)
        {
            storeTowerUpgradeData.TokensObtained--;
        }
    }

    public void SelectUpgrade(GameObject Button)
    {
        if (storeTowerUpgradeData.TokensObtained > 0)
        {
            Destroy(Button);
        }
    }

    public void ShowNextUpgradeLevel(GameObject Button)
    {
        if (storeTowerUpgradeData.TokensObtained > 0)
        {
            Button.SetActive(true);
        }
    }

    // When player returns to this scene after the first time they've seen the buttons for upgrades they've bought should stay deleted so this function ensures its not visible
    public void CheckIfButtonShouldBeDeleted(GameObject Button)
    {
        int length = storeTowerUpgradeData.ListOfUpgradesObtained.Count;
        int count = 0;
        while (count < length)
        {
            if (Button == null)
            {
                return;
            }
            if (Button.name == storeTowerUpgradeData.ListOfUpgradesObtained[count])
            {
                Destroy(Button);
                ButtonHolder.SetActive(true);
            }
            if (ButtonHolder.name == storeTowerUpgradeData.ListOfUpgradesObtained[count])
            {
                Destroy(ButtonHolder);
            }
            count++;
        }
    }

    public void ButtonHolding(GameObject Button)
    {
        ButtonHolder = Button;
    }

    public void EndRun()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void StartRun()
    {
        GameObject[] TemptTowerArray = new GameObject[20];
        string general = playerData.activeGeneral.ToString();

        if (general == "Bee" || general == "Ant")
        {
            for (int i = 0; i < 3; i++)
            {
                TemptTowerArray[i] = playerData.Towers[i];
            }
            for (int i = 3; i < 20; i++)
            {
                TemptTowerArray[i] = null;
            }
            playerData.TowersObtained = 3;
        }
        else if (general == "Wasp")
        {
            for (int i = 0; i < 2; i++)
            {
                TemptTowerArray[i] = playerData.Towers[i];
            }
            for (int i = 2; i < 20; i++)
            {
                TemptTowerArray[i] = null;
            }
            playerData.TowersObtained = 2;
        }
        else if (general == "Dev")
        {
            for (int i = 0; i < 10; i++)
            {
                TemptTowerArray[i] = playerData.Towers[i];
            }
            for (int i = 10; i < 20; i++)
            {
                TemptTowerArray[i] = null;
            }
            playerData.TowersObtained = 10;
        }
        playerData.Towers = TemptTowerArray;

        playerData.Morale = 100;
        playerData.EvolutionPoints = 20;
        playerData.EnemiesKilled = 0;
        playerData.NumberOfWorldsCompleted = 0;
        playerData.CurrentWorld = "0,3";
        playerData.InitializeWorldsCompletedArray(100);
        playerData.InitializeTowerUnlockArray(6);
        playerData.InitializeTowerUnlockOrderArray(0);
        playerData.InitializeTowerUnlockOrderArray(6);

        SceneManager.LoadScene("Game View");
    }
}