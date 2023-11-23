using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int EvolutionPoints;
    public int Morale;

    public PlayerData (PlayerStats stats)
    {
        EvolutionPoints = stats.CurrentEvolutionPoints;
        Morale = stats.Morale;
    }
}
