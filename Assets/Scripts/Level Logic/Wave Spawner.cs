using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Globalization;

public class WaveSpawner : MonoBehaviour
{
    public PlayerData PlayerData;
    [Header("Enemy Prefabs")]
    //every enemy type
    public GameObject enemy1; //Robot1
    public GameObject enemy2; //Robot2
    public GameObject enemy3; //Robot3
    public GameObject enemy4; //Robot4
    public GameObject enemy5; //Robot5
    public GameObject enemyA; //SpaceShip1
    public GameObject enemyB; //SpaceShip2
    public GameObject enemyC; //SpaceShip3
    public GameObject enemyD; //SpaceShip4
    public GameObject enemyE; //SpaceShip5

    public GameObject enemyV; //Fast Enemy
    public GameObject enemyW; //Scarab
    public GameObject enemyX; //Drone

    
    public struct waveFormation
    {
        //enemy id, amount of enemies, time between enemy spawns
        public int id;
        public int enemyAmount;
        public float timeBetweenEnemySpawnsSeconds;
    }
    public List<WaveFormat> waveFormats;
    private WaveFormat waveFormat;
    private List<List<waveFormation>> waves = new List<List<waveFormation>>(); // List of waves
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

        // Pick a random waveFormat from the list
        waveFormat = waveFormats[UnityEngine.Random.Range(0, waveFormats.Count)];

        // Load wave data from waveFormat
        for(int i = 0; i < waveFormat.waveFormations.Length; i++)
        {
            waves.Add(new List<waveFormation>());                               // Add a new list for each wave
            string[] formationsInWave = waveFormat.waveFormations[i].Split(' ');// Split the wave into formations
            for(int j = 0; j < formationsInWave.Length; j++)                    // For each formation in the wave
            {
                string[] enemyData = formationsInWave[j].Split(',');            // Split the formation into enemy data
                waveFormation wave = new waveFormation();                       // Create a new waveFormation
                wave.id = int.Parse(enemyData[0]);                              // Set the enemy id
                wave.enemyAmount = int.Parse(enemyData[1]);                     // Set the amount of enemies
                wave.timeBetweenEnemySpawnsSeconds = float.Parse(enemyData[2], CultureInfo.InvariantCulture); // Set the time between enemy spawns
                //Debug.Log("i: " + i + " j: " + j);
                waves.ElementAt(i).Add(wave);                                   // Add the waveFormation to the list of waves
            }
        }
        maxWaveCount = waves.Count;// Set the max wave count
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
            if(enemiesDoneSpawning && PlayerStatistics.Instance.enemiesPresent == 0) // If all enemies are dead and no more are spawning
            {
                Debug.Log("Wave Complete!");
                //PlayerStatistics.Instance.ResetEnemiesKilled();
                if(currentWaveCount >= maxWaveCount)  // If the last wave is complete
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
        enemiesDoneSpawning = false;                                // Set enemiesDoneSpawning to false
        foreach(waveFormation formation in formationsForThisWave)   // For each formation in the wave
        {
            for(int i = 0; i < formation.enemyAmount; i++)          // For each enemy in the formation
            {
                SpawnEnemy(formation.id);                   // Spawn the enemy
                yield return new WaitForSeconds(formation.timeBetweenEnemySpawnsSeconds);   // Wait for the time between enemy spawns
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
            case 11:
                Instantiate(enemyV, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 12:
                Instantiate(enemyW, SpawnPoint.position, SpawnPoint.rotation);
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
