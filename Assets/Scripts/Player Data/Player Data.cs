using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats")]
public class PlayerData : ScriptableObject
{
    public int Morale;
    public int MoraleDuringWorld = 0;
    public int EvolutionPoints = 20;
    public int EvolutionPointsDuringWorld = 0;

    public int EnemiesKilled = 0;

    //public int enemiesPresent = 0;
    public int EnemiesKilledDuringWorld = 0;

    public int NumberOfWorldsCompleted = 0;
    public string CurrentWorld = "";
    public string[] WorldsCompleted;

    public string[] LocationOfTowerUnlock;

    public int TowersObtained = 3;
    public GameObject[] Towers;
    public GameObject[] TowerPool;
    public GameObject[] TowerUnlockOrder;
    public Generals activeGeneral = Generals.Bee;
    public Modifiers activeModifier = Modifiers.None;

    public void UpdateStats(int morale, int evolutionPoints, int enemiesKilled)
    {
        MoraleDuringWorld = morale;
        EvolutionPointsDuringWorld = evolutionPoints;
        EnemiesKilled = enemiesKilled;
    }

    public void UpdateData(bool completed)
    {
        if (completed)
        {
            Morale = MoraleDuringWorld;
            EvolutionPoints = EvolutionPointsDuringWorld;
            EnemiesKilled = EnemiesKilledDuringWorld;
        }
    }

    public void InitializeWorldsCompletedArray(int arraySize)
    {
        WorldsCompleted = new string[arraySize];
    }
    public void InitializeTowerUnlockArray(int arraySize)
    {
        LocationOfTowerUnlock = new string[arraySize];
    }
    public void InitializeTowersArray(int arraySize)
    {
        Towers = new GameObject[arraySize];
    }
    public void InitializeTowerPoolArray(int arraySize)
    {
        TowerPool = new GameObject[arraySize];
    }
    public void InitializeTowerUnlockOrderArray(int arraySize)
    {
        TowerUnlockOrder = new GameObject[arraySize];
    }
}
