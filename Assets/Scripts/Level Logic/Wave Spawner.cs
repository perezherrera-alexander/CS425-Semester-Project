using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class WaveSpawner : MonoBehaviour
{
    public PlayerData PlayerData;

    public Transform enemyPrefab1;

    public Transform enemyPrefab2;

    public Transform SpawnPoint;

    public float timeBetweenWaves = 5f;

    public float EnemyTimeSeperation = 0.5f;

    //private float countDown = 0;

    private float timer = 2;

    public TextMeshProUGUI waveCountDownText;

    public TextMeshProUGUI levelCompleteText;

    public int EnemiesPerWave = 0;
    public int currentWaveCount = 1;
    public int maxWaveAmount = 0;
    public GameStates gameState;

    void Start()
    {
        levelCompleteText.text = "";
        timer = timeBetweenWaves;
    }

    void Update()
    {
        if(gameState == GameStates.InbetweenWaves)
        {
            if(timer <= 0)
            {
                gameState = GameStates.WaveStarting;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else if(gameState == GameStates.WaveStarting)
        {
            StartCoroutine(SpawnWave());
            gameState = GameStates.WaveInProgress;
        }
        else if(gameState == GameStates.WaveInProgress)
        {
            // Check if all enemies have been killed, if it isn't the last wave, go back to inbetween waves
            if(PlayerStatistics.Instance.GetEnemiesKilled() >= EnemiesPerWave)
            {
                Debug.Log("Wave Complete!");
                PlayerStatistics.Instance.ResetEnemiesKilled();
                if(currentWaveCount >= maxWaveAmount)
                {
                    Debug.Log("Level Complete!");
                    gameState = GameStates.LevelComplete;
                    timer = timeBetweenWaves;
                }
                else
                {
                    Debug.Log("Next Wave!");
                    gameState = GameStates.InbetweenWaves;
                    currentWaveCount++;
                    timer = timeBetweenWaves;
                }
            }

        }
        else if(gameState == GameStates.LevelComplete)
        {
            levelCompleteText.text = "Level Complete!";
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                // Load the next level
                Debug.Log("Loading Next Level");
                PlayerData.UpdateData(true);
                PlayerData.WorldsCompleted[PlayerData.NumberOfWorldsCompleted] = PlayerData.CurrentWorld;
                PlayerData.NumberOfWorldsCompleted += 1;
                UnityEngine.SceneManagement.SceneManager.LoadScene("World Map");
            }

        }

        waveCountDownText.text = "Wave: " + currentWaveCount + " / " + maxWaveAmount;
    }

    IEnumerator SpawnWave()
    {

        for (int i = 0; i < EnemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(EnemyTimeSeperation);
        }
        yield return null;
    }

    void SpawnEnemy()
    {
        // Randomly decide to spawn enemy 1 or 2
        // Obivosuly we'll make this a little more sophisticated later
        int enemyType = UnityEngine.Random.Range(0, 2);
        if(enemyType == 0)
            Instantiate(enemyPrefab1, SpawnPoint.position, SpawnPoint.rotation);
        else if (enemyType == 1)
            Instantiate(enemyPrefab2, SpawnPoint.position, SpawnPoint.rotation);
    }
}
