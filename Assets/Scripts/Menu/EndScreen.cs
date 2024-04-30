using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    // Start is called before the first frame update
    void Start()
    {
        storeTowerUpgradeData.TokensObtained = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
