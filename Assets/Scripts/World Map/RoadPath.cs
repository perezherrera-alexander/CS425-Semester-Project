using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RoadPath : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject RoadGroup;
    public GameObject[] Roads1;
    public GameObject[] Roads2;
    public GameObject[] Roads3;
    public GameObject[] Roads4;
    public GameObject[] Roads5;
    public GameObject[] Roads6;
    public GameObject[] Roads7;
    public GameObject[] Roads8;
    public GameObject[] Roads9;
    public GameObject[] Roads10;

    // Start is called before the first frame update
    public void StartingWorldPath()
    {
        string[] SplitString = playerData.CurrentWorld.Split(',');

        int cols = int.Parse(SplitString[0]);
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
            //playerData.RoadPathTaken[1] = Roads1[rows];
        }
        else if (cols == 2)
        {
            Roads2[rows].SetActive(true);
            //playerData.RoadPathTaken[length] = Roads2[rows];
        }

        else if(cols == 3)
        {
            Roads3[rows].SetActive(true);
            //playerData.RoadPathTaken[length] = Roads3[rows];
        }

        else if(cols == 4)
        {
            Roads4[rows].SetActive(true);
            //playerData.RoadPathTaken[length] = Roads4[rows];
        }

        else if(cols == 5)
        {
            Roads5[rows].SetActive(true);
            //playerData.RoadPathTaken[length] = Roads5[rows];
        }

        else if(cols == 6)
        {
            Roads6[rows].SetActive(true);
            //playerData.RoadPathTaken[length] = Roads6[rows];
        }

        else if (cols == 7)
        {
            Roads7[rows].SetActive(true);
            //playerData.RoadPathTaken[length] = Roads7[rows];
        }

        else if (cols == 8)
        {
            Roads8[rows].SetActive(true);
            //playerData.RoadPathTaken[length] = Roads8[rows];
        }

        else if (cols == 9)
        {
            Roads9[rows].SetActive(true);
            //playerData.RoadPathTaken[length] = Roads9[rows];
        }

        else if (cols == 10)
        {
            Roads10[rows].SetActive(true);
            //playerData.RoadPathTaken[length] = Roads10[rows];
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
