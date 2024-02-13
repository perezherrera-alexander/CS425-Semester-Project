using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoraleUI : MonoBehaviour
{
    [Header("Component References")]
    public TextMeshProUGUI currentMoraleText;
    private PlayerStatistics playerStatisticsReference; // Reference to PlayerStatistics script (found at runtime)

    [Header("Slider Components")]
    public Slider slider;
    public Gradient sliderGradient;
    public Image sliderFill;
    private int maxMoraleAmount;
    private int currentMoraleScore;

    
    void Start()
    {
        // Code originally written by Edward Martinez, moved and slightly modified by Alexander Perez
        
        // Checks if PlayerStatistics instance exists, if so, get reference to it
        GameObject playerStatsObject = GameObject.Find("Game Master");

        if (playerStatsObject != null) {
            playerStatisticsReference = playerStatsObject.GetComponent<PlayerStatistics>();
            if (playerStatisticsReference != null) {
                maxMoraleAmount = playerStatisticsReference.GetMaxMorale();
                currentMoraleScore = playerStatisticsReference.GetMorale();
            }
            else {
                Debug.Log("PlayerStatistics instance not found");
            }
        }
        else {
            Debug.Log("Game Master not found");
        }
    }
    void Update()
    {
        currentMoraleText.text = Mathf.Ceil(playerStatisticsReference.GetMorale()).ToString();
    }

    // Set & Get Morale functions (as well as the original slider functionality) come from a different project (CS 328) Alexander Perez worked on
    public void SetMorale(float morale) // SetMorale gets called in PlayerStatistics Update() function
    {
        slider.value = morale;
        sliderFill.color = sliderGradient.Evaluate(slider.normalizedValue);
    }
    public void SetMaxMorale(float morale) // Only needs to be used once (at start) unless we want to change max morale during gameplay
    {
        int iMorale = (int) morale;
        slider.maxValue = iMorale;
        slider.value = iMorale;

        sliderFill.color = sliderGradient.Evaluate(1f);
    }
}