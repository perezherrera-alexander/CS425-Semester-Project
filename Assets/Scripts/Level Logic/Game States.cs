using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    InbetweenWaves,
    WaveStarting,
    WaveInProgress,
    LevelComplete
}

public enum Modifiers
{
    None,
    Money,
    Morale, // Increase health
    Range,
    Damage,
    Cooldown
}

public enum Generals
{
    Bee,
    Ant,
    Wasp,
    Beetle,
    Dev

}