using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoraleUI : MonoBehaviour
{
    public TextMeshProUGUI CurrentMorale;

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
                int CurrentMoralescore = playerStats.GetMorale();

                CurrentMorale.text = "Morale: " + Mathf.Ceil(CurrentMoralescore).ToString();
            }
            else
                Debug.Log("playerstats empty");
        }
        else
            Debug.Log("playerstatsobject empty");
    }
}