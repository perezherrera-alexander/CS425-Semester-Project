using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform SpawnPoint;

    public float timeBetweenWaves = 5f;

    public float EnemyTimeSeperation = 0.5f;

    private float countDown = 2;

    public TextMeshProUGUI waveCountDownText;

    private int WaveNumber = 0;


    // Update is called once per frame
    void Update()
    {
        if (countDown <= 0)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }

        //waveCountDownText.text = Mathf.Ceil(countDown).ToString();

        countDown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        WaveNumber++;

        for (int i = 0; i < WaveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(EnemyTimeSeperation);
        }
        Debug.Log("Wave Spawning");
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
    }
}
