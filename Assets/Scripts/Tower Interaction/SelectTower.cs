using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using System;
using System.Linq;

public class SelectTower : MonoBehaviour
{

    [SerializeField]
    private TowerPlacement towerPlacement;

    [SerializeField]
    private PauseMenu pauseMenu;

    [SerializeField]
    private nestPanelManager nestMenu;

    [SerializeField]
    private ShopLogic shopLogic;

    [SerializeField]
    private nestPanelManager nestPanel;

    // Start is called before the first frame update
    void Start()
    {
        towerPlacement = FindObjectOfType<TowerPlacement>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        nestMenu = FindObjectOfType<nestPanelManager>();
        shopLogic = FindObjectOfType<ShopLogic>();
        nestPanel = FindObjectOfType<nestPanelManager>();
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

        if (nestMenu.panelState)
        {
            return;
        }

        if (shopLogic.shopIsOpen)
        {
            return;
        }

        if (nestPanel.panelState)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // If we're over a ShopButton, don't select the tower
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
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