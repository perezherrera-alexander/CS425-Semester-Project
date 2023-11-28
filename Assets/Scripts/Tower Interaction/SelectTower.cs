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
    public string TowerID;

    [SerializeField]
    TowerManager towerManager;

    // Start is called before the first frame update
    void Start()
    {
        towerManager = GameObject.FindObjectOfType<TowerManager>();
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit collider belongs to the TowerLayer
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Tower"))
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

                TMP_Text output = towerCanvasInstance.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
                output.text = "Tower Name: " + scriptName;

            }

            // Add a button for tower deletion
            Button deleteButton = towerCanvasInstance.GetComponentInChildren<Button>();
            if (deleteButton != null)
            {
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
            }



            if (towerScript != null)
            {
                Type scriptType = towerScript.GetType();

                string scriptName = scriptType.Name;

                Debug.Log(scriptName);

                if (scriptName == "beeTower")
                {
                    TowerID = selectedTower.transform.GetChild(1).gameObject.GetComponent<beeTower>().id;
                    Debug.Log(TowerID);
                    towerManager.RemoveTower(TowerID);
                }

                if (scriptName == "mortarTower")
                {
                    TowerID = selectedTower.transform.GetChild(1).gameObject.GetComponent<mortarTower>().id;
                    Debug.Log(TowerID);
                    towerManager.RemoveTower(TowerID);
                }

                if (scriptName == "tetherTower")
                {
                    TowerID = selectedTower.transform.GetChild(1).gameObject.GetComponent<tetherTower>().id;
                    Debug.Log(TowerID);
                    towerManager.RemoveTower(TowerID);
                }

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