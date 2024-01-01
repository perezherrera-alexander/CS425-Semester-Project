using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ValidWorlds : MonoBehaviour
{
    public PlayerData playerData;
    public TrackLevelsCompleted trackLevelsCompleted;

    public string[] SceneName;

    // Check if player is selecting a world already completed
    public void SelectingValidWorlds(string worldName)
    {      
        for(int i = 0; i < trackLevelsCompleted.WorldName.Length; i++)
        {
            if(worldName == trackLevelsCompleted.WorldName[i])
            {
                if (trackLevelsCompleted.completed[i] == true)
                {
                    Debug.Log("World has already been completed, select a new world to move forward");
                }
                else
                {
                    playerData.CurrentWorld = worldName;
                    SceneManager.LoadScene(SceneName[i]);
                }
            }
        }
    }
}
