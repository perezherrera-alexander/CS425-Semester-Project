using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
//using Codice.CM.Common.Tree;
using System;

public class ShopLogic : MonoBehaviour
{
    [Header("Tower Prefabs")]
    [SerializeField] private GameObject BeeTowerPrefab;
    [SerializeField] private GameObject MortarTowerPrefab;
    [SerializeField] private GameObject TetherTowerPrefab;
    [SerializeField] private GameObject FlameTowerPrefab;
    [SerializeField] private GameObject MeleeTowerPrefab;
    [SerializeField] private GameObject MortarAntPrefab;
    [SerializeField] private GameObject AttackBeePrefab;
    [SerializeField] private GameObject BeeSwarmPrefab;
    [SerializeField] private GameObject BuffingBeePrefab;
    [SerializeField] private GameObject WaspTowerPrefab;
    [SerializeField] private GameObject WaspMeleePrefab;
    [SerializeField] private GameObject BeetleTowerPrefab;
    [SerializeField] private GameObject GrassHopperPrefab;
    [SerializeField] private GameObject MantisTowerPrefab;
    [SerializeField] private GameObject MothTowerPrefab;
    [SerializeField] private GameObject SpiderTowerPrefab;
    [SerializeField] private GameObject StagBeetlePrefab;
    [SerializeField] private GameObject CentipedePrefab;

    [Header("Player Information")]
    [SerializeField] private PlayerStatistics playerStatistics; // Used to check if the player has enogh money when purchasing
    [SerializeField] private PlayerData playerData; // Used to get the list of towers the player has unlocked
    [Header("Shop UI")]
    public bool shopIsOpen = false;
    // Both of these gameobjects are found at runtime, but they should be children of the current gameobject
    private GameObject shopPanel; // The shop UI panel
    private GameObject openShopButton; // The button that opens the shop UI
    [SerializeField] private GameObject shopRowTemplate;
    [SerializeField] private GameObject shopButtonTemplate;
    private int numberOfTowersUnlocked;
    public GameObject towerDescriptionPanelPrefab;
    [Header("Scripts")]
    [SerializeField] private TowerPlacement towerPlacement; // Tower placing is handed off to the TowerPlacement script
    private TargetingTypes savedTargettingType;
    [SerializeField] TowerSaveLoadManager towerSaveLoadManager;
    void Start()
    {
        // Grab references to the shop button and shop panel in the scene
        openShopButton = transform.GetChild(0).gameObject;
        shopPanel = transform.GetChild(1).gameObject;

        InitializeShopUI(); // Creates rows and populates them with buttons
    }

    void Update()
    {
        // If an upgrade panel opens, close the shop UI
        if (GameObject.FindWithTag("TowerPanel") != null)
        {
            if(shopIsOpen)
            {
                ToggleShopUI();
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.B))
            {
                ToggleShopUI();
            }
        }
    }

    void InitializeShopUI(){
        // Initialize variables to create the shop UI
        numberOfTowersUnlocked = playerData.TowersObtained;
        int numberOfRowsNeeded = Mathf.CeilToInt((float)numberOfTowersUnlocked / 2);
        int towersLeftToSpawn = numberOfTowersUnlocked;
        GameObject shopCanvasChild = shopPanel.transform.GetChild(0).gameObject;

        // Spawn the needed rows and popluate them with buttons
        for (int i = 0; i < numberOfRowsNeeded; i++)
        {
            GameObject newRow = Instantiate(shopRowTemplate, shopPanel.transform);
            // Attach the empty row to the shop UI child
            newRow.transform.SetParent(shopCanvasChild.transform);
            for(int j = 0; j < 2; j++)
            {
                // Button Creation
                if (towersLeftToSpawn > 0)
                {
                    GameObject towerReference = playerData.Towers[numberOfTowersUnlocked - towersLeftToSpawn];
                    Transform towerChildWithScript = towerReference.transform.Find("Rotate");

                    string towerName = towerReference.transform.GetComponentInChildren<BaseTowerLogic>().towerName;
                    int towerCost = towerChildWithScript.GetComponentInChildren<BaseTowerLogic>().buildCost;
                    string towerDescription = towerChildWithScript.GetComponentInChildren<BaseTowerLogic>().towerDescription;

                    //Create a tower description panel for each button
                    GameObject newTowerDescriptionPanel = Instantiate(towerDescriptionPanelPrefab, shopPanel.transform);
                    newTowerDescriptionPanel.name = towerName + " Description Panel";
                    newTowerDescriptionPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = towerName;
                    newTowerDescriptionPanel.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = towerCost.ToString();
                    newTowerDescriptionPanel.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = towerDescription;
                    newTowerDescriptionPanel.transform.GetChild(0).gameObject.SetActive(false);

                    // Create the button, attach it to the row and initialize it
                    GameObject newButton = Instantiate(shopButtonTemplate, newRow.transform);
                    newButton.transform.SetParent(newRow.transform);
                    newButton.name = towerName + " Button";
                    // Set the button's image to the tower's image
                    newButton.GetComponent<RawImage>().color = new Color(1, 1, 1, 1); // Button's default color is #717171 (for debugging) but is set to white at run time as otherwise the image is tinted.
                    newButton.GetComponent<RawImage>().texture = towerReference.transform.GetComponentInChildren<BaseTowerLogic>().towerImage;
                    newButton.GetComponent<Button>().onClick.AddListener(delegate {PurchaseTower(towerName); });



                    towersLeftToSpawn--;
                }
            }
        }
    }

    // ToggleShopUI, ShopVisible, and ShopHidden were written by Edward Martinez
    public void ToggleShopUI()
    {
        if (shopIsOpen)
        {
            ShopHidden();
        }
        else
        {
            ShopVisible();
        }
    }

    public void ShopVisible () // Move the shop open button to the left to make way for the shop panel
    {
        //openShopButton.transform.position = new Vector3(openShopButton.transform.position.x - 400, openShopButton.transform.position.y, openShopButton.transform.position.z);
        openShopButton.SetActive(false);
        shopPanel.SetActive(true);
        shopIsOpen = true;
    }

    public void ShopHidden () // Move it back when closing the shop
    {
        //openShopButton.transform.position = new Vector3(openShopButton.transform.position.x + 400, openShopButton.transform.position.y, openShopButton.transform.position.z);
        openShopButton.SetActive(true);
        shopPanel.SetActive(false);
        shopIsOpen = false;
    }
    
    // Purchase functions written by Edward Martinez
    public void PurchaseTower(string towerName)
    {
        if (towerName == "Bee Tower") PurchaseBeeTower(towerName);
        else if (towerName == "Mortar Tower") PurchaseMortarTower(towerName);
        else if (towerName == "Tether Tower") PurchaseTetherTower(towerName);
        else if (towerName == "Fire Ant") PurchaseFlameTower(towerName);
        else if (towerName == "Army Ant") PurchaseMeleeTower(towerName);
        else if (towerName == "Mortar Ant") PurchaseMortarAntTower(towerName);
        else if (towerName == "Attack Bee") PurcahseAttackBeeTower(towerName);
        else if (towerName == "Bee Swarm") PurchaseBeeSwarmTower(towerName);
        else if (towerName == "Buffing Bee") PurchaseBuffingBeeTower(towerName);
        else if (towerName == "Wasp Tower") PurchaseWaspTower(towerName);
        else if (towerName == "Wasp Melee") PurchaseWaspMeleeTower(towerName);
        else if (towerName == "Beetle Tower") PurchaseBeetleTower(towerName);
        else if (towerName == "Grasshopper Lair") PurchaseGrassHopperTower(towerName);
        else if (towerName == "Mantis Tower") PurchaseMantisTower(towerName);
        else if (towerName == "Moth Tower") PurchaseMothTower(towerName);
        else if (towerName == "Spider") PurchaseSpiderTower(towerName);
        else if (towerName == "Stag Beetle") PurchaseStagBeetleTower(towerName);
        else if (towerName == "Centipede Tower") PurchaseCentipedeTower(towerName);
        else {
            Debug.Log("Tower: " + towerName + " not found or not yet implemented. Defaulting to Bee Tower.");
            PurchaseBeeTower("Bee Tower");
        }
    }
    public void PurchaseBeeTower(string towerName)
    {
        PurchaseLogic(towerName, BeeTowerPrefab);
    }
    public void PurchaseMortarTower(string towerName)
    {
        PurchaseLogic(towerName, MortarTowerPrefab);
    }
    public void PurchaseTetherTower(string towerName)
    {
        PurchaseLogic(towerName, TetherTowerPrefab);
    }
    public void PurchaseFlameTower(string towerName)
    {
        PurchaseLogic(towerName, FlameTowerPrefab);
    }
    public void PurchaseMeleeTower(string towerName)
    {
        PurchaseLogic(towerName, MeleeTowerPrefab);
    }

    public void PurchaseMortarAntTower(string towerName)
    {
        PurchaseLogic(towerName, MortarAntPrefab);
    }

    public void PurcahseAttackBeeTower(string towerName)
    {
        PurchaseLogic(towerName, AttackBeePrefab);
    }

    public void PurchaseBeeSwarmTower(string towerName)
    {
        PurchaseLogic(towerName, BeeSwarmPrefab);
    }

    public void PurchaseBuffingBeeTower(string towerName)
    {
        PurchaseLogic(towerName, BuffingBeePrefab);
    }

    public void PurchaseWaspMeleeTower(string towerName)
    {
        PurchaseLogic(towerName, WaspMeleePrefab);
    }

    public void PurchaseWaspTower(string towerName)
    {
        PurchaseLogic(towerName, WaspTowerPrefab);
    }

    public void PurchaseBeetleTower(string towerName)
    {
        PurchaseLogic(towerName, BeetleTowerPrefab);
    }

    public void PurchaseGrassHopperTower(string towerName)
    {
        PurchaseLogic(towerName, GrassHopperPrefab);
    }

    public void PurchaseMantisTower(string towerName)
    {
        PurchaseLogic(towerName, MantisTowerPrefab);
    }

    public void PurchaseMothTower(string towerName)
    {
        PurchaseLogic(towerName, MothTowerPrefab);
    }

    public void PurchaseSpiderTower(string towerName)
    {
        PurchaseLogic(towerName, SpiderTowerPrefab);
    }

    public void PurchaseStagBeetleTower(string towerName)
    {
        PurchaseLogic(towerName, StagBeetlePrefab);
    }

    public void PurchaseCentipedeTower(string towerName)
    {
        PurchaseLogic(towerName, CentipedePrefab);
    }

    private void PurchaseLogic(string towerName, GameObject towerPrefab)
    {
        Transform child = towerPrefab.transform.Find("Rotate");

        // Check if you have enough money to purchase the tower
        int towerCost = child.GetComponent<BaseTowerLogic>().buildCost;

        if (MoneyCheck(towerCost)) // Only true if you have enough money (deducts money) or you are in developer mode
        {
            towerPlacement.PlaceTower(towerPrefab);
            ToggleShopUI();
        }
        else
        {
            Debug.Log("Not enough money to purchase the " + towerName + ".");
        }
    }

    private bool MoneyCheck(int towerCost)
    {
        if(playerData.activeModifier == Modifiers.Developer)
        {
            return true;
        }
        else if (playerStatistics.GetMoney() >= towerCost)
        {
            playerStatistics.AddMoney(-towerCost);
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void ReverseMoneyCheck(int towerCost)
    {
        if(playerData.activeModifier != Modifiers.Developer) {
            playerStatistics.AddMoney(towerCost);
        }
        else {
            return; // Do nothing if in developer mode
        }
    }

}
