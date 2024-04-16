using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataPanelPopulator : MonoBehaviour
{
    public TMP_Text UpgradeNameText;
    public TMP_Text CostText;
    public TMP_Text DescriptionText;
    public TowerUpgradeData UpgradeData;

    public void Start()
    {
        UpgradeNameText.text = UpgradeData.UpgradeName;
        CostText.text = "Cost: " + UpgradeData.Cost.ToString();
        DescriptionText.text = UpgradeData.Description;
    }
}