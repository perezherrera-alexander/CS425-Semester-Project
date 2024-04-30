using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridLayoutBluePrintDemo : MonoBehaviour
{
    public GameObject WorldButtonPrefab;
    public GameObject WorldButtonParent;
    public WorldMapGeneratorDemo worldMapGeneratorDemo;

    public int cols = 6;
    public int rows = 5;

    public bool[,] WorldCompleted;
    public GameObject[,] WorldButtonGrid;
    public bool[,] WorldsInUseForMapGeneration;

    private void OnEnable()
    {
        WorldButtonGrid = new GameObject[rows, cols];
        WorldCompleted = new bool[rows, cols];
        WorldsInUseForMapGeneration = new bool[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
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

                buttonComponent.interactable = false;

                WorldsInUseForMapGeneration[row, col] = false;

                if (col == 0 && row == 2)
                {
                    buttonImage.enabled = true;
                    buttonImage.color = Color.black;
                    buttonComponent.enabled = true;
                    WorldsInUseForMapGeneration[row, col] = true;
                }

                if ((col == 1 || col == 4) && (row == 1 || row == 2 || row == 3))
                {
                    buttonImage.enabled = true;
                    buttonImage.color = Color.white;
                    buttonComponent.enabled = true;
                    WorldsInUseForMapGeneration[row, col] = true;
                }

                if ((col == 2 || col == 3) && (row == 0 || row == 1 || row == 2 || row == 3 || row == 4))
                {
                    buttonImage.enabled = true;
                    buttonImage.color = Color.white;
                    buttonComponent.enabled = true;
                    WorldsInUseForMapGeneration[row, col] = true;
                }

                if (col == 5 && row == 2)
                {
                    buttonImage.enabled = true;
                    buttonImage.color = Color.cyan;
                    buttonComponent.enabled = true;
                    WorldsInUseForMapGeneration[row, col] = true;
                }
            }
        }
        worldMapGeneratorDemo.Obtain2dArrays(WorldButtonGrid, WorldsInUseForMapGeneration);
    }
}