using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class SelectTower : MonoBehaviour
{
    public GameObject towerCanvasPrefab;
    public Material highlightMaterial;

    private GameObject selectedTower;
    private GameObject towerCanvasInstance;
    private SelectTower SelecttTwerInstance;
    private bool towerSelected = false;
    public string TowerID;

    [SerializeField]
    private TowerSaveLoadManager towerSaveLoadManager;

    [SerializeField]
    private TowerPlacement towerPlacement;

    [SerializeField]
    private PauseMenu pauseMenu;

    private Material[] originalMaterials;

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
        towerSaveLoadManager = GameObject.FindObjectOfType<TowerSaveLoadManager>();
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

            SetSelectedTowerMaterial();

            basicTowerScript towerScript = selectedTower.GetComponentInChildren<basicTowerScript>();


            towerCanvasInstance = Instantiate(towerCanvasPrefab) as GameObject;


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
            SetDeselectedTowerMaterial();

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
                int towerCost = towerScript.BuildCost;

                PlayerStats playerStats = PlayerStats.Instance;

                if (playerStats != null)
                {
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
                    towerSaveLoadManager.RemoveTower(TowerID);
                }

                if (scriptName == "mortarTower")
                {
                    TowerID = selectedTower.transform.GetChild(1).gameObject.GetComponent<mortarTower>().id;
                    Debug.Log(TowerID);
                    towerSaveLoadManager.RemoveTower(TowerID);
                }

                if (scriptName == "tetherTower")
                {
                    TowerID = selectedTower.transform.GetChild(1).gameObject.GetComponent<tetherTower>().id;
                    Debug.Log(TowerID);
                    towerSaveLoadManager.RemoveTower(TowerID);
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

    private void SetSelectedTowerMaterial()
    {
        // Store the original materials of the tower's children, excluding the Tower Particle System
        Renderer[] childRenderers = selectedTower.GetComponentsInChildren<Renderer>().Where(r => r.gameObject.name != "Tower Particle System").ToArray();
        originalMaterials = new Material[childRenderers.Length];
        for (int i = 0; i < childRenderers.Length; i++)
        {
            originalMaterials[i] = childRenderers[i].material;
        }

        foreach (Renderer childRenderer in childRenderers)
        {
            childRenderer.material = highlightMaterial;
        }
    }

    private void SetDeselectedTowerMaterial()
    {
        Renderer[] childRenderers = selectedTower.GetComponentsInChildren<Renderer>().Where(r => r.gameObject.name != "Tower Particle System").ToArray();
        for (int i = 0; i < childRenderers.Length; i++)
        {
            childRenderers[i].material = originalMaterials[i];
        }
    }
}
