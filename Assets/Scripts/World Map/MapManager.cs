using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public PlayerData PlayerData;
    public TrackLevelsCompleted trackLevelsCompleted;

    public string worldName = "";

    void Start()
    {
        if (PlayerData.LevelCompleted == true)
        {
            for (int i = 0; i < PlayerData.NumberOfWorldsCompleted; i++)
            {
                worldName = PlayerData.WorldsCompleted[i];
                trackLevelsCompleted.CompletedLevel(worldName);
            }

            PlayerData.LevelCompleted = false;
        }
        else
        {
            for (int i = 0; i < PlayerData.NumberOfWorldsCompleted; i++)
            {
                worldName = PlayerData.WorldsCompleted[i];
                trackLevelsCompleted.CompletedLevel(worldName);
            }
        }
    }

}
