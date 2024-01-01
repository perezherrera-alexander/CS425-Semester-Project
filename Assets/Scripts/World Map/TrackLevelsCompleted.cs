using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrackLevelsCompleted : MonoBehaviour
{
    public GameObject[] worldButton;

    public string[] WorldName;

    public void CompletedLevel(string LevelCompleted)
    {
        Debug.Log(LevelCompleted);
        Debug.Log("i entered completedlevel function");
        for (int i = 0; i < WorldName.Length; i++)
        {
            Debug.Log("I entered the first if satement");
            if (WorldName[i] == LevelCompleted)
            {
                Debug.Log(WorldName[i]);
                Debug.Log("I entered the second if statement");
                worldButton[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

    public void ValidWorlds()
    {
        // Check for current World
        // If current world
        // Highlight all valid worlds the player can go to
            // If player selects valid world
            // load world scene and start game
            // Else tell player to select a valid world
        // Else all invalid worlds are darkened to show it is invalid
    }
}
