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
    }
    public void OpenDataPanel(string ButtonName)
    {
        if (OpenClosed == false)
        {
            NewUpgradeWindow.SetActive(true);
            TowerUpgradeName = ButtonName;
            Panel();
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
            Panel();
            OpenClosed = true;
            ButtonNameHolder = ButtonName;
            OldUpgradeWindow = NewUpgradeWindow;
            return;
        }

        else if (OpenClosed)
        {
            NewUpgradeWindow.SetActive(false);
            OpenClosed = false;
            //OldUpgradeWindow = NewUpgradeWindow;
            return;
        }
    }

    public void Panelset(GameObject panel)
    {
        NewUpgradeWindow = panel;
    }

    public void PassPannelButtons(GameObject UpgradeButton)
    {
        UniqueUpgradeButton = UpgradeButton;
        UniqueUpgradeButton.SetActive(true);
    }

    private void Panel()
    {
        TMP_Text HeaderName = NewUpgradeWindow.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        HeaderName.text = "SELECT " + TowerUpgradeName + " UPGRADES";
    }

    public void ButtonPressed(TowerUpgradeData UpgradeData)
    {
        TowerUpgradeDataWindow.SetActive(true);
        Populator.PopulatePanel(UpgradeData);
    }

    public void SelectUpgrade()
    {
        if (storeTowerUpgradeData.TokensObtained > 0)
        {
            storeTowerUpgradeData.TokensObtained--;
        }
        else if (storeTowerUpgradeData.TokensObtained == 0)
        {
            Debug.Log("No tokens left to purchase upgrade");
            return;
        }
    }

    public void EndRun()
    {
        SceneManager.LoadScene("Main Menu");
    }
}