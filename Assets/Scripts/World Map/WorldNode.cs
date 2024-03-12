using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldNode : MonoBehaviour
{
    public int row;
    public int col;
    public List<WorldNode> Connections;
    public bool IsStartingWorldNode { get; private set; }
    public bool IsEndingWorldNode { get; private set; }

    public void InitializeNode(int row, int col, bool StartWorld, bool EndWorld)
    {
        this.row = row;
        this.col = col;
        this.IsStartingWorldNode = StartWorld;
        this.IsEndingWorldNode = EndWorld;
        Connections = new List<WorldNode>();
    }

    public void SetButtonClickAction(System.Action<int, int> clickAction)
    {
        // Set the button click action
        GetComponent<Button>().onClick.AddListener(() => clickAction.Invoke(col, row));
    }

    public void AddConnection(WorldNode otherNode)
    {
        // Add the other node to the list of connections
        Connections.Add(otherNode);
    }
}
