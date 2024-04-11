using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LossConditionCheck : MonoBehaviour
{
    PlayerStatistics playerStatistics;

    int CurrentMorale;

    public GameObject GameOverScreen;

    public StoreTowerUpgradeData storeTowerUpgradeData;

    // Start is called before the first frame update
    void Start()
    {
        playerStatistics = FindObjectOfType<PlayerStatistics>();
        CurrentMorale = playerStatistics.GetMorale();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentMorale = playerStatistics.GetMorale();

        if (CurrentMorale <= 0)
        {
            GameOverScreen.SetActive(true);
        }
    }

    public void EnhanceYourCritters()
    {
        storeTowerUpgradeData.TokensObtained = 1;
        //SceneManager.LoadScene("Tower Upgrade");
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Quitting to Main Menu......");
        SceneManager.LoadScene("Main Menu");
    }
}
