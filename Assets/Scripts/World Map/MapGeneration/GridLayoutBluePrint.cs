using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutBluePrint : MonoBehaviour
{
    public GameObject WorldButtonPrefab;
    public GameObject WorldButtonParent;
    public WorldMapGenerator worldMapGenerator;

    public int cols = 12;
    public int rows = 7;

    public bool[,] WorldCompleted;
    public GameObject[,] WorldButtonGrid;
    public bool[,] WorldsInUseForMapGeneration;

    private void OnEnable()
    {
        WorldButtonGrid = new GameObject[rows, cols];
        WorldCompleted = new bool[rows, cols];
        WorldsInUseForMapGeneration = new bool[rows, cols];

        for (int row = 0; row < 7; row++)
        {
            for (int col = 0; col < 12; col++)
            {
                GameObject WorldButton = Instantiate(WorldButtonPrefab);
                WorldButton.transform.SetParent(WorldButtonParent.transform);

                WorldButtonGrid[row, col] = WorldButton;
                WorldCompleted[row, col] = false;

                Image buttonImage = WorldButtonGrid[row, col].GetComponent<Image>();
                Button buttonComponent = WorldButtonGrid[row, col].GetComponent<Button>();

                buttonImage.enabled = false;
                buttonComponent.enabled = false;
                buttonImage.color = Color.gray;

                WorldsInUseForMapGeneration[row, col] = false;

                if (col == 0 && row == 3)
                {
                    buttonImage.enabled = true;
                    buttonImage.color = Color.black;
                    buttonComponent.enabled = true;
                    WorldsInUseForMapGeneration[row, col] = true;
                }

                if ((col == 1 || col == 10) && (row == 2 || row == 3 || row == 4))
                {
                    buttonImage.enabled = true;
                    buttonImage.color = Color.white;
                    buttonComponent.enabled = true;
                    WorldsInUseForMapGeneration[row, col] = true;
                }

                if ((col == 2 || col == 9) && (row == 1 || row == 2 || row == 3 || row == 4 || row == 5))
                {
                    buttonImage.enabled = true;
                    buttonImage.color = Color.white;
                    buttonComponent.enabled = true;
                    WorldsInUseForMapGeneration[row, col] = true;
                }

                if ((col > 2 && col < 9))
                {
                    buttonImage.enabled = true;
                    buttonImage.color = Color.white;
                    buttonComponent.enabled = true;
                    WorldsInUseForMapGeneration[row, col] = true;
                }

                if (col == 11 && row == 3)
                {
                    buttonImage.enabled = true;
                    buttonImage.color = Color.cyan;
                    buttonComponent.enabled = true;
                    WorldsInUseForMapGeneration[row, col] = true;
                }
            }
        }
        worldMapGenerator.Obtain2dArrays(WorldButtonGrid, WorldsInUseForMapGeneration);
    }
}