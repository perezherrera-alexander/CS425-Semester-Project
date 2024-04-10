using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Store Tower Upgrades", menuName = "Obtained Tower Upgrade Data")]
public class StoreTowerUpgradeData : ScriptableObject
{
    public int TokensObtained = 0;
    public List<string> ListOfUpgradesObtained;
    public string TowerUpgradePicked = "";

    public void UpdateListOfUpgrades()
    {
        if (TokensObtained > 0)
        {
            ListOfUpgradesObtained.Add(TowerUpgradePicked);
        }
        else
        {
            Debug.Log("NO TOKENS");
            return;
        }
    }

    public void TowerUpgradeChosen(string Upgrade)
    {
        TowerUpgradePicked = Upgrade;
    }
}
