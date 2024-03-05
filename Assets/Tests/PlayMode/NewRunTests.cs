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
        //Finding the Canvas object to check if the scene New Run loaded in
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
        //Testing the functionality of the general select function by checking to see if the name of the general gets assigned
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
        //Testing the funcitonality of the new run button by checking to see if the player interface object exists which can only be found on the game view scene
        if (game == null)
        {
            Debug.Log("Player Interface not found");
            Assert.Fail();
        }

        
    }

    //Test that the waypoints object has more than one waypoint in it
    [UnityTest]
    public IEnumerator CheckWaypoints()
    {
        var menu = new GameObject();
        menu = GameObject.Find("Canvas");
        menu.GetComponent<NewLevel>().goToScene("Game View");
        yield return null;
        //check for a waypoint system that is non-empty

        var path = new GameObject();
        path = GameObject.Find("Path");
        if(path == null)
        {
            Debug.Log("Path not found");
            Assert.Fail();
        }
        yield return null;
        //set to 8 because there are 9 waypoints in the scene, and too simple paths are bad
        Assert.Greater(Path.waypoints.Length, 8);
        
    }

    //Eventually test that enemies can spawn and move along the path
    //[UnityTest]
    //public IEnumerator CheckEnemySpawns()
    
    //[UnityTest]
    //public IEnumerator CheckEnemyMovesOnPath()

    [UnityTest]

    public IEnumerator ChangeToMainMenu()
    {
        var menu = new GameObject();
        menu = GameObject.Find("Canvas");
        menu.GetComponent<NewLevel>().goToScene("Main Menu");
        yield return null;
        var game = new GameObject();
        game = GameObject.Find("MainMenu");
        //Testing the functionality of the return to main menu button by checking to see if the main menu object exists which can only by found on the main menu scene
        if (game == null)
        {
            Debug.Log("Main Menu not found");
            Assert.Fail();
        }


    }
}
