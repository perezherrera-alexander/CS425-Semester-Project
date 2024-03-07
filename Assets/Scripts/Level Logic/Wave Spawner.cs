using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class WaveSpawner : MonoBehaviour
{
    public PlayerData PlayerData;
    [Header("Enemy Prefabs")]
    public Transform enemyPrefab1;
    public Transform enemyPrefab2;
    public Transform SpawnPoint;
    [Header("Wave Settings")]
    public GameStates gameState;
    [Range(0, 10)]
    public int enemiesPerWave;
    [Range(0, 10)]
    public int currentWaveCount;
    [Range(1, 10)]
    public int maxWaveCount;
    [Range(0f, 5f)]
    public float timeBetweenWaves;
    [Range(0f, 2f)]
    public float timeBetweenEnemySpawns;
    //private float timeBetweenWavesTimer;
    
    [Header("UI")]
    public TextMeshProUGUI waveCountDownText;
    public TextMeshProUGUI levelCompleteText;
    public TextMeshProUGUI beginNextWaveText;
    private bool loadNextLevelOnce = true;
    void Start()
    {
        levelCompleteText.text = "";
    }

    void Update()
    {
        
        if(gameState == GameStates.InbetweenWaves)
        {
            beginNextWaveText.text = "Press SPACEBAR to begin next wave";
            if(Input.GetKeyDown(KeyCode.Space))
            {
                gameState = GameStates.WaveStarting;
            }
        }
        else if(gameState == GameStates.WaveStarting)
        {
            beginNextWaveText.text = "";
            StartCoroutine(SpawnWave());
            gameState = GameStates.WaveInProgress;
        }
        else if(gameState == GameStates.WaveInProgress)
        {
            // Check if all enemies have been killed, if it isn't the last wave, go back to inbetween waves
            if(PlayerStatistics.Instance.GetEnemiesKilled() >= enemiesPerWave)
            {
                Debug.Log("Wave Complete!");
                PlayerStatistics.Instance.ResetEnemiesKilled();
                if(currentWaveCount >= maxWaveCount)
                {
                    Debug.Log("Level Complete!");
                    gameState = GameStates.LevelComplete;
                }
                else
                {
                    Debug.Log("Next Wave!");
                    gameState = GameStates.InbetweenWaves;
                    currentWaveCount++;
                }
            }

        }
        else if(gameState == GameStates.LevelComplete)
        {
            levelCompleteText.text = "Level Complete!";
            if(loadNextLevelOnce)
            {
                loadNextLevelOnce = false;
                StartCoroutine(LoadNextLevel());
            }
        }

        waveCountDownText.text = "Wave: " + currentWaveCount + " / " + maxWaveCount;
    }

    IEnumerator SpawnWave()
    {

        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemySpawns);
        }
        yield return null;
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3f);
        // Load the next level

        PlayerData.UpdateData(true);
        PlayerData.WorldsCompleted[PlayerData.NumberOfWorldsCompleted] = PlayerData.CurrentWorld;
        PlayerData.NumberOfWorldsCompleted += 1;
        Debug.Log("Loading Next Level (Go into code and change this to the next level)");
        if (PlayerData.NumberOfWorldsCompleted < 12)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("World Map Generation");
        }
        else if (PlayerData.NumberOfWorldsCompleted == 12)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
        }
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
