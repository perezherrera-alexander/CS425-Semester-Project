using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossConditionCheck : MonoBehaviour
{
    PlayerStats playerStats;

    int CurrentMorale;

    public GameObject GameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        CurrentMorale = playerStats.GetMorale();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentMorale = playerStats.GetMorale();

        if (CurrentMorale <= 0)
        {
            GameOverScreen.SetActive(true);
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMainMenu()
    {
        Debug.Log("Quitting to Main Menu......");
        SceneManager.LoadScene("Main Menu");
    }
}
