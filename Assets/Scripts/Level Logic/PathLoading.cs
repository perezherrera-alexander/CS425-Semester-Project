using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLoading : MonoBehaviour
{
    public GameObject[] pathPrefabs;
    void Start()
    {
        // If "World Objects" has a child, destroy it
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        // Assign one of the prefab paths to the "World Objects" game object
        int pathIndex = Random.Range(0, pathPrefabs.Length);
        //Debug.Log("Path index: " + pathIndex);
        GameObject path = Instantiate(pathPrefabs[pathIndex], transform.position, Quaternion.identity);
        path.transform.parent = transform;

        // Find WaveSpawner and assign START point
        GameObject waveSpawner = GameObject.Find("Game Master");
        waveSpawner.GetComponent<WaveSpawner>().SpawnPoint = path.transform.GetChild(1);
    }

}
