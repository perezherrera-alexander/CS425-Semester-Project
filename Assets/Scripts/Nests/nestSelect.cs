using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nestSelect : MonoBehaviour
{
    public nestPanelManager nestPanelManager;

    public GameObject nestCanvasPrefab;
    [SerializeField]
    private PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        nestPanelManager = FindObjectOfType<nestPanelManager>();
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
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Tower"))
                {
                    toggleNestCanvas(hit.collider.gameObject);
                }
            }
        }
    }

   private void toggleNestCanvas(GameObject nest)
    {
        nestPanelManager nestPanelManager = nestPanelManager.Instance;

        if(nestPanelManager != null )
        {
            nestPanelManager.ToggleNestPanel(nest);
            Debug.Log("IS THIS NULL");
        }
    }

        
}
