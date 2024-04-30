using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RoadPathDemo : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject RoadGroup;
    public GameObject[] Roads1;
    public GameObject[] Roads2;
    public GameObject[] Roads3;
    public GameObject[] Roads4;


    // Start is called before the first frame update
    public void StartingWorldPath()
    {
        string[] SplitString = playerData.CurrentWorld.Split(',');

        int cols = int.Parse(SplitString[0]);
        Debug.Log(cols);
        if (cols == 0)
        {

            RoadGroup.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void SetVisibleCurrentRoadPath()
    {
        string[] SplitString = playerData.CurrentWorld.Split(',');

        int cols = int.Parse(SplitString[0]);
        int rows = int.Parse(SplitString[1]);

        int length = playerData.NumberOfWorldsCompleted - 1;

        if (cols == 1)
        {
            Roads1[rows].SetActive(true);
            //playerData.RoadPathTaken[length] = Roads2[rows];
        }

        else if(cols == 2)
        {
            Roads2[rows].SetActive(true);
            //playerData.RoadPathTaken[length] = Roads3[rows];
        }

        else if(cols == 3)
        {
            Roads3[rows].SetActive(true);
            //playerData.RoadPathTaken[length] = Roads4[rows];
        }

        else if(cols == 4)
        {
            Roads4[rows].SetActive(true);
            //playerData.RoadPathTaken[length] = Roads5[rows];
        }
    }

    public void PathTakenTrail(int col, int row, int position)
    {

        if (position == 0)
        {
            return;
        }

        else
        {
            if (position + 1 < playerData.NumberOfWorldsCompleted)
            {
                string[] SplitString = playerData.WorldsCompleted[position + 1].Split(',');

                int cols = int.Parse(SplitString[0]);
                int rows = int.Parse(SplitString[1]);

                int newRow = rows - row;
                Debug.Log(newRow);

                if (newRow == -1)
                {
                    playerData.RoadPathTaken[position - 1].SetActive(true);
                    playerData.RoadPathTaken[position - 1].transform.GetChild(0).GetComponent<RawImage>().color = Color.black;
                    playerData.RoadPathTaken[position - 1].transform.GetChild(1).GetComponent<RawImage>().enabled = false;
                    playerData.RoadPathTaken[position - 1].transform.GetChild(2).GetComponent<RawImage>().enabled = false;
                }

                else if (newRow == 0)
                {
                    playerData.RoadPathTaken[position - 1].SetActive(true);
                    playerData.RoadPathTaken[position - 1].transform.GetChild(0).GetComponent<RawImage>().enabled = false;
                    playerData.RoadPathTaken[position - 1].transform.GetChild(1).GetComponent<RawImage>().color = Color.black;
                    playerData.RoadPathTaken[position - 1].transform.GetChild(2).GetComponent<RawImage>().enabled = false;
                }

                else if (newRow == 1)
                {
                    playerData.RoadPathTaken[position - 1].SetActive(true);
                    playerData.RoadPathTaken[position - 1].transform.GetChild(0).GetComponent<RawImage>().enabled = false;
                    playerData.RoadPathTaken[position - 1].transform.GetChild(1).GetComponent<RawImage>().enabled = false;
                    playerData.RoadPathTaken[position - 1].transform.GetChild(2).GetComponent<RawImage>().color = Color.black;
                }
            }
        }
    }
}
