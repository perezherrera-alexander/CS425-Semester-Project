using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemoveSelectedTowerUpgrade : MonoBehaviour
{
    public TMP_Text UpgradeName;
    public string Upgradename2;
    public GameObject button;
    public StoreTowerUpgradeData storeTowerUpgradeData;
    public int row;

    public void SetName(string name)
    {
        Upgradename2 = name;
        UpgradeName.text = name;
    }
    public void DeleteButton()
    {
        storeTowerUpgradeData.TokensObtained++;
        storeTowerUpgradeData.ListOfUpgradesObtained.Remove(Upgradename2);
        Destroy(button);
    }
}
