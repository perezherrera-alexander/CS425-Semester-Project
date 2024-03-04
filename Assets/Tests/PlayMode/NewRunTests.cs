using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor;

public class NewRunTests 
{
    [SetUp]

    public void Setup()
    {
        SceneManager.LoadScene("New Run");
    }

    [UnityTest]

    public IEnumerator NewRunLoaded()
    {
        var menu = new GameObject();
        menu = GameObject.Find("Canvas");
        if(menu == null)
        {
            Debug.Log("Menu GameObject not found");
            Assert.Fail();
        }


        yield return null;
    }

    [UnityTest]

    public IEnumerator GeneralSelection()
    {

        var menu = new GameObject();
        menu = GameObject.Find("Canvas");
        menu.GetComponent<NewLevel>().generalSelect("Bee");
        var name = menu.GetComponent<NewLevel>().getGeneralName();
        if (name == null)
        {
            Debug.Log("General Not selected");
            Assert.Fail();
        }

        yield return null;

    }

    [UnityTest]

    public IEnumerator ChangeToGameView()
    {
        var menu = new GameObject();
        menu = GameObject.Find("Canvas");
        menu.GetComponent<NewLevel>().goToScene("Game View");
        yield return null;
        var game = new GameObject();
        game = GameObject.Find("PlayerInterface");
        if (game == null)
        {
            Debug.Log("Player Interface not found");
            Assert.Fail();
        }

        
    }

    [UnityTest]

    public IEnumerator ChangeToMainMenu()
    {
        var menu = new GameObject();
        menu = GameObject.Find("Canvas");
        menu.GetComponent<NewLevel>().goToScene("Main Menu");
        yield return null;
        var game = new GameObject();
        game = GameObject.Find("MainMenu");
        if (game == null)
        {
            Debug.Log("Main Menu not found");
            Assert.Fail();
        }


    }
}
