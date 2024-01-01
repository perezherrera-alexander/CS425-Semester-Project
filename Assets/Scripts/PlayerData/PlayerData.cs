using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats")]
public class PlayerData : ScriptableObject
{
    public int Morale = 100;
    public int MoraleDuringWorld = 0;

    public int EvolutionPoints = 20;
    public int EvolutionPointsDuringWorld = 0;

    public int EnemiesKilled = 0;
    public int EnemiesKilledDuringWorld = 0;

    public int NumberOfWorldsCompleted = 0;
    public string CurrentWorld = "";
    public string[] WorldsCompleted;

    public bool LevelCompleted = false;

    public int TowersObtained = 3;
    public GameObject[] Towers;

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

            LevelCompleted = true;
        }
    }

    public void InitializeWorldsCompletedArray(int arraySize)
    {
        WorldsCompleted = new string[arraySize];
    }
}
