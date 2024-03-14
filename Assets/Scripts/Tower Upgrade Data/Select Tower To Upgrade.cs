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

    public DataPanelPopulator Populator;

    public void OpenDataPanel(string ButtonName)
    {
        SelectTowerUpgradePanel.SetActive(false);
        ChooseTowerUpgradeOptionPanel.SetActive(true);
        TowerUpgradeName = ButtonName;
        Panel();
    }

    public void PassPannelButtons(GameObject UpgradeButton)
    {
        UniqueUpgradeButton = UpgradeButton;
        UniqueUpgradeButton.SetActive(true);
    }

    public void CloseDataPanel()
    {
        SelectTowerUpgradePanel.SetActive(true);
        ChooseTowerUpgradeOptionPanel.SetActive(false);
        TowerUpgradeDataWindow.SetActive(false);
        UniqueUpgradeButton.SetActive(false);
    }

    private void Panel()
    {
        TMP_Text HeaderName = ChooseTowerUpgradeOptionPanel.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        HeaderName.text = "SELECT " + TowerUpgradeName + " UPGRADES";
    }

    public void ButtonPressed(TowerUpgradeData UpgradeData)
    {
        TowerUpgradeDataWindow.SetActive(true);
        Populator.PopulatePanel(UpgradeData);
    }

    public void StartRun()
    {
        SceneManager.LoadScene("Game View");
    }

    public void EndRun()
    {
        SceneManager.LoadScene("Main Menu");
    }
}