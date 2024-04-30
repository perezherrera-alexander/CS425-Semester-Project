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
    public GameObject enemy6; //SpaceShip1
    public GameObject enemy7; //SpaceShip2
    public GameObject enemy8; //SpaceShip3
    public GameObject enemy9; //SpaceShip4
    public GameObject enemy10; //SpaceShip5

    public GameObject enemyS1; //Speedster 1
    public GameObject enemyS2; //Speedster 2
    public GameObject enemyS3; //Speedster 3
    public GameObject enemyD1; //Drone 1
    public GameObject enemyD2; //Drone 2
    public GameObject enemyD3; //Drone 3
    public GameObject enemyC1; //Crawler 1
    public GameObject enemyC2; //Crawler 2
    public GameObject enemyC3; //Crawler 3
    public GameObject enemyF; //Fridge
    public GameObject enemyM; //MiniBoss 1
    public GameObject enemyW; //Boss 1
    public GameObject childEnemy; //Robot1
    public GameObject childEnemy2; //Robot2
    public GameObject childEnemy3; //Robot3
    public GameObject childEnemy4; //Robot4
    public GameObject childEnemy5; //Robot5
    
    
    public struct waveFormation
    {
        //enemy id, amount of enemies, time between enemy spawns
        public int id;
        public int enemyAmount;
        public float timeBetweenEnemySpawnsSeconds;
    }
    public List<WaveFormat> waveFormatsEasy;
    public List<WaveFormat> waveFormatsNormal;
    public List<WaveFormat> waveFormatsHard;
    public WaveFormat FinalBossWave;
    public WaveFormat EnemeyShowcaseWave;
    private WaveFormat waveFormat;
    private List<List<waveFormation>> waves = new List<List<waveFormation>>(); // List of waves
    public Transform SpawnPoint;
    [Header("Wave Settings")]
    public GameStates gameState;
    public bool enemiesDoneSpawning = false;
    public int currentWaveCount = 1; // These are important
    public int maxWaveCount; // me too
    public bool autostartNextWave = false;
    //private float timeBetweenWavesTimer;
    public bool tutorialMode = false;
    public bool ShowBossForFinalDemo = false;
    public bool EnemyShowcaseMode = false;
    public WaveFormat waveInUse;
    [Header("UI")]
    public TextMeshProUGUI waveCountDownText;
    public TextMeshProUGUI levelCompleteText;
    public TextMeshProUGUI beginNextWaveText;
    private bool loadNextLevelOnce = true;
    public int expectedEnemies = 0;
    void Start()
    {
        levelCompleteText.text = "";

        LoadWaveFormat();
    }

    void LoadWaveFormat()
    {
        PickWaveFormat();
        waveInUse = waveFormat;

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
                waves.ElementAt(i).Add(wave);                                   // Add the waveFormation to the list of waves
            }
        }
        maxWaveCount = waves.Count;// Set the max wave count
    }
    void PickWaveFormat() 
    {
        if(tutorialMode) {
            waveFormat = waveFormatsEasy[0];
            return;
        }
        if(ShowBossForFinalDemo)
        {
            waveFormat = FinalBossWave;
            return;
        }
        if(EnemyShowcaseMode)
        {
            waveFormat = EnemeyShowcaseWave;
            return;
        }
        switch (SettingsValues.difficulty)
        {
            case 0:
                //Debug.Log("Difficulty: Easy");
                waveFormat = waveFormatsEasy[UnityEngine.Random.Range(0, waveFormatsEasy.Count)];
                break;
            case 1:
                //Debug.Log("Difficulty: Normal");
                waveFormat = waveFormatsNormal[UnityEngine.Random.Range(0, waveFormatsNormal.Count)];
                break;
            case 2:
                //Debug.Log("Difficulty: Hard");
                waveFormat = waveFormatsHard[UnityEngine.Random.Range(0, waveFormatsHard.Count)];
                break;
            default:
                //Debug.Log("Difficulty not set, defaulting to normal");
                waveFormat = waveFormatsNormal[UnityEngine.Random.Range(0, waveFormatsNormal.Count)];
                break;
        }
        Debug.Log("Difficulty: " + SettingsValues.difficulty);
    }

    void Update()
    {
        if(gameState == GameStates.InbetweenWaves)
        {
            if(!autostartNextWave) beginNextWaveText.text = "Press SPACEBAR to begin next wave";
            if(Input.GetKeyDown(KeyCode.Space) || autostartNextWave)
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
        else if(gameState == GameStates.GameOver)
        {
            //Debug.Log("Game Over!");
            // Do nothing, this is a trap state.
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
                Instantiate(enemy6, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 7:
                Instantiate(enemy7, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 8:
                Instantiate(enemy8, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 9:
                Instantiate(enemy9, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 10:
                Instantiate(enemy10, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 11:
                Instantiate(enemyS1, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 12:
                Instantiate(enemyS2, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 13:
                Instantiate(enemyS3, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 14:
                Instantiate(enemyD1, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 15:
                Instantiate(enemyD2, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 16:
                Instantiate(enemyD3, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 17:
                Instantiate(enemyC1, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 18:
                Instantiate(enemyC2, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 19:
                Instantiate(enemyC3, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 20:
                Instantiate(enemyF, SpawnPoint.position, SpawnPoint.rotation);
                break;
            case 21:
                Instantiate(enemyM, SpawnPoint.position, SpawnPoint.rotation);
                break;
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3f);
        // Load the next level

        if(tutorialMode)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
            yield break;
        }

        PlayerData.UpdateData(true);
        PlayerData.WorldsCompleted[PlayerData.NumberOfWorldsCompleted] = PlayerData.CurrentWorld;
        PlayerData.NumberOfWorldsCompleted += 1;
        //Debug.Log("Loading Next Level (Go into code and change this to the next level)");
        if (PlayerData.NumberOfWorldsCompleted < 12)
        {
            GameObject Path = GameObject.Find("World Objects");
            Path = Path.transform.GetChild(0).gameObject;
            // Remove the word "(Clone)" from the end of the path name
            PlayerData.PathsVisited.Add(Path.name.Substring(0, Path.name.Length - 7));
            UnityEngine.SceneManagement.SceneManager.LoadScene("World Map Generation");
        }
        else if (PlayerData.NumberOfWorldsCompleted == 12)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
        }
    }

    public void SetAutoStartNextWave()
    {
        autostartNextWave = !autostartNextWave;
    }

    public void SpawnChildren(int enemyID, int waypointindex, Transform spawnPosition)
    {
        GameObject enemy = null;
        switch (enemyID)
        {
            case 1:
                enemy = childEnemy;
                break;
            case 2:
                enemy = childEnemy2;
                break;
            case 3:
                enemy = childEnemy3;
                break;
            case 4:
                enemy = childEnemy4;
                break;
            case 5:
                enemy = childEnemy5;
                break;
            case 6:
                enemy = enemy6;
                break;
            case 7:
                enemy = enemy7;
                break;
            case 8:
                enemy = enemy8;
                break;
            case 9:
                enemy = enemy9;
                break;
            case 10:
                enemy = enemy10;
                break;
            case 11:
                enemy = enemyS1;
                break;
            case 12:
                enemy = enemyS2;
                break;
            case 13:
                enemy = enemyS3;
                break;
            case 14:
                enemy = enemyD1;
                break;
            case 15:
                enemy = enemyD2;
                break;
            case 16:
                enemy = enemyD3;
                break;
            case 17:
                enemy = enemyC1;
                break;
            case 18:
                enemy = enemyC2;
                break;
            case 19:
                enemy = enemyC3;
                break;
            case 20:
                enemy = enemyF;
                break;
            case 21:
                enemy = enemyM;
                break;
            case 22:
                enemy = enemyW;
                break;
        }
        //spawn 4 Robot1 entities headed towards the current waypoint
        //each robot will have a different start position
        for (int i = 0; i < 4; i++)
        {
            GameObject troop = Instantiate(enemy, spawnPosition.position + new Vector3(i * 1.0f, 0, i * 1.0f), Quaternion.identity);
            troop.GetComponent<BaseEnemyLogic>().differentStart = true;
            //change target aswell
            troop.GetComponent<BaseEnemyLogic>().target = Path.waypoints[waypointindex];
            
            troop.GetComponent<BaseEnemyLogic>().waypointindex = waypointindex;

            
        }
    }
    public void callout()
    {
        Debug.Log("Called out");
    }
}
