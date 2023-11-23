using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EvolutionPointsUI : MonoBehaviour
{
    public TextMeshProUGUI EvolutionPointsTotal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject playerStatsObject = GameObject.Find("Game Master");

        if (playerStatsObject != null)
        {
            PlayerStats playerStats = playerStatsObject.GetComponent<PlayerStats>();

            if (playerStats != null)
            {
                int CurrentEvolutionPointsTotal = playerStats.GetMoney();

                EvolutionPointsTotal.text = "Evolution Points: " + Mathf.Ceil(CurrentEvolutionPointsTotal).ToString();
            }
            else
                Debug.Log("playerstats empty");
        }
        else
            Debug.Log("playerstatsobject empty");
    }
}
