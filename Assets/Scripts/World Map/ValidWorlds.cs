using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Runtime.Serialization;

public class ValidWorlds : MonoBehaviour
{
    public PlayerData playerData;
    public TrackLevelsCompleted trackLevelsCompleted;

    public GameObject InvalidWorldMessage;

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
                    OpenPanel();

                }
                else
                {
                    playerData.CurrentWorld = worldName;
                    SceneManager.LoadScene(SceneName[i]);
                }
            }
        }
    }

    public void OpenPanel()
    {
        InvalidWorldMessage.SetActive(true);

        Button closePanelButton = InvalidWorldMessage.transform.GetChild(0).GetChild(1).GetComponent<Button>();

        if (closePanelButton != null )
        {
            closePanelButton.onClick.AddListener(ClosePanel);
        }
    }

    public void ClosePanel()
    {
        InvalidWorldMessage.SetActive(false);
    }
}
