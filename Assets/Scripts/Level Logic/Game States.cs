using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    InbetweenWaves,
    WaveStarting,
    WaveInProgress,
    LevelComplete,
    GameOver
}

[Flags] // Bitwise enum (https://www.c-sharpcorner.com/article/understanding-bitwise-enums-in-c-sharp/)
public enum Modifiers
{
    None = 0,
    Money = 1, // Infinite Money
    Morale = 2, // Used as a Hardcore mode (1 health)
    Range = 4,
    Damage = 8,
    Cooldown = 16,
    Developer = 32,
    HalfMorale = 64,
    DoubleMorale = 128,
    HalfMoney = 256,
    DoubleMoney = 512
}

public enum Generals
{
    Bee,
    Ant,
    Wasp,
    Beetle,
    Dev

}