using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLoading : MonoBehaviour, ISaveable
{
    public GameObject[] pathPrefabs;
    public PlayerData playerData;

    public int LevelChoice = 0;
    void Start()
    {
        // If "World Objects" has a child, destroy it
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        if (playerData.LevelLoaded == false)
        {
            // Assign one of the prefab paths to the "World Objects" game object
            int pathIndex = UnityEngine.Random.Range(0, pathPrefabs.Length);
            LevelChoice = pathIndex;
            //Debug.Log("Path index: " + pathIndex);
            GameObject path = Instantiate(pathPrefabs[pathIndex], transform.position, Quaternion.identity);
            path.transform.parent = transform;

            // Find WaveSpawner and assign START point
            GameObject waveSpawner = GameObject.Find("Game Master");
            waveSpawner.GetComponent<WaveSpawner>().SpawnPoint = path.transform.GetChild(1);
        }

        if (playerData.LevelLoaded == true)
        {
            //Debug.Log("Path index: " + pathIndex);
            GameObject path = Instantiate(pathPrefabs[LevelChoice], transform.position, Quaternion.identity);
            path.transform.parent = transform;

            // Find WaveSpawner and assign START point
            GameObject waveSpawner = GameObject.Find("Game Master");
            waveSpawner.GetComponent<WaveSpawner>().SpawnPoint = path.transform.GetChild(1);

            playerData.LevelLoaded = false;
        }
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
