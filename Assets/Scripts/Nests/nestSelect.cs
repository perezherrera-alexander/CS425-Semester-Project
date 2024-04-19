using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nestSelect : MonoBehaviour
{
    [SerializeField]
    private nestPanelManager nestPanelManage;

    public GameObject nestCanvasPrefab;
    [SerializeField]
    private PauseMenu pauseMenu;
    private ShopLogic shopLogic;
    private TowerPanelManager towerPanel;
    private TowerPlacement towerPlacement;
    private nestTargetPlacement targetPlacement;
    // Start is called before the first frame update
    void Start()
    {
        towerPlacement = FindObjectOfType<TowerPlacement>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        nestPanelManage = FindObjectOfType<nestPanelManager>();
        shopLogic = FindObjectOfType<ShopLogic>();
        towerPanel = FindObjectOfType<TowerPanelManager>();
        targetPlacement = FindObjectOfType<nestTargetPlacement>();
    }

    private void OnMouseDown()
    {
        if (towerPlacement.IsPlacingTower)
        {
            return;
        }
        if (targetPlacement.isPlacingTarget)
        {
            return;
        }
        if (pauseMenu.GameIsPaused)
        {
            return;
        }

        if (shopLogic.shopIsOpen)
        {
            return;
        }

        if (towerPanel.PanelState)
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
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Nest"))
                {
                    toggleNestCanvas(hit.collider.gameObject);

                }
            }
        }
    }

   private void toggleNestCanvas(GameObject nest)
    {
        nestPanelManager nestpanelManager = nestPanelManager.Instance;
        
        if (nestpanelManager != null )
        {
            nestpanelManager.ToggleNestPanel(nest);
            
        }
    }

        
}
