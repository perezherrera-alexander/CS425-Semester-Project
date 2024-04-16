using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapLoadSave : MonoBehaviour, ISaveable
{
    public PlayerData playerData;
    public GameObjectConversion GameObjectConversion;
    public object CaptureState()
    {
        return new SaveData
        {
            CurrentEvolutionPoints = playerData.EvolutionPoints,
            currentMorale = playerData.Morale,
            NumberOfWorldsCompleted = playerData.NumberOfWorldsCompleted,
            CurrentWorld = playerData.CurrentWorld,
            WorldsCompleted = playerData.WorldsCompleted,
            LocationOfTowerUnlock = playerData.LocationOfTowerUnlock,
            TowersObtained = playerData.TowersObtained,
            Towers = GameObjectConversion.SaveTowerObtained(playerData.Towers, playerData.TowersObtained),
            TowerUnlockOrder = GameObjectConversion.SaveTowerUnlockOrder(playerData.TowerUnlockOrder),
            ActiveGenerals = playerData.activeGeneral,
            ActiveModifiers = playerData.activeModifier

        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        Debug.Log(playerData.EvolutionPoints);
        playerData.EvolutionPoints = saveData.CurrentEvolutionPoints;
        playerData.Morale = saveData.currentMorale;
        playerData.NumberOfWorldsCompleted = saveData.NumberOfWorldsCompleted;
        playerData.CurrentWorld = saveData.CurrentWorld;
        playerData.WorldsCompleted = saveData.WorldsCompleted;
        playerData.LocationOfTowerUnlock = saveData.LocationOfTowerUnlock;
        playerData.TowersObtained = saveData.TowersObtained;
        playerData.Towers = GameObjectConversion.LoadTowerObtained(saveData.Towers);
        playerData.TowerUnlockOrder = GameObjectConversion.LoadTowerUnlockOrder(saveData.TowerUnlockOrder);
        playerData.activeGeneral = saveData.ActiveGenerals;
        playerData.activeModifier = saveData.ActiveModifiers;
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
        public string[] Towers;
        public string[] TowerUnlockOrder;
        public Generals ActiveGenerals;
        public Modifiers ActiveModifiers;

    }
}
