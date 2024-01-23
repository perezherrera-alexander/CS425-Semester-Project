using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class SelectTower : MonoBehaviour
{
    public GameObject towerCanvasPrefab;

    [SerializeField]
    private TowerPlacement towerPlacement;

    [SerializeField]
    private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        towerPlacement = FindObjectOfType<TowerPlacement>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void OnMouseDown()
    {
        if (towerPlacement.IsPlacingTower)
        {
            return;
        }

        if (pauseMenu.GameIsPaused)
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
}