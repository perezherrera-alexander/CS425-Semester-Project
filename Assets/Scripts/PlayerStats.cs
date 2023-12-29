using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, ISaveable
{
    // Singleton instance
    public static PlayerStats Instance;

    [SerializeField]
    public int Morale;
    [SerializeField]
    public int CurrentEvolutionPoints;

    private int enemiesKilled = 0;

    //private string CustomSaveName;

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            Morale = 100;
            CurrentEvolutionPoints = 20;
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

    public PlayerStats GetPlayerStats ()
    {
        return this;
    }


    public object CaptureState ()
    {
        return new SaveData
        {
            CurrentEvolutionPoints = CurrentEvolutionPoints,
            Morale = Morale
        };
    }

    public void RestoreState (object state)
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
