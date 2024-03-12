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
    public SaveLoadManager saveLoadManager;
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

        // turn all nodes 3 nodes ahead from last level completed white and more vibrant
        int maxCol = Mathf.Min(playerData.NumberOfWorldsCompleted + 3, WorldButtonsHolder.GetLength(0));
        for (int col = 0; col < maxCol; col++)
        {
            for (int row = 0; row < 7; row++)
            {
                if (worldMapGenerator.WorldsInUseForMapGeneration[col, row] == true)
                {
                    Image buttonImage = WorldButtonsHolder[col, row].GetComponent<Image>();
                    Button buttonComponent = WorldButtonsHolder[col, row].GetComponent<Button>();

                    buttonImage.color = Color.white;
                    buttonComponent.enabled = true;

                    var colors = buttonComponent.colors;
                    var disabledColor = colors.disabledColor;
                    disabledColor.a = 1;
                    colors.disabledColor = disabledColor;
                    buttonComponent.colors = colors;

                    buttonComponent.interactable = false;
                }
            }
        }

        // All worlds completed nodes are turned black
        for (int i = 0; i < playerData.NumberOfWorldsCompleted; i++)
        {
            string[] SplitString = playerData.WorldsCompleted[i].Split(',');

            int col = int.Parse(SplitString[0]);
            int row = int.Parse(SplitString[1]);

            Image buttonImages = WorldButtonsHolder[col, row].GetComponent<Image>();
            Button buttonComponent = WorldButtonsHolder[col, row].GetComponent<Button>();
            buttonImages.color = Color.black;

            var DisabledColor = buttonComponent.colors;
            DisabledColor.disabledColor = Color.white;
            buttonComponent.colors = DisabledColor;
        }

        // All possible worlds you can visit next are turned yellow
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
                    Button buttonComponent = WorldButtonsHolder[newCol, newRow].GetComponent<Button>();

                    buttonImage.color = Color.yellow;
                    buttonComponent.interactable = true;
                }
            }
        }

        // Show all locations of tower unlock points
        for (int i = 0; i < playerData.LocationOfTowerUnlock.Length; i++)
        {
            string[] SplitString = playerData.LocationOfTowerUnlock[i].Split(',');

            int col = int.Parse(SplitString[0]);
            int row = int.Parse(SplitString[1]);

            Image buttonImage = WorldButtonsHolder[col, row].GetComponent<Image>();

            buttonImage.color = Color.blue;
        }

        saveLoadManager.Save();
    }
}
