using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public int WaveNumber = 0;


    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            StartCoroutine(SpawnWave());
            timer = timeBetweenWaves;
        }

        waveCountDownText.text = "Wave: " + Mathf.Ceil(countDown).ToString() + " / 20";

        timer -= Time.deltaTime;
        countDown += Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {

        for (int i = 0; i < WaveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(EnemyTimeSeperation);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab1, SpawnPoint.position, SpawnPoint.rotation);
        Instantiate(enemyPrefab2, SpawnPoint.position, SpawnPoint.rotation);
    }
}
