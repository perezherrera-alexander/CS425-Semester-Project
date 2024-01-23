using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour, ISaveable
{
    public PlayerData playerData;
    // Singleton instance
    public static PlayerStatistics Instance { get; private set; }
    public int Morale = 100;
    public int CurrentEvolutionPoints = 20;
    private int enemiesKilled = 0;
    //private string CustomSaveName;
    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null) // If instance doesn't exist, create one
        {
            Morale = playerData.Morale;
            CurrentEvolutionPoints = playerData.EvolutionPoints;
            Instance = this;
        }
        else // Otherwise destroy the existing instance
        {
            Destroy(gameObject);
        }
    }


    public void Update()
    {
        int morale = GetMorale();
        int evolutionPoints = GetMoney();
        int enemiesKilled = GetEnemiesKilled();

        playerData.UpdateStats(morale, evolutionPoints, enemiesKilled);

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

    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }

    public void ResetEnemiesKilled()
    {
        enemiesKilled = 0;
    }

    public void AddEnemiesKilled()
    {
        enemiesKilled++;
    }

    public PlayerStatistics GetPlayerStats()
    {
        return this;
    }


    public object CaptureState()
    {
        return new SaveData
        {
            CurrentEvolutionPoints = CurrentEvolutionPoints,
            Morale = Morale
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        CurrentEvolutionPoints = saveData.CurrentEvolutionPoints;
        Morale = saveData.Morale;
    }

    [Serializable]
    private struct SaveData
    {
        public int CurrentEvolutionPoints;
        public int Morale;
    }
}
