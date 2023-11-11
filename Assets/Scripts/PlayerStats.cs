using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int StartingMoney;
    public int CurrentMoney;
    // Singleton instance
    public static PlayerStats Instance;


    private void Start()
    {
        CurrentMoney = StartingMoney;
    }

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
