using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerTargetingUI : MonoBehaviour
{
    basicTowerScript tower;

    void Start()
    {
        tower = GameObject.FindObjectOfType<basicTowerScript>();
    }
    public void TowerTargeting(int index)
    {
        switch (index)
        {
            case 0:
                tower.GetComponentInChildren<basicTowerScript>().targeting = "first";
                break;

            case 1:
                tower.GetComponentInChildren<basicTowerScript>().targeting = "last";
                break;

            case 2:
                tower.GetComponentInChildren<basicTowerScript>().targeting = "close";
                break;

            case 3:
                tower.GetComponentInChildren<basicTowerScript>().targeting = "strong";
                break;
        }
    }
}
