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
        for (int i = 0; i < PlayerData.NumberOfWorldsCompleted; i++)
        {
            worldName = PlayerData.WorldsCompleted[i];
            trackLevelsCompleted.CompletedLevel(worldName);
        }
    }

}
