using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class SelectTower : MonoBehaviour
{
    public GameObject towerCanvasPrefab;
    private GameObject selectedTower;
    public string TowerID;

    [SerializeField]
    private TowerSaveLoadManager towerSaveLoadManager;

    [SerializeField]
    private TowerPlacement towerPlacement;

    [SerializeField]
    private PauseMenu pauseMenu;

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
        TowerPanelManager towerPanelManager = TowerPanelManager.Instance;

        if (towerPanelManager != null)
        {
            towerPanelManager.ToggleTowerPanel(tower);
        }
    }

    public void DeleteSelectedTower()
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
        selectedTower = null;
    }
}
