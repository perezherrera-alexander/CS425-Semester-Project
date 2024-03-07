using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

public class WaveSpawner : MonoBehaviour
{
    public PlayerData PlayerData;
    [Header("Enemy Prefabs")]
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemyA;
    public GameObject enemyB;
    public GameObject enemyC;
    public GameObject enemyD;
    public GameObject enemyE;
    public struct waveFormation
    {
        public int id;
        public int enemyAmount;
        public float timeBetweenEnemySpawnsSeconds;
    }
    public WaveFormat waveFormat;
    private List<List<waveFormation>> waves = new List<List<waveFormation>>();
    public Transform SpawnPoint;
    [Header("Wave Settings")]
    public GameStates gameState;
    public bool enemiesDoneSpawning = false;
    public int currentWaveCount = 1; // These are important
    public int maxWaveCount; // me too
    //private float timeBetweenWavesTimer;
    
    [Header("UI")]
    public TextMeshProUGUI waveCountDownText;
    public TextMeshProUGUI levelCompleteText;
    public TextMeshProUGUI beginNextWaveText;
    private bool loadNextLevelOnce = true;
    void Start()
    {
        levelCompleteText.text = "";

        // Load wave data from waveFormat
        for(int i = 0; i < waveFormat.waveFormations.Length; i++)
        {
            waves.Add(new List<waveFormation>());
            string[] formationsInWave = waveFormat.waveFormations[i].Split(' ');
            for(int j = 0; j < formationsInWave.Length; j++)
            {
                string[] enemyData = formationsInWave[j].Split(',');
                waveFormation wave = new waveFormation();
                wave.id = int.Parse(enemyData[0]);
                wave.enemyAmount = int.Parse(enemyData[1]);
                wave.timeBetweenEnemySpawnsSeconds = float.Parse(enemyData[2]);
                //Debug.Log("i: " + i + " j: " + j);
                waves.ElementAt(i).Add(wave);
            }
        }
        maxWaveCount = waves.Count;
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
            StartCoroutine(SpawnWave(waves[currentWaveCount-1]));
            gameState = GameStates.WaveInProgress;
        }
        else if(gameState == GameStates.WaveInProgress)
        {
            if(enemiesDoneSpawning && PlayerStatistics.Instance.enemiesPresent == 0) 
            {
                Debug.Log("Wave Complete!");
                //PlayerStatistics.Instance.ResetEnemiesKilled();
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

    IEnumerator SpawnWave(List<waveFormation> formationsForThisWave)
    {
        enemiesDoneSpawning = false;
        foreach(waveFormation formation in formationsForThisWave)
        {
            for(int i = 0; i < formation.enemyAmount; i++)
            {
                SpawnEnemy(formation.id);
                yield return new WaitForSeconds(formation.timeBetweenEnemySpawnsSeconds);
            }
        }
        enemiesDoneSpawning = true;
    }

    void SpawnEnemy(int id)
    {
        // Instantiate enemy type at spawn point
        switch (id)
        {
            case 1:
                Instantiate(enemy1, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 2:
                Instantiate(enemy2, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 3:
                Instantiate(enemy3, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 4:
                Instantiate(enemy4, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 5:
                Instantiate(enemy5, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 6:
                Instantiate(enemyA, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 7:
                Instantiate(enemyB, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 8:
                Instantiate(enemyC, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 9:
                Instantiate(enemyD, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 10:
                Instantiate(enemyE, SpawnPoint.position, SpawnPoint.rotation);
                break;
        }
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

    
}
