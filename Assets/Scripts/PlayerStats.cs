using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int StartingMoney;
    public int CurrentMoney;

    private void Start()
    {
        CurrentMoney = StartingMoney;
    }

    public void AddMoney (int MoneyGained)
    {
        CurrentMoney += MoneyGained;
    }

    public int GetMoney ()
    {
        return CurrentMoney;
    }
}
