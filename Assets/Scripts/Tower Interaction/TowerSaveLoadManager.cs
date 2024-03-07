using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSaveLoadManager : MonoBehaviour, ISaveable
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

        Debug.Log($"Added tower: {towerID} at position {position}, Tower Name: {name}, with Targeting: {targetType}");
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

            if (towerData.TowerName == "Bee Tower")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), BeeTowerPrefab);

                instantiateTower.GetComponentInChildren<BeeTower>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<BeeTower>().targetingType = towerData.TargetingOption;
            }

            if (towerData.TowerName == "Mortar Tower")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), MortarTowerPrefab);

                instantiateTower.GetComponentInChildren<mortarTower>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<mortarTower>().targetingType = towerData.TargetingOption;
            }

            if (towerData.TowerName == "Tether Tower")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), TetherTowerPrefab);

                instantiateTower.GetComponentInChildren<tetherTower>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<tetherTower>().targetingType = towerData.TargetingOption;

            }










            if (towerData.TowerName == "Flame Tower")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), FlameTowerPrefab);

                instantiateTower.GetComponentInChildren<FlameTower>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<FlameTower>().targetingType = towerData.TargetingOption;

            }

            if (towerData.TowerName == "Melee Tower")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), MeleeTowerPrefab);

                instantiateTower.GetComponentInChildren<meleeTower>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<meleeTower>().targetingType = towerData.TargetingOption;

            }

            if (towerData.TowerName == "Mortar Ant")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), MortarAntPrefab);

                instantiateTower.GetComponentInChildren<mortarTower>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<mortarTower>().targetingType = towerData.TargetingOption;

            }

            if (towerData.TowerName == "Attack Bee")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), AttackBeePrefab);

                instantiateTower.GetComponentInChildren<BeeTower>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<BeeTower>().targetingType = towerData.TargetingOption;

            }

            if (towerData.TowerName == "Bee Swarm")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), BeeSwarmPrefab);

                instantiateTower.GetComponentInChildren<BeeSwarm>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<BeeSwarm>().targetingType = towerData.TargetingOption;

            }

            if (towerData.TowerName == "Buffing Bee")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), BuffingBeePrefab);

                instantiateTower.GetComponentInChildren<BuffingBees>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<BuffingBees>().targetingType = towerData.TargetingOption;

            }

            if (towerData.TowerName == "Wasp Melee")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), WaspMeleePrefab);

                instantiateTower.GetComponentInChildren<WaspMelee>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<WaspMelee>().targetingType = towerData.TargetingOption;

            }

            if (towerData.TowerName == "Wasp Tower")
            {
                GameObject instantiateTower = InstantiateTowerPrefab(new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2]), WaspTowerPrefab);

                instantiateTower.GetComponentInChildren<StraightShooter>().id = towerData.TowerID;

                instantiateTower.GetComponentInChildren<StraightShooter>().targetingType = towerData.TargetingOption;

            }

            Debug.Log($"Instantiated tower: {towerData.TowerID} at position {new Vector3(towerData.TowerPosition[0], towerData.TowerPosition[1], towerData.TowerPosition[2])}," +
                      $"Tower Name: {towerData.TowerName}, targeting Option {towerData.TargetingOption}");
        }
    }

    public GameObject InstantiateTowerPrefab(Vector3 position, GameObject tower)
    {
        GameObject towerPrefab = Instantiate(tower, position, Quaternion.identity);

        towerPrefab.GetComponentInParent<BoxCollider>().enabled = true;

        // Iterate over all children of the tower prefab
        for (int i = 0; i < towerPrefab.transform.childCount; i++)
        {
            Transform towerChild = towerPrefab.transform.GetChild(i);

            // Check if the child has a SphereCollider component
            SphereCollider sphereCollider = towerChild.GetComponentInChildren<SphereCollider>();

            if (sphereCollider != null)
            {
                // Enable the SphereCollider if it exists
                sphereCollider.enabled = true;

                // Activate tower logic
                BaseTowerLogic towerLogic = towerChild.GetComponent<BaseTowerLogic>();
                if (towerLogic != null)
                {
                    towerLogic.ActivateTower();
                }
                else
                {
                    Debug.LogWarning("No BaseTowerLogic component found in the tower child.");
                }

                Debug.Log("Activating tower and setting sphere collider");
                return towerPrefab; // Return the prefab after finding the appropriate child
            }
        }

        // If no child with SphereCollider is found, log a warning
        Debug.LogWarning("No child with SphereCollider found in the tower prefab.");

        return towerPrefab; // Return the prefab even if no appropriate child is found
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