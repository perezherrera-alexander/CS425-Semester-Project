using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EvolutionPointsUI : MonoBehaviour
{
    public TextMeshProUGUI EvolutionPointsTotal;

    // Evolution Points icon (the dna strand) attribution:
    // DNA icon by Timothy Dilich from Noun Project CC BY 1.0
    void Start()
    {

    }

    void Update()
    {
        GameObject playerStatsObject = GameObject.Find("Game Master");

        if (playerStatsObject != null)
        {
            PlayerStatistics playerStatistics = playerStatsObject.GetComponent<PlayerStatistics>();

            if (playerStatistics != null)
            {
                int CurrentEvolutionPointsTotal = playerStatistics.GetMoney();

                EvolutionPointsTotal.text = Mathf.Ceil(CurrentEvolutionPointsTotal).ToString();
            }
            else
                Debug.Log("playerstats empty");
        }
        else
            Debug.Log("playerstatsobject empty");
    }
}
