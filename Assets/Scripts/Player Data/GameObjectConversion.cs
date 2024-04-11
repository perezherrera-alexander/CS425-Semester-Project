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
            case "Centipede Mother":
                return CentipedePrefab;
            case "Mantis Warrior":
                return MantisPrefab;
            case "Beetle Buzzer":
                return BeetlePrefab;
            case "Stag Staller":
                return StagBeetlePrefab;
            case "Spider Tower":
                return SpiderPrefab;
            case "Grasshopper Lair":
                return GrassHopperPrefab;
            case "Moth Man":
                return MothPrefab;

            default:
                Debug.LogWarning("Tower prefab not found: " + name);
                return null;

        }
    }
}
