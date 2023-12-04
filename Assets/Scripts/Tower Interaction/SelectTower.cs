using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectTower : MonoBehaviour
{
    public GameObject towerCanvasPrefab;

    private GameObject selectedTower;
    private GameObject towerCanvasInstance;
    private SelectTower SelecttTwerInstance;
    private bool towerSelected = false;
    public string TowerID;

    [SerializeField]
    private TowerManager towerManager;

    [SerializeField]
    private TowerPlacement towerPlacement;

    [SerializeField]
    private PauseMenu pauseMenu;

    public Material highlightMaterial;
    private Material originalTowerMaterial;


    private void Awake()
    {
        // Ensure only one instance exists
        if (SelecttTwerInstance == null)
        {
            SelecttTwerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        towerManager = GameObject.FindObjectOfType<TowerManager>();
        towerPlacement = FindObjectOfType<TowerPlacement>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void OnMouseDown()
    {
        if (towerPlacement.IsPlacingTower || pauseMenu.GameIsPaused)
        {
            return;
        }


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

            HighlightSelectedTower(selectedTower);

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
            ResetSelectedTowerColor();
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

    private void ResetSelectedTowerColor ()
    {
        if (selectedTower != null)
        {
            Renderer[] childRenderers = selectedTower.GetComponentsInChildren<Renderer>();
            foreach (Renderer childRenderer in childRenderers)
            {
                // Reset the color of the previously selected tower for each child
                childRenderer.material = originalTowerMaterial;
            }
        }
    }

    private void HighlightSelectedTower(GameObject tower)
    {
        Renderer[] childRenderers = tower.GetComponentsInChildren<Renderer>();
        foreach (Renderer childRenderer in childRenderers)
        {
            // Store the original material to reset it later
            originalTowerMaterial = childRenderer.material;

            // Apply the highlight material
            childRenderer.material = highlightMaterial;
        }
    }
}