using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectConversion : MonoBehaviour
{
    [Header("Tower Prefabs")]
    [SerializeField] private GameObject BeeTowerPrefab;
    [SerializeField] private GameObject MortarTowerPrefab;
    [SerializeField] private GameObject TetherTowerPrefab;
    [SerializeField] private GameObject FlameTowerPrefab;
    [SerializeField] private GameObject MeleeTowerPrefab;
    [SerializeField] private GameObject MortarAntPrefab;
    [SerializeField] private GameObject AttackBeePrefab;
    [SerializeField] private GameObject BeeSwarmPrefab;
    [SerializeField] private GameObject BuffingBeePrefab;
    [SerializeField] private GameObject WaspTowerPrefab;
    [SerializeField] private GameObject WaspMeleePrefab;
    [SerializeField] private GameObject SpiderTowerPrefab;

    public GameObject[] LoadTowerArray;
    public string[] SaveTowerArray;

    public string[] SaveTowerObtained(GameObject[] tower, int size)
    {
        Debug.Log("List of towers obtained to save");
        SaveTowerArray = new string[20];
        int count = 0;

        while (count < size)
        {
            if (tower[count] == null)
            {
                Debug.Log("Tower is null");
            }
            else
            {
                SaveTowerArray[count] = tower[count].name;
            }
            count++;
        }
        return SaveTowerArray;
    }

    public string[] SaveTowerUnlockOrder(GameObject[] tower)
    {
        Debug.Log("List of tower unlock order to save");
        SaveTowerArray = new string[6];
        int count = 0;

        while (count < tower.Length)
        {
            if (tower[count] == null)
            {
                Debug.Log("Tower is null");
            }
            else
            {
                SaveTowerArray[count] = tower[count].name;
            }
            count++;
        }
        return SaveTowerArray;
    }

    public GameObject[] LoadTowerObtained(string[] TowerName)
    {
        Debug.Log("List of towers obtained to load");
        LoadTowerArray = new GameObject[20];
        int count = 0;

        while (count < TowerName.Length)
        {
            if (TowerName[count] == null)
            {
                Debug.Log("Tower name not available");
            }
            else
            {
                LoadTowerArray[count] = GetTowerPrefab(TowerName[count]);
            }
            count++;
        }

        return LoadTowerArray;
    }

    public GameObject[] LoadTowerUnlockOrder(string[] TowerName)
    {
        Debug.Log("List of tower unlock order to load");
        LoadTowerArray = new GameObject[6];
        int count = 0;

        while (count < TowerName.Length)
        {
            if (TowerName[count] == null)
            {
                Debug.Log("Tower name not available");
            }
            else
            {
                LoadTowerArray[count] = GetTowerPrefab(TowerName[count]);
            }
            count++;
        }

        return LoadTowerArray;
    }

    private GameObject GetTowerPrefab(string name)
    {
        switch (name)
        {
            case "BeeTower":
                return BeeTowerPrefab;
            case "MortarTower":
                return MortarTowerPrefab;
            case "tetherTower":
                return TetherTowerPrefab;
            case "FlameTower":
                return FlameTowerPrefab;
            case "MeleeTower":
                return MeleeTowerPrefab;
            case "MortarAnt":
                return MortarAntPrefab;
            case "AttackBeeTower":
                return AttackBeePrefab;
            case "BeeSwarmTower":
                return BeeSwarmPrefab;
            case "BuffingBeeTower":
                return BuffingBeePrefab;
            case "Wasp":
                return WaspTowerPrefab;
            case "WaspMelee":
                return WaspMeleePrefab;
            case "Spider":
                return SpiderTowerPrefab;

            default:
                Debug.LogWarning("Tower prefab not found: " + name);
                return null;

        }
    }
}
