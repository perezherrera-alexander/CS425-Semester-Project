using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public PlayerData playerData;

    private void Start()
    {
        
    }

    public void StartWorld1 ()
    {
        string WorldName = "World1";
        playerData.CurrentWorld = WorldName;
        SceneManager.LoadScene("Game View");
    }

    public void StartWorld2 ()
    {
        string WorldName = "World2";
        playerData.CurrentWorld = WorldName;
        SceneManager.LoadScene("Game View 1");
    }

    public void StartWorld3 ()
    {
        string WorldName = "World3";
        playerData.CurrentWorld = WorldName;
        SceneManager.LoadScene("Game View 2");
    }

    public void StartWorld4 ()
    {
        string WorldName = "World4";
        playerData.CurrentWorld = WorldName;
        SceneManager.LoadScene("Game View 3");
    }

    public void StartWorld5 ()
    {
        string WorldName = "World5";
        playerData.CurrentWorld = WorldName;
        SceneManager.LoadScene("Game View 4");
    }

    public void StartWorld6 ()
    {
        string WorldName = "World6";
        playerData.CurrentWorld = WorldName;
        SceneManager.LoadScene("Game View 5");
    }

    public void StartWorld7 ()
    {
        string WorldName = "World7";
        playerData.CurrentWorld = WorldName;
        SceneManager.LoadScene("Game View 6");
    }

    public void StartWorld8 ()
    {
        string WorldName = "World8";
        playerData.CurrentWorld = WorldName;
        SceneManager.LoadScene("Game View 7");
    }
}
