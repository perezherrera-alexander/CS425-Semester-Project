using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class TestWorldMap
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("Game View");
    }

    [UnityTest]
    public IEnumerator LoadWorldMap()
    {
        yield return new WaitForSeconds(2f); // Adjust the time as per your scene loading time

        var gameMaster = GameObject.Find("Game Master");

        Assert.IsNotNull(gameMaster, "Game Master object was not found");

        if (gameMaster != null)
        {
            var waveSpawner = gameMaster.GetComponent<WaveSpawner>();

            Assert.IsNotNull(waveSpawner, "WaveSpawner component not found on Game Master object");

            waveSpawner.gameState = GameStates.LevelComplete;

            // Wait for one frame for game state transition
            yield return null;

            // Check if the game state transitions as expected
            Assert.AreEqual(GameStates.LevelComplete, waveSpawner.gameState, "Game state did not transition to LevelComplete after setting currentWaveCount to 10");
        }
    }
}
