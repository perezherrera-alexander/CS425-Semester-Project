using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour, ISaveable
{
    public GameObject BeeTowerPrefab;

    [Serializable]
    private struct TowerData
    {
        public string TowerID;
        public float[] TowerPosition;
    }

    private Dictionary<string, TowerData> towerDictionary = new Dictionary<string, TowerData>();

    public void AddTower(string towerID, Vector3 position)
    {
        towerDictionary[towerID] = new TowerData
        {
            TowerID = towerID,
            TowerPosition = new float[] { position.x, position.y, position.z }
        };

        Debug.Log($"Added tower: {towerID} at position {position}");
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
            Debug.Log($"Restored tower: {towerData.TowerID} at position {new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2])}");
        }

        // Call to instantiate all saved towers back into the game
        InstantiateTowers();
    }

    public void InstantiateTowers ()
    {
        foreach (var towerData in towerDictionary.Values)
        {
            GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]));

            instantiateTower.GetComponentInChildren<beeTower>().id = towerData.TowerID;

            Debug.Log($"Instantiated tower: {towerData.TowerID} at position {new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2])}");
        }
    }

    public GameObject InstantiateTowerPrefab (Vector3 position)
    {
        GameObject towerPrefab = Instantiate(BeeTowerPrefab, position, Quaternion.identity);

        towerPrefab.GetComponentInParent<BoxCollider>().enabled = true;
        towerPrefab.transform.GetChild(1).gameObject.GetComponent<beeTower>().ActivateTower();
        //towerPrefab.transform.GetChild(1).gameObject.GetComponent<beeTower>().id = 

        return towerPrefab;
    }

    [Serializable]
    private struct SaveData
    {
        public string TowerID;
        public float[] TowerPosition;
    }

    [Serializable]
    private struct SaveDataList
    {
        public List<TowerData> TowerDataList;
    }
}