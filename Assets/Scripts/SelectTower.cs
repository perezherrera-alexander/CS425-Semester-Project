using UnityEngine;
using UnityEngine.UI;

public class SelectTower : MonoBehaviour
{
    public GameObject towerCanvasPrefab;
    public Text towerInfoText;

    private GameObject selectedTower;
    private GameObject towerCanvasInstance;
    private bool towerSelected = false;

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

            // Instantiate the tower canvas prefab
            towerCanvasInstance = Instantiate(towerCanvasPrefab) as GameObject;

            // Set the position of the canvas (optional: set it relative to the tower's position)
            towerCanvasInstance.transform.position = selectedTower.transform.position;

            // Attach UI elements or handle UI logic directly
            Text canvasText = towerCanvasInstance.GetComponentInChildren<Text>();
            if (canvasText != null)
            {
                // Set tower information in the UI text
                canvasText.text = "Tower Info: " + selectedTower.name;
            }

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
}
