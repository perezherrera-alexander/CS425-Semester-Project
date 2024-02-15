using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    public bool ShopStatus = false;
    public GameObject ShopUI; // The game object that holds the shop UI (also hold the Shop script)
    [SerializeField] private GameObject ShopButton;
    [SerializeField] private GameObject shopRow;
    [SerializeField] private GameObject buttonTemplate;
    [SerializeField] private PlayerData playerData; 
    private float ShopUIWidth;
    private int numberOfTowersUnlocked = 0;
    

    public void Start()
    {
        ShopUIWidth = ShopUI.GetComponent<RectTransform>().rect.width;
        numberOfTowersUnlocked = playerData.Towers.Length;
        int numberOfRowsNeeded = Mathf.CeilToInt((float)numberOfTowersUnlocked / 2);
        int towersLeftToSpawn = numberOfTowersUnlocked;
        GameObject shopCanvasChild = ShopUI.transform.GetChild(0).gameObject;

        //Debug.Log("Number of towers unlocked: " + numberOfTowersUnlocked);
        //Debug.Log("Number of rows needed: " + numberOfRowsNeeded);

        // Spawn the appropriate number of rows
        for (int i = 0; i < numberOfRowsNeeded; i++)
        {
            //Debug.Log("Spawning a new row");
            GameObject newRow = Instantiate(shopRow, ShopUI.transform);
            // Attach the empty row to the shop UI child
            newRow.transform.SetParent(shopCanvasChild.transform);
            for(int j = 0; j < 2; j++)
            {
                //Debug.Log("Spawning a new button");
                //Debug.Log("Towers left to spawn: " + towersLeftToSpawn);
                // Spawn the correct tower button in the row
                if (towersLeftToSpawn > 0)
                {
                    //Debug.Log("tower reference: " + playerData.Towers[numberOfTowersUnlocked - towersLeftToSpawn]);
                    GameObject towerReference = playerData.Towers[numberOfTowersUnlocked - towersLeftToSpawn];
                    Transform child = towerReference.transform.Find("Rotate");
                    string towerName = towerReference.transform.GetComponentInChildren<BaseTowerLogic>().towerName;
                    //string towerName = child.GetComponent<BaseTowerLogic>().name;
                    int towerCost = child.GetComponent<BaseTowerLogic>().buildCost;
                    Debug.Log("Tower name: " + towerName);
                    Debug.Log("Tower cost: " + towerCost);
                    //string towerName = towerReference.GetComponentInChildren<BaseTowerLogic>().towerName;

                    // Create the button
                    GameObject newButton = Instantiate(buttonTemplate, newRow.transform);
                    // Attach the button to the row
                    newButton.transform.SetParent(newRow.transform);
                    // Set the button's image to the tower's image
                    //newButton.GetComponent<Image>().sprite = towerReference.GetComponentInChildren<SpriteRenderer>().sprite;
                    // Set the button's onClick event to the correct tower's purchase function
                    newButton.GetComponent<Button>().onClick.AddListener(delegate {ShopUI.GetComponent<ShopScript>().PurchaseTower(towerName); });
                    // Decrement the number of towers left to spawn
                    towersLeftToSpawn--;
                }
                else
                {
                    // Do nothing
                }
            }
        }
    }


    public void ToggleShopUI ()
    {
        if (ShopStatus)
        {
            ShopHidden();
        }
        else
        {
            ShopVisible();
        }
    }
    public void ShopVisible ()
    {
        // Move the shop open button to the left
        ShopButton.transform.position = new Vector3(ShopButton.transform.position.x - ShopUIWidth, ShopButton.transform.position.y, ShopButton.transform.position.z);
        ShopUI.SetActive(true);
        ShopStatus = true;
    }

    public void ShopHidden ()
    {
        // Move it back when closing the shop
        ShopButton.transform.position = new Vector3(ShopButton.transform.position.x + ShopUIWidth, ShopButton.transform.position.y, ShopButton.transform.position.z);
        ShopUI.SetActive(false);
        ShopStatus = false;
    }
}
