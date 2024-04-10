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
            ButtonNameHolder = ButtonName;
            return;
        }

        if (ButtonNameHolder != ButtonName)
        {
            ChooseTowerUpgradeOptionPanel.SetActive(false);
            UniqueUpgradeButton.SetActive(false);

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
            ButtonNameHolder = ButtonName;
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

    public void CreateButtonForUpgrade()
    {
        GameObject UpgradeButton = Instantiate(TowerUpgradeButton);
        UpgradeButton.transform.SetParent(UpgradePickedParent.transform, false);

        int size = storeTowerUpgradeData.ListOfUpgradesObtained.Count - 1;
        UpgradeButton.GetComponent<RemoveSelectedTowerUpgrade>().SetName(storeTowerUpgradeData.ListOfUpgradesObtained[size]);
    }

    public void EndRun()
    {
        SceneManager.LoadScene("Main Menu");
    }
}