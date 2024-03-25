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
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        nestPanelManage = FindObjectOfType<nestPanelManager>();
    }

    private void OnMouseDown()
    {
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
