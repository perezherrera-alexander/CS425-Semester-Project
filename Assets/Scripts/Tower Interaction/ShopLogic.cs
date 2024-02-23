using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopLogic : MonoBehaviour
{
    [Header("Tower Prefabs")]
    [SerializeField] private GameObject BeeTowerPrefab;
    [SerializeField] private GameObject MortarTowerPrefab;
    [SerializeField] private GameObject TetherTowerPrefab;
    [SerializeField] private GameObject FlameTowerPrefab;
    [SerializeField] private GameObject MeleeTowerPrefab;
    [SerializeField] private GameObject WaspTowerPrefab;
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
    private float shopUIWidth;
    private int numberOfTowersUnlocked;
    private float buttonXonClosedShop;
    private float buttonYonClosedShop;
    [Header("Scripts")]
    [SerializeField] private TowerPlacement towerPlacement; // Tower placing is handed off to the TowerPlacement script

    void Start()
    {
        // Grab references to the shop button and shop panel in the scene
        openShopButton = transform.GetChild(0).gameObject;
        shopPanel = transform.GetChild(1).gameObject;

        // Save the button's position to restore later when needed
        buttonXonClosedShop = openShopButton.transform.position.x;
        buttonYonClosedShop = openShopButton.transform.position.y;

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
        shopUIWidth = shopPanel.GetComponent<RectTransform>().rect.width;
        numberOfTowersUnlocked = playerData.TowersObtained;
        int numberOfRowsNeeded = Mathf.CeilToInt((float)numberOfTowersUnlocked / 2);
        int towersLeftToSpawn = numberOfTowersUnlocked;
        GameObject shopCanvasChild = shopPanel.transform.GetChild(0).gameObject;

        // Spawn the needed rows and popluate them with buttons
        for (int i = 0; i < numberOfRowsNeeded; i++)
        {
            //Debug.Log("Spawning a new row");
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

                    int towerCost = towerChildWithScript.GetComponent<BaseTowerLogic>().buildCost;
                    // Will update in the future to display tower stats such as range, cost, damage, etc. But for now towerCost goes unused.

                    // Create the button, attach it to the row and initialize it
                    GameObject newButton = Instantiate(shopButtonTemplate, newRow.transform);
                    newButton.transform.SetParent(newRow.transform);
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
        openShopButton.transform.position = new Vector3(openShopButton.transform.position.x - shopUIWidth, openShopButton.transform.position.y, openShopButton.transform.position.z);
        shopPanel.SetActive(true);
        shopIsOpen = true;
    }

    public void ShopHidden () // Move it back when closing the shop
    {
        openShopButton.transform.position = new Vector3(openShopButton.transform.position.x + shopUIWidth, openShopButton.transform.position.y, openShopButton.transform.position.z);
        shopPanel.SetActive(false);
        shopIsOpen = false;
    }
    
    // Purchase functions written by Edward Martinez
    public void PurchaseTower(string towerName)
    {
        //Debug.Log("Shop script called from button click with tower name: " + towerName);
        if (towerName == "Bee Tower")
        {
            PurchaseBeeTower();
        }
        else if (towerName == "Mortar Tower")
        {
            PurchaseMortarTower();
        }
        else if (towerName == "Tether Tower")
        {
            PurchaseTetherTower();
        }
        else if (towerName == "Fire Ant")
        {
            PurchaseFlameTower();
        }
        else if (towerName == "Army Ant")
        {
            PurchaseMeleeTower();
        }
        else if (towerName == "Wasp Tower")
        {
            PurchaseWaspTower();
        }
        else {
            Debug.Log("Tower: " + towerName + " not found or not yet implemented. Defaulting to Bee Tower.");
            PurchaseBeeTower();
        }
    }
    public void PurchaseBeeTower()
    {
        Transform child = BeeTowerPrefab.transform.Find("Rotate");

        // Check if you have enough money to purchase the Bee Tower
        int towerCost = child.GetComponent<BeeTower>().buildCost;

        if (playerStatistics.GetMoney() >= towerCost)
        {
            // Deduct the money and call the PlaceTower function
            playerStatistics.AddMoney(-towerCost);
            towerPlacement.PlaceTower(BeeTowerPrefab, "Bee Tower"); // Pass along the prefab's name so we know which script to call later in TowerPlacement.cs
        }
        else
        {
            Debug.Log("Not enough money to purchase the Bee Tower.");
        }
    }
    public void PurchaseMortarTower()
    {
        Transform child = MortarTowerPrefab.transform.Find("Rotate");

        // Check if you have enough money to purchase the Bee Tower
        int towerCost = child.GetComponent<mortarTower>().buildCost;

        if (playerStatistics.GetMoney() >= towerCost)
        {
            // Deduct the money and call the PlaceTower function
            playerStatistics.AddMoney(-towerCost);
            towerPlacement.PlaceTower(MortarTowerPrefab, "Morar Tower");
        }
        else
        {
            Debug.Log("Not enough money to purchase the Mortar Tower.");
        }
    }
    public void PurchaseTetherTower()
    {
        Transform child = TetherTowerPrefab.transform.Find("Rotate");

        // Check if you have enough money to purchase the Bee Tower
        int towerCost = child.GetComponent<tetherTower>().buildCost;

        if (playerStatistics.GetMoney() >= towerCost)
        {
            // Deduct the money and call the PlaceTower function
            playerStatistics.AddMoney(-towerCost);
            towerPlacement.PlaceTower(TetherTowerPrefab, "Tether Tower");
        }
        else
        {
            Debug.Log("Not enough money to purchase the Tether Tower.");
        }
    }
    public void PurchaseFlameTower()
    {
        Transform child = FlameTowerPrefab.transform.Find("Rotate");

        // Check if you have enough money to purchase the Bee Tower
        int towerCost = child.GetComponent<FlameTower>().buildCost;

        if (playerStatistics.GetMoney() >= towerCost)
        {
            // Deduct the money and call the PlaceTower function
            playerStatistics.AddMoney(-towerCost);
            towerPlacement.PlaceTower(FlameTowerPrefab, "Flame Tower");
        }
        else
        {
            Debug.Log("Not enough money to purchase the Flame Tower.");
        }
    }
    public void PurchaseMeleeTower()
    {
        Transform child = MeleeTowerPrefab.transform.Find("Rotate");

        // Check if you have enough money to purchase the Bee Tower
        int towerCost = child.GetComponent<meleeTower>().buildCost;

        if (playerStatistics.GetMoney() >= towerCost)
        {
            // Deduct the money and call the PlaceTower function
            playerStatistics.AddMoney(-towerCost);
            towerPlacement.PlaceTower(MeleeTowerPrefab, "Melee Tower");
        }
        else
        {
            Debug.Log("Not enough money to purchase the Melee Tower.");
        }
    }
    public void PurchaseWaspTower()
    {
        Transform child = WaspTowerPrefab.transform.Find("Rotate");

        // Check if you have enough money to purchase the Bee Tower
        int towerCost = child.GetComponent<StraightShooter>().buildCost;

        if (playerStatistics.GetMoney() >= towerCost)
        {
            // Deduct the money and call the PlaceTower function
            playerStatistics.AddMoney(-towerCost);
            towerPlacement.PlaceTower(WaspTowerPrefab, "Wasp Tower");
        }
        else
        {
            Debug.Log("Not enough money to purchase the Wasp Tower.");
        }
    }

}
