using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveNameUI : MonoBehaviour
{
    private string inputText;
    SaveLoadManager saveLoadManager;
    //PlayerStats playerStats;

    void Start()
    {
        saveLoadManager = GameObject.FindObjectOfType<SaveLoadManager>();
        //playerStats = GameObject.FindObjectOfType<PlayerStats>();
    }


    public void GettingSaveName (string input)
    {
        inputText = input;
        Debug.Log(inputText);
    }

    /*
    public void PassNametoSaveLoadManager ()
    {
        saveLoadManager.Save(inputText);
        playerStats.SetSaveName(inputText);
    }
    */
}
