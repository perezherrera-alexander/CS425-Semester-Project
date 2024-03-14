using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Upgrade", menuName = "Tower Upgrade Data")]
public class TowerUpgradeData : ScriptableObject
{
    public string UpgradeName;
    public int Cost;
    public string Description;
}
