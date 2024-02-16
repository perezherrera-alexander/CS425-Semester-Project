using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldMapGenerator : MonoBehaviour
{
    public GridLayoutBluePrint grid;
    public PlayerData playerData;

    public GameObject[,] WorldButtons;
    public bool[,] WorldsInUseForMapGeneration;
    public bool[,] ConnectedWorld;

    public bool[,] ActiveWorlds;

    public void Obtain2dArrays(GameObject[,] buttons, bool[,] status)
    {
        // Assuming buttons and status are [7, 12] arrays
        int rows = buttons.GetLength(0);
        int cols = buttons.GetLength(1);

        WorldButtons = new GameObject[cols, rows];
        WorldsInUseForMapGeneration = new bool[cols, rows];

        for (int col = 0; col < cols; col++)
        {
            for (int row = 0; row < rows; row++)
            {
                WorldButtons[col, row] = buttons[row, col];
                WorldsInUseForMapGeneration[col, row] = status[row, col];
            }
        }
        SetWorldButtons();
        GenerateConnections();
    }

    // Set the onclick event for the buttons
    public void SetWorldButtons()
    {
        for (int col = 0; col < WorldButtons.GetLength(0); col++)
        {
            for (int row = 0; row < WorldButtons.GetLength(1); row++)
            {
                InitializeWorldNode(col, row);
            }
        }
    }

    // Setting up the WorldNode for each button object to store information
    private void InitializeWorldNode(int col, int row)
    {
        GameObject buttonObject = WorldButtons[col, row];
        WorldNode worldNode = buttonObject.GetComponent<WorldNode>();

        if (worldNode == null)
        {
            worldNode = buttonObject.AddComponent<WorldNode>();
        }

        bool isStartingWorldNode = (col == 0) && (row == 3);
        bool isEndingWorldNode = (col == 11) && (row == 3);

        worldNode.InitializeNode(row, col, isStartingWorldNode, isEndingWorldNode);

        // Set the button click action
        worldNode.SetButtonClickAction(OnButtonClick);
    }

    // The onbuttonclick event that executes when a button is pressed
    void OnButtonClick(int col, int row)
    {
        WorldNode worldNode = WorldButtons[col, row].GetComponent<WorldNode>();

        if (worldNode != null)
        {
            Debug.Log("Button clicked at: " + col + ", " + row);

            // Print information about the clicked node and its connections
            Debug.Log("Connections from this node:");
            foreach (var adjacentNode in worldNode.Connections)
            {
                Debug.Log("Adjacent Node: " + adjacentNode.col + ", " + adjacentNode.row);
            }

            // Print information about nodes connecting to this node
            Debug.Log("Nodes connecting to this node:");
            foreach (var otherNode in WorldButtons)
            {
                WorldNode otherWorldNode = otherNode.GetComponent<WorldNode>();
                if (otherWorldNode != null && otherWorldNode.Connections.Contains(worldNode))
                {
                    Debug.Log("Connected Node: " + otherWorldNode.col + ", " + otherWorldNode.row);
                }
            }
        }
        else
        {
            Debug.Log("Button clicked at: " + col + ", " + row + " - No WorldNode component found");
        }

        /*int World = UnityEngine.Random.Range(0, 8);
        if (World == 0)
            SceneManager.LoadScene("Game View");
        else if (World == 1)
            SceneManager.LoadScene("Game View 1");
        else if (World == 2)
            SceneManager.LoadScene("Game View 2");
        else if (World == 3)
            SceneManager.LoadScene("Game View 3");
        else if (World == 4)
            SceneManager.LoadScene("Game View 4");
        else if (World == 5)
            SceneManager.LoadScene("Game View 5");
        else if (World == 6)
            SceneManager.LoadScene("Game View 6");
        else if (World == 7)
            SceneManager.LoadScene("Game View 7");
        */
        SceneManager.LoadScene("Game View");

        string combineddata = string.Join(",", col, row);

        playerData.CurrentWorld = combineddata; 
    }

    // The function that finds and stores the connection of each current node for the next possible nodes
    private void GenerateConnections()
    {
        // List of possible directions to connect the node to the next node (top-right, right, bottom-right)
        int[,] directions = { { -1, 1 }, { 0, 1 }, { 1, 1 } };

        ActiveWorlds = new bool[12, 7];
        ActiveWorlds[0, 3] = true;

        // Iterate through all nodes in the grid
        for (int col = 0; col < 12; col++)
        {
            for (int row = 0; row < 7; row++)
            {
                // Check if the node is not in use
                if (WorldsInUseForMapGeneration[col, row] == true && ActiveWorlds[col, row] == true)
                {
                    Image buttonImages = WorldButtons[col, row].GetComponent<Image>();
                    Button buttonComponents = WorldButtons[col, row].GetComponent<Button>();

                    buttonImages.enabled = true;
                    buttonComponents.enabled = true;

                    // Get the current node
                    WorldNode currentNode = WorldButtons[col, row].GetComponent<WorldNode>();

                    // Iterate through possible directions (top-right, right, bottom-right)
                    for (int i = 0; i < directions.GetLength(0); i++)
                    {
                        int newRow = row + directions[i, 0];
                        int newCol = col + directions[i, 1];

                        // Check if the new position is within bounds
                        if (newRow >= 0 && newRow < WorldButtons.GetLength(1) && newCol >= 0 && newCol < WorldButtons.GetLength(0))
                        {
                            // Check if the adjacent node is not in use
                            if (WorldsInUseForMapGeneration[newCol, newRow] == true)
                            {
                                    // Get the adjacent node
                                    WorldNode adjacentNode = WorldButtons[newCol, newRow].GetComponent<WorldNode>();

                                    // Add connection between the current node and adjacent node
                                    currentNode.AddConnection(adjacentNode);

                                    Image buttonImage = WorldButtons[newCol, newRow].GetComponent<Image>();
                                    Button buttonComponent = WorldButtons[newCol, newRow].GetComponent<Button>();

                                    buttonImage.enabled = true;
                                    buttonComponent.enabled = true;
                                    ActiveWorlds[newCol, newRow] = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
