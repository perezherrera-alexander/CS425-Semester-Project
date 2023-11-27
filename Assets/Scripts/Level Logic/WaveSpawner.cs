using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab1;

    public Transform enemyPrefab2;

    public Transform SpawnPoint;

    public float timeBetweenWaves = 5f;

    public float EnemyTimeSeperation = 0.5f;

    private float countDown = 0;

    private float timer = 2;

    public TextMeshProUGUI waveCountDownText;

    public TextMeshProUGUI levelCompleteText;

    [NonSerialized] public bool levelComplete = false;

    public int EnemiesPerWave = 0;
    [NonSerialized] public int currentWaveCount = 0;
    [NonSerialized] public bool waveInProgress = false;
    public int maxWaveAmount = 0;

    void Start()
    {
        levelCompleteText.text = "";
        timer = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if(!waveInProgress && timer <= 0 && !levelComplete)
        {
            waveInProgress = true;
            StartCoroutine(SpawnWave());
        }
        else if (!waveInProgress && timer > 0 && !levelComplete) {
            timer -= Time.deltaTime;
        }

        waveCountDownText.text = "Wave: " + currentWaveCount + " / " + maxWaveAmount;

        timer -= Time.deltaTime;
        countDown += Time.deltaTime;

        if(currentWaveCount >= maxWaveAmount)
        {
            levelComplete = true;
            levelCompleteText.text = "Level Complete!";
        }
    }

    IEnumerator SpawnWave()
    {

        for (int i = 0; i < EnemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(EnemyTimeSeperation);
        }
        Debug.Log("Couroutine Finished");
        currentWaveCount++;
        waveInProgress = false;
        timer = timeBetweenWaves;
        yield return null;
    }

    void SpawnEnemy()
    {
        // Randomly decide to spawn enemy 1 or 2
        int enemyType = UnityEngine.Random.Range(0, 2);
        if(enemyType == 0)
            Instantiate(enemyPrefab1, SpawnPoint.position, SpawnPoint.rotation);
        else if (enemyType == 1)
            Instantiate(enemyPrefab2, SpawnPoint.position, SpawnPoint.rotation);
    }
}
