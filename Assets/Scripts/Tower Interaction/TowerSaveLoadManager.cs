using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSaveLoadManager : MonoBehaviour, ISaveable
{
    public GameObject BeeTowerPrefab;
    public GameObject MortarTowerPrefab;
    public GameObject TetherTowerPrefab;

    private Dictionary<string, TowerData> towerDictionary = new Dictionary<string, TowerData>();

    public void AddTower(string towerID, Vector3 position, string name, TargetingTypes targetType)
    {
        towerDictionary[towerID] = new TowerData
        {
            TowerID = towerID,
            TowerPosition = new float[] { position.x, position.y, position.z },
            TowerName = name,
            TargetingOption = targetType
        };

        Debug.Log($"Added tower: {towerID} at position {position}, Tower Name: {name}");
    }

    public void RemoveTower(string towerID)
    {
        Debug.Log($"Removing tower: {towerID}");
        towerDictionary.Remove(towerID);
        Debug.Log($"Remaining towers: {string.Join(", ", towerDictionary.Keys)}");
    }

    public void InstantiateTowers()
    {
        foreach (var towerData in towerDictionary.Values)
        {

            if (towerData.TowerName == "beeTurret")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), BeeTowerPrefab);

                instantiateTower.GetComponentInChildren<BeeTower>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<BeeTower>().targetingType = towerData.TargetingOption;
            }

            if (towerData.TowerName == "mortarTurret")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), MortarTowerPrefab);

                instantiateTower.GetComponentInChildren<mortarTower>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<mortarTower>().targetingType = towerData.TargetingOption;
            }

            if (towerData.TowerName == "tetherTower")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), TetherTowerPrefab);

                instantiateTower.GetComponentInChildren<tetherTower>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<tetherTower>().targetingType = towerData.TargetingOption;

            }

            Debug.Log($"Instantiated tower: {towerData.TowerID} at position {new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2])}," +
                      $"Tower Name: {towerData.TowerName}, targeting Option {towerData.TargetingOption}");
        }
    }

    public GameObject InstantiateTowerPrefab(Vector3 position, GameObject tower)
    {
        GameObject towerPrefab = Instantiate(tower, position, Quaternion.identity);

        towerPrefab.GetComponentInParent<BoxCollider>().enabled = true;
        towerPrefab.transform.GetChild(1).GetComponentInChildren<SphereCollider>().enabled = true;
        towerPrefab.transform.GetComponentInChildren<BaseTowerLogic>().ActivateTower();

        Debug.Log("activating tower and setting sphere collider");

        return towerPrefab;
    }

    public void UpdateTargetingType(string towerID, TargetingTypes UpdatedTargetingType)
    {
        if (towerDictionary.ContainsKey(towerID))
        {
            TowerData towerData = towerDictionary[towerID];
            towerData.TargetingOption = UpdatedTargetingType;

            // Replace the existing tower data in the dictionary
            towerDictionary[towerID] = towerData;

            Debug.Log($"New targeting option for tower {towerID} is : {UpdatedTargetingType}");
        }
        else
        {
            Debug.Log($"Tower with ID {towerID} not found for updating targeting option");
        }
    }


    public object CaptureState()
    {
        Debug.Log("CAPTURING STATE");
        var saveDataList = new List<TowerData>();

        foreach (var towerdata in towerDictionary.Values)
        {
            var saveTowerData = new TowerData
            {
                TowerID = towerdata.TowerID,
                TowerPosition = towerdata.TowerPosition,
                TowerName = towerdata.TowerName,
                TargetingOption = towerdata.TargetingOption
            };
            saveDataList.Add(saveTowerData);
        }
        return new SaveDataList { TowerDataList = saveDataList };
    }

    public void RestoreState(object state)
    {
        var saveDataList = (SaveDataList)state;
        foreach (var towerData in saveDataList.TowerDataList)
        {
            towerDictionary[towerData.TowerID] = towerData;
            Debug.Log($"Restored tower: {towerData.TowerID} at position {new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2])}," +
                      $" Tower name: {towerData.TowerName}, Targeting Option: {towerData.TargetingOption}");
        }

        // Call to instantiate all saved towers back into the game
        InstantiateTowers();
    }

    [Serializable]
    private struct TowerData
    {
        public string TowerID;
        public float[] TowerPosition;
        public string TowerName;
        public TargetingTypes TargetingOption;
    }

    [Serializable]
    private struct SaveDataList
    {
        public List<TowerData> TowerDataList;
    }
}