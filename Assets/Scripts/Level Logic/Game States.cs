using System;
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

[Flags] // Bitwise enum
public enum Modifiers
{
    None = 0,
    Money = 1,
    Morale = 2, // Increase health
    Range = 4,
    Damage = 8,
    Cooldown = 16,
    Developer = 32
}

public enum Generals
{
    Bee,
    Ant,
    Wasp,
    Beetle,
    Dev

}