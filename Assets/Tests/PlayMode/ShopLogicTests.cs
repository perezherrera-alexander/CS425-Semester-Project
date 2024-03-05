using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class ShopLogicTests
{
    [SetUp]
    public void Setup()
    {
        SceneManager.LoadScene("Game View");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator OpenAndCloseShop()
    {
        var GameObject = new GameObject();
        GameObject = GameObject.Find("Shop Logic");

        if(GameObject == null)
        {
            Debug.Log("GameObject is null");
            Assert.Fail();
        }
        else GameObject.GetComponent<ShopLogic>().ToggleShopUI();

        yield return null; // Use yield to skip a frame.

        // Use the Assert class to test conditions.
        Assert.IsTrue(GameObject.GetComponent<ShopLogic>().shopIsOpen);
    }

    // [UnityTest]
    // public IEnumerator CheckEnemyMovesOnPath()
    // {
    //     // check if the drone exists
    //     var drone = new GameObject();
    //     drone = GameObject.Find("Drone");
    //     if(drone == null)
    //     {
    //         Debug.Log("Drone not found");
    //         Assert.Fail();
    //     }
        

    // }

    //Test that the waypoints object has more than one waypoint in it
    [UnityTest]
    public IEnumerator CheckWaypoints()
    {
        var path = new GameObject();
        path = GameObject.Find("Path");
        if(path == null)
        {
            Debug.Log("Path not found");
            Assert.Fail();
        }
        yield return null;
        Assert.Greater(Path.waypoints.Length, 8);
    }

    [UnityTest]
    public IEnumerator PurchaseTowerFromShop()
    {
        // Find Shop Logic and make sure it exists
        var shopLogic = new GameObject();
        shopLogic = GameObject.Find("Shop Logic");
        if(shopLogic == null)
        {
            Debug.Log("Shop Logic GameObject not found");
            Assert.Fail();
        }

        // Find Game Master and then its Player Statistics script
        var playerData = new GameObject();
        playerData = GameObject.Find("Game Master");
        if(playerData == null)
        {
            Debug.Log("Game Master GameObject not found");
            Assert.Fail();
        }
        var playerStatistics = playerData.GetComponent<PlayerStatistics>();
        if(playerStatistics == null)
        {
            Debug.Log("Player Statistics not found");
            Assert.Fail();
        }

        // Check the player's money before and after purchasing a tower
        var initialMoney = playerStatistics.GetMoney();
        shopLogic.GetComponent<ShopLogic>().PurchaseTower("Bee Tower");
        var finalMoney = playerStatistics.GetMoney();

        yield return null;
        Assert.AreNotEqual(initialMoney, finalMoney);
    }

    [TearDown]
    public void Teardown()
    {
        // This isn't strictly necessary and Unity throws a warning when I use this function
        //SceneManager.UnloadSceneAsync("Game View");
    }
}
