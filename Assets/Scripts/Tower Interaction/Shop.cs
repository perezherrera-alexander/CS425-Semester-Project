using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [Header("Tower Prefabs")]
    [SerializeField] private GameObject BeeTowerPrefab;
    [SerializeField] private GameObject MortarTowerPrefab;
    [SerializeField] private GameObject TetherTowerPrefab;
    [Header("Scripts")]
    [SerializeField] private TowerPlacement towerPlacement;
    [SerializeField] private PlayerStatistics playerStatistics;

    public void PurchaseTower(string towerName)
    {
        //Debug.Log("Shop script called from button click with tower name: " + towerName);
        if (towerName == "Bee Tower")
        {
            PurchaseBeeTower();
        }
        else if (towerName == "Mortar Tower")
        {
            PurchaseMortarTower();
        }
        else if (towerName == "Tether Tower")
        {
            PurchaseTetherTower();
        }
    }
    public void PurchaseBeeTower()
    {
        Transform child = BeeTowerPrefab.transform.Find("Rotate");

        // Check if you have enough money to purchase the Bee Tower
        int towerCost = child.GetComponent<BeeTower>().buildCost;

        if (playerStatistics.GetMoney() >= towerCost)
        {
            // Deduct the money and call the PlaceTower function
            playerStatistics.AddMoney(-towerCost);
            towerPlacement.PlaceTower(BeeTowerPrefab, "Bee Tower"); // Pass along the prefab's name so we know which script to call later in TowerPlacement.cs
        }
        else
        {
            Debug.Log("Not enough money to purchase the Bee Tower.");
        }
    }

    public void PurchaseMortarTower()
    {
        Transform child = MortarTowerPrefab.transform.Find("Rotate");

        // Check if you have enough money to purchase the Bee Tower
        int towerCost = child.GetComponent<mortarTower>().buildCost;

        if (playerStatistics.GetMoney() >= towerCost)
        {
            // Deduct the money and call the PlaceTower function
            playerStatistics.AddMoney(-towerCost);
            towerPlacement.PlaceTower(MortarTowerPrefab, "Morar Tower");
        }
        else
        {
            Debug.Log("Not enough money to purchase the Mortar Tower.");
        }
    }

    public void PurchaseTetherTower()
    {
        Transform child = TetherTowerPrefab.transform.Find("Rotate");

        // Check if you have enough money to purchase the Bee Tower
        int towerCost = child.GetComponent<tetherTower>().buildCost;

        if (playerStatistics.GetMoney() >= towerCost)
        {
            // Deduct the money and call the PlaceTower function
            playerStatistics.AddMoney(-towerCost);
            towerPlacement.PlaceTower(TetherTowerPrefab, "Tether Tower");
        }
        else
        {
            Debug.Log("Not enough money to purchase the Tether Tower.");
        }
    }
}