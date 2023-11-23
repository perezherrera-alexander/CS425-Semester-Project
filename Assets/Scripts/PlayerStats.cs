using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Singleton instance
    public static PlayerStats Instance;

    public int Morale = 100;
    public int CurrentEvolutionPoints = 20;

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney (int MoneyGained)
    {
        CurrentEvolutionPoints += MoneyGained;
    }

    public int GetMoney ()
    {
        return CurrentEvolutionPoints;
    }

    public void ReduceMorale(int MoraleLost)
    {
        Morale -= MoraleLost;
    }

    public int GetMorale ()
    {
        return Morale;
    }


    public PlayerStats GetPlayerStats ()
    {
        return this;
    }

    public void SavePlayer ()
    {
        SaveLoadManager.SaveStats(this);
    }

    public void LoadPlayer ()
    {
        PlayerData data = SaveLoadManager.loadStats();

        CurrentEvolutionPoints = data.EvolutionPoints;
        Morale = data.Morale;
    }
}
