using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour, ISaveable
{
    public PlayerData playerData;
    // Singleton instance
    public static PlayerStatistics Instance { get; private set; }
    //public HealthBar moraleBar;
    public MoraleUI moraleBar;
    public int MaxMorale = 100;
    public int currentMorale = 100;
    public int CurrentEvolutionPoints = 20;
    private int enemiesKilled = 0;
    public int enemiesPresent = 0;
    //private string CustomSaveName;
    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null) // If instance doesn't exist, create one
        {
            currentMorale = playerData.Morale;
            CurrentEvolutionPoints = playerData.EvolutionPoints;
            Instance = this;
        }
        else // Otherwise destroy the existing instance
        {
            Destroy(gameObject);
        }
        moraleBar.SetMaxMorale(MaxMorale);
    }


    public void Update()
    {
        int currentMorale = GetMorale();
        moraleBar.SetMorale(currentMorale);
        int evolutionPoints = GetMoney();
        int enemiesKilled = GetEnemiesKilled();

        playerData.UpdateStats(currentMorale, evolutionPoints, enemiesKilled);

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
        currentMorale -= MoraleLost;

    }

    public int GetMorale ()
    {
        return currentMorale;
    }

    public int GetMaxMorale()
    {
        return MaxMorale;
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
            currentMorale = currentMorale,
            NumberOfWorldsCompleted = playerData.NumberOfWorldsCompleted,
            CurrentWorld = playerData.CurrentWorld,
            WorldsCompleted = playerData.WorldsCompleted,
            LocationOfTowerUnlock = playerData.LocationOfTowerUnlock,
            TowersObtained = playerData.TowersObtained,
            /*
            Towers = playerData.Towers,
            TowerUnlockOrder = playerData.TowerUnlockOrder,
            ActiveGenerals = playerData.activeGeneral,
            ActiveModifiers = playerData.activeModifier
            */
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        CurrentEvolutionPoints = saveData.CurrentEvolutionPoints;
        currentMorale = saveData.currentMorale;
        playerData.NumberOfWorldsCompleted = saveData.NumberOfWorldsCompleted;
        playerData.CurrentWorld = saveData.CurrentWorld;
        playerData.WorldsCompleted = saveData.WorldsCompleted;
        playerData.LocationOfTowerUnlock = saveData.LocationOfTowerUnlock;
        playerData.TowersObtained = saveData.TowersObtained;
        //playerData.Towers = saveData.Towers;
        //playerData.TowerUnlockOrder = saveData.TowerUnlockOrder;
        //playerData.activeGeneral = saveData.ActiveGenerals;
        //playerData.activeModifier = saveData.ActiveModifiers;
}

    [Serializable]
    private struct SaveData
    {
        public int CurrentEvolutionPoints;
        public int currentMorale;
        public int NumberOfWorldsCompleted;
        public string CurrentWorld;
        public string[] WorldsCompleted;
        public string[] LocationOfTowerUnlock;
        public int TowersObtained;
        /*
        public GameObject[] Towers;
        public GameObject[] TowerUnlockOrder;
        public Generals ActiveGenerals;
        public Modifiers ActiveModifiers;
        */
    }
}
