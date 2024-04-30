using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectConversion : MonoBehaviour
{
    [Header("Tower Prefabs")]
    [SerializeField] private GameObject CentipedePrefab;
    [SerializeField] private GameObject MantisPrefab;
    [SerializeField] private GameObject BeetlePrefab;
    [SerializeField] private GameObject StagBeetlePrefab;
    [SerializeField] private GameObject SpiderPrefab;
    [SerializeField] private GameObject GrassHopperPrefab;
    [SerializeField] private GameObject MothPrefab;

    [SerializeField] private GameObject AttackBeePrefab;
    [SerializeField] private GameObject BeeSwarmPrefab;
    [SerializeField] private GameObject BuffingBeePrefab;
    [SerializeField] private GameObject FlamePrefab;
    [SerializeField] private GameObject MeleePrefab;
    [SerializeField] private GameObject MortarAntPrefab;
    [SerializeField] private GameObject WaspPrefab;
    [SerializeField] private GameObject WaspMeleePrefab;


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
                Debug.Log(tower[count].name);
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
                Debug.Log(tower[count].name);
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
            case "AttackBeeTower":
                return AttackBeePrefab;
            case "BeeSwarmTower":
                return BeeSwarmPrefab;
            case "BuffingBeeTower":
                return BuffingBeePrefab;
            case "FlameTower":
                return FlamePrefab;
            case "MeleeTower":
                return MeleePrefab;
            case "MortarAnt":
                return MortarAntPrefab;
            case "Wasp":
                return WaspPrefab;
            case "WaspMelee":
                return WaspMeleePrefab;

            case "Centipede Tower":
                return CentipedePrefab;
            case "MantisTower":
                return MantisPrefab;
            case "BeetleTower":
                return BeetlePrefab;
            case "StagBeetleTower":
                return StagBeetlePrefab;
            case "Spider":
                return SpiderPrefab;
            case "grassHopperTower":
                return GrassHopperPrefab;
            case "mothTower":
                return MothPrefab;

            default:
                Debug.LogWarning("Tower prefab not found: " + name);
                return null;

        }
    }
}
