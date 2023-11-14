using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectTower : MonoBehaviour
{
    public GameObject towerCanvasPrefab;

    private GameObject selectedTower;
    private GameObject towerCanvasInstance;
    private bool towerSelected = false;

    //public TextMeshProUGUI towernameUI;
    private string towerName = "ttt";

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked object is a tower and not placed through the shop
                if (hit.collider.CompareTag("Bee") || hit.collider.CompareTag("Mortar") || hit.collider.CompareTag("Tether"))
                {
                    ToggleTowerCanvas(hit.collider.gameObject);
                }
            }
        }
    }

    private void ToggleTowerCanvas(GameObject tower)
    {

        if (towerCanvasInstance == null)
        {
            selectedTower = tower;
            towerSelected = true;

            basicTowerScript towerScript = selectedTower.GetComponentInChildren<basicTowerScript>();

            // Instantiate the tower canvas prefab
            towerCanvasInstance = Instantiate(towerCanvasPrefab) as GameObject;

            // Set the position of the canvas (optional: set it relative to the tower's position)
            towerCanvasInstance.transform.position = selectedTower.transform.position;


            if (towerScript != null)
            {
                Type scriptType = towerScript.GetType();

                string scriptName = scriptType.Name;

                Debug.Log(scriptName);
            }

            
            TMP_Text output = towerCanvasInstance.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            output.text = "Tower Name: " + GetOriginalPrefabName(selectedTower);



            Debug.Log (towerName);
            
            

            // Example: Add a button for tower deletion
            Button deleteButton = towerCanvasInstance.GetComponentInChildren<Button>();
            if (deleteButton != null)
            {
                Debug.Log("ENTERED");
                deleteButton.onClick.AddListener(DeleteSelectedTower);
            }

        }
        else
        {
            Destroy(towerCanvasInstance);
        }
    }

    public void DeleteSelectedTower()
    {
        // Check if a tower is selected through player interaction before trying to delete
        if (towerSelected && selectedTower != null)
        {

            basicTowerScript towerScript = selectedTower.GetComponentInChildren<basicTowerScript>();

            if (towerScript != null)
            {
                // Access the BuildCost directly
                int towerCost = towerScript.BuildCost;

                // Access PlayerStats
                PlayerStats playerStats = PlayerStats.Instance;

                if (playerStats != null)
                {
                    // Access the addMoney function or any other function in PlayerStats
                    playerStats.AddMoney(towerCost);
                }

                // ... rest of the code ...
            }

            Debug.Log("Deleting tower through player interaction.");
            Destroy(selectedTower);
            towerSelected = false;
            selectedTower = null;

            // Destroy the canvas after a short delay
            Destroy(towerCanvasInstance, 0.1f); // Adjust the delay as needed
        }
    }

    private string GetOriginalPrefabName(GameObject tower)
    {
        if (tower != null)
        {
            // Check if the tower has a "(Clone)" suffix
            if (tower.name.EndsWith("(Clone)"))
            {
                // Remove the "(Clone)" suffix and return the original name
                return tower.name.Replace("(Clone)", "").Trim();
            }
        }

        // Return the current tower name if it doesn't have a "(Clone)" suffix
        return tower.name;
    }
}
