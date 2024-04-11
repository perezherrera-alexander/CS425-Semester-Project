using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectTowerToUpgrade : MonoBehaviour
{
    public GameObject OldUpgradeWindow;
    public GameObject NewUpgradeWindow;


    public GameObject SelectTowerUpgradePanel;
    public GameObject TowerUpgradeDataWindow;
    public GameObject UniqueUpgradeButton;
    public string TowerUpgradeName = "";

    public GameObject TowerUpgradeButton;
    public GameObject UpgradePickedParent;

    public TMP_Text TokenCounter;

    public DataPanelPopulator Populator;
    public StoreTowerUpgradeData storeTowerUpgradeData;

    public bool OpenClosed = false;
    public string ButtonNameHolder;

    public void Update()
    {
        TokenCounter.text = "Tokens Left: " + storeTowerUpgradeData.TokensObtained.ToString();
        if (storeTowerUpgradeData.TokensObtained == 0)
        {
            Debug.Log("No tokens left to purchase upgrade");
            SceneManager.LoadScene("Game View");
            return;
        }
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
            return;
        }

        else if (OpenClosed)
        {
            NewUpgradeWindow.SetActive(false);
            OpenClosed = false;
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

    public void EndRun()
    {
        SceneManager.LoadScene("Main Menu");
    }
}