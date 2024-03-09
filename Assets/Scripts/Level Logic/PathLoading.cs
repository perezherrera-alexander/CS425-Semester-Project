using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLoading : MonoBehaviour, ISaveable
{
    [Tooltip("Enable to use debug mode, which will not load a specified, saved, or randomly chosen level (use the one already in the scene).")]
    public bool enableDebug = false;
    [Tooltip("Enable to only load the level specified by the index. Will ignore saved level or rnadom choice (but not debug mode).")]
    public bool loadSpecificLevel = false;
    public int specificLevelIndex = 0;
    public GameObject[] pathPrefabs;
    public PlayerData playerData;
    public int LevelChoice = 0; // I feel like this can private
    void Start()
    {
        if(enableDebug) return; // If debug is enabled, don't load saved level or randomly pick one (use the one already in the scene)

        // If "World Objects" has a child, destroy it
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        if(loadSpecificLevel) // Load the specified level, ignoring everything else (debug mode, save data, random choice)
        {
            GameObject path = Instantiate(pathPrefabs[specificLevelIndex], transform.position, Quaternion.identity);
            path.transform.parent = transform;
            assignSTARTpoint(path);
            return;
        }

        // If there is save data, load the level from the save data, otherwise load a random level
        if (playerData.LevelLoaded == false)
        {
            // Assign one of the prefab paths to the "World Objects" game object
            int pathIndex = UnityEngine.Random.Range(1, pathPrefabs.Length);
            LevelChoice = pathIndex;
            GameObject path = Instantiate(pathPrefabs[pathIndex], transform.position, Quaternion.identity);
            path.transform.parent = transform;

            // Find WaveSpawner and assign START point
            GameObject waveSpawner = GameObject.Find("Game Master");
            waveSpawner.GetComponent<WaveSpawner>().SpawnPoint = path.transform.GetChild(1);
        }
        else
        {
            GameObject path = Instantiate(pathPrefabs[LevelChoice], transform.position, Quaternion.identity);
            path.transform.parent = transform;

            // Find WaveSpawner and assign START point
            GameObject waveSpawner = GameObject.Find("Game Master");
            waveSpawner.GetComponent<WaveSpawner>().SpawnPoint = path.transform.GetChild(1);

            playerData.LevelLoaded = false;
        }
    }

    public void assignSTARTpoint(GameObject pathPrefab)
    {
        //Debug.Log("Assigning START point");
        GameObject waveSpawner = GameObject.Find("Game Master");
        waveSpawner.GetComponent<WaveSpawner>().SpawnPoint = pathPrefab.transform.GetChild(1);
    }

    public object CaptureState()
    {
        Debug.Log(LevelChoice);
        Debug.Log("CaptureState called");
        return new SaveData
        {
            LevelChoice = LevelChoice
        };
    }

    public void RestoreState(object state)
    {
        Debug.Log("RestoreState called");
        var saveData = (SaveData)state;

        LevelChoice = saveData.LevelChoice;
        Debug.Log(LevelChoice);
        Debug.Log("Did Restore state even load");
    }

    [Serializable]
    private struct SaveData
    {
        public int LevelChoice;
    }
}
