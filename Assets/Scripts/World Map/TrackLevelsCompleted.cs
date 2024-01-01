using UnityEngine;
using UnityEngine.UI;

public class TrackLevelsCompleted : MonoBehaviour
{
    public GameObject[] worldButton;

    public string[] WorldName;

    public bool[] completed;

    public void CompletedLevel(string LevelCompleted)
    {
        Debug.Log(LevelCompleted);
        for (int i = 0; i < WorldName.Length; i++)
        {
            if (WorldName[i] == LevelCompleted)
            {
                Debug.Log(WorldName[i]);
                worldButton[i].GetComponent<Image>().color = Color.black;
                completed[i] = true;
            }
        }
    }
}
