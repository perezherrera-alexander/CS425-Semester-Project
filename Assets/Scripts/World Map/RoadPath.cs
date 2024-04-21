using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        int rows = int.Parse(SplitString[1]);
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

        if (cols == 1)
        {
            Roads1[rows].SetActive(true);
        }
        else if (cols == 2)
        {
            Roads2[rows].SetActive(true);
        }

        else if(cols == 3)
        {
            Roads3[rows].SetActive(true);
        }

        else if(cols == 4)
        {
            Roads4[rows].SetActive(true);
        }

        else if(cols == 5)
        {
            Roads5[rows].SetActive(true);
        }

        else if(cols == 6)
        {
            Roads6[rows].SetActive(true);
        }

        else if (cols == 7)
        {
            Roads7[rows].SetActive(true);
        }

        else if (cols == 8)
        {
            Roads8[rows].SetActive(true);
        }

        else if (cols == 9)
        {
            Roads9[rows].SetActive(true);
        }

        else if (cols == 10)
        {
            Roads10[rows].SetActive(true);
        }
    }
}
