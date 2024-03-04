using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
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

        // Use yield to skip a frame.
        yield return null;

        // Use the Assert class to test conditions.
        Assert.IsTrue(GameObject.GetComponent<ShopLogic>().shopIsOpen);
    }

    [UnityTest]
    public IEnumerator PurchaseTowerFromShop()
    {
        
        var shopLogic = new GameObject();
        shopLogic = GameObject.Find("Shop Logic");
        if(shopLogic == null)
        {
            Debug.Log("Shop Logic GameObject not found");
            Assert.Fail();
        }

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

        var initialMoney = playerStatistics.GetMoney();
        shopLogic.GetComponent<ShopLogic>().PurchaseTower("Bee Tower");
        var finalMoney = playerStatistics.GetMoney();

        // Use yield to skip a frame.
        yield return null;

        // Use the Assert class to test conditions.
        Assert.AreNotEqual(initialMoney, finalMoney);
    }

    [TearDown]
    public void Teardown()
    {
        //SceneManager.UnloadSceneAsync("Game View");
    }
}
