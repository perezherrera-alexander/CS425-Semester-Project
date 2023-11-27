using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour, ISaveable
{
    public GameObject BeeTowerPrefab;
    public GameObject MortarTowerPrefab;
    public GameObject TetherTowerPrefab;

    [Serializable]
    private struct TowerData
    {
        public string TowerID;
        public float[] TowerPosition;
        public string TowerName;
    }

    private Dictionary<string, TowerData> towerDictionary = new Dictionary<string, TowerData>();

    public void AddTower(string towerID, Vector3 position, string name)
    {
        towerDictionary[towerID] = new TowerData
        {
            TowerID = towerID,
            TowerPosition = new float[] { position.x, position.y, position.z },
            TowerName = name
        };

        Debug.Log($"Added tower: {towerID} at position {position}, Tower Name: {name}");
    }

    public void RemoveTower(string towerID)
    {
        Debug.Log($"Removing tower: {towerID}");
        towerDictionary.Remove(towerID);
        Debug.Log($"Remaining towers: {string.Join(", ", towerDictionary.Keys)}");
    }

    public object CaptureState()
    {
        Debug.Log("CAPTURING STATE");
        var saveDataList = new List<TowerData>(towerDictionary.Values);
        return new SaveDataList { TowerDataList = saveDataList };
    }

    public void RestoreState(object state)
    {
        var saveDataList = (SaveDataList)state;
        foreach (var towerData in saveDataList.TowerDataList)
        {
            towerDictionary[towerData.TowerID] = towerData;
            Debug.Log($"Restored tower: {towerData.TowerID} at position {new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2])}, Tower name: {towerData.TowerName}");
        }

        // Call to instantiate all saved towers back into the game
        InstantiateTowers();
    }

    public void InstantiateTowers()
    {
        foreach (var towerData in towerDictionary.Values)
        {

            if (towerData.TowerName == "beeTurret")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), BeeTowerPrefab);

                instantiateTower.GetComponentInChildren<beeTower>().id = towerData.TowerID;
            }

            if (towerData.TowerName == "mortarTurret")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), MortarTowerPrefab);

                instantiateTower.GetComponentInChildren<mortarTower>().id = towerData.TowerID;
            }

            if (towerData.TowerName == "tetherTower")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), TetherTowerPrefab);

                instantiateTower.GetComponentInChildren<tetherTower>().id = towerData.TowerID;

            }

            Debug.Log($"Instantiated tower: {towerData.TowerID} at position {new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2])}, Tower Name: {towerData.TowerName}");
        }
    }

    public GameObject InstantiateTowerPrefab(Vector3 position, GameObject tower)
    {
        GameObject towerPrefab = Instantiate(tower, position, Quaternion.identity);

        towerPrefab.GetComponentInParent<BoxCollider>().enabled = true;
        towerPrefab.transform.GetComponentInChildren<basicTowerScript>().ActivateTower();

        return towerPrefab;
    }

    [Serializable]
    private struct SaveData
    {
        public string TowerID;
        public float[] TowerPosition;
        public string TowerName;
    }

    [Serializable]
    private struct SaveDataList
    {
        public List<TowerData> TowerDataList;
    }
}