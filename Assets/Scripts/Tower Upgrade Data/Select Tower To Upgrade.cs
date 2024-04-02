using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectTowerToUpgrade : MonoBehaviour
{
    public GameObject ChooseTowerUpgradeOptionPanel;
    public GameObject SelectTowerUpgradePanel;
    public GameObject TowerUpgradeDataWindow;
    public GameObject UniqueUpgradeButton;
    public string TowerUpgradeName;

    public TMP_Text TokenCounter;

    public DataPanelPopulator Populator;
    public StoreTowerUpgradeData storeTowerUpgradeData;

    public bool OpenClosed = false;

    public void Update()
    {
        TokenCounter.text = "Tokens Left: " + storeTowerUpgradeData.TokensObtained.ToString();
    }
    public void OpenDataPanel(string ButtonName)
    {
        if (OpenClosed == false)
        {
            ChooseTowerUpgradeOptionPanel.SetActive(true);
            TowerUpgradeName = ButtonName;
            Panel();
            OpenClosed = true;
            if (UniqueUpgradeButton == null)
            {
                return;
            }
            else
            {
                UniqueUpgradeButton.SetActive(false);
            }
            return;
        }

        if (OpenClosed)
        {
            ChooseTowerUpgradeOptionPanel.SetActive(false);
            UniqueUpgradeButton.SetActive(false);
            OpenClosed = false;
            return;
        }
    }

    public void PassPannelButtons(GameObject UpgradeButton)
    {
        UniqueUpgradeButton = UpgradeButton;
        UniqueUpgradeButton.SetActive(true);
    }

    private void Panel()
    {
        TMP_Text HeaderName = ChooseTowerUpgradeOptionPanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        HeaderName.text = "SELECT " + TowerUpgradeName + " UPGRADES";
    }

    public void ButtonPressed(TowerUpgradeData UpgradeData)
    {
        TowerUpgradeDataWindow.SetActive(true);
        Populator.PopulatePanel(UpgradeData);
    }

    public void StartRun()
    {
        if (storeTowerUpgradeData.TokensObtained > 1)
        {
            storeTowerUpgradeData.TokensObtained--;
        }
        else if (storeTowerUpgradeData.TokensObtained == 1)
        {
            storeTowerUpgradeData.TokensObtained--;
            SceneManager.LoadScene("Game View");
        }
    }

    public void EndRun()
    {
        SceneManager.LoadScene("Main Menu");
    }
}