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
