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
    //public TrackLevelsCompleted trackLevelsCompleted;

    //public GameObject InvalidWorldMessage;

    public string[] SceneName;




    public GameObject[,] WorldButtonsHolder;

    public bool[,] WorldsInUseForMapGenerationHolder;

    public WorldMapGenerator worldMapGenerator;

    int[,] directions = { { -1, 1 }, { 0, 1 }, { 1, 1 } };

    public void Start()
    {
        WorldButtonsHolder = worldMapGenerator.WorldButtons;

        WorldsInUseForMapGenerationHolder = worldMapGenerator.WorldsInUseForMapGeneration;

        for (int i = 0; i < playerData.NumberOfWorldsCompleted; i++)
        {
            string[] SplitString = playerData.WorldsCompleted[i].Split(',');

            int col = int.Parse(SplitString[0]);
            int row = int.Parse(SplitString[1]);

            Image buttonImages = WorldButtonsHolder[col, row].GetComponent<Image>();
            buttonImages.color = Color.red;
        }


        for (int i = 0; i < 3; i++)
        {

            string[] SplitString = playerData.WorldsCompleted[playerData.NumberOfWorldsCompleted - 1].Split(',');

            int col = int.Parse(SplitString[0]);
            int row = int.Parse(SplitString[1]);

            int newRow = row + directions[i, 0];
            int newCol = col + directions[i, 1];

            // Check if the new position is within bounds
            if (newRow >= 0 && newRow < 7 && newCol >= 0 && newCol < 12)
            {
                // Check if the adjacent node is not in use
                if (WorldsInUseForMapGenerationHolder[newCol, newRow] == true)
                {
                    // Get the adjacent node
                    WorldNode adjacentNode = WorldButtonsHolder[newCol, newRow].GetComponent<WorldNode>();

                    Image buttonImage = WorldButtonsHolder[newCol, newRow].GetComponent<Image>();

                    buttonImage.color = Color.yellow;
                }
            }
        }
    }

    public void ValidWorld ()
    {

    }

    /*
    // Check if player is selecting a world already completed
    public void SelectingValidWorlds(string worldName)
    {
        for (int i = 0; i < trackLevelsCompleted.WorldName.Length; i++)
        {
            if (worldName == trackLevelsCompleted.WorldName[i])
            {
                if (trackLevelsCompleted.completed[i] == true)
                {
                    Debug.Log("World has already been completed, select a new world to move forward");
                    //OpenPanel();

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

        if (closePanelButton != null)
        {
            closePanelButton.onClick.AddListener(ClosePanel);
        }
    }

    public void ClosePanel()
    {
        InvalidWorldMessage.SetActive(false);
    }
    */
}
