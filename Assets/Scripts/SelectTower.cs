using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.PlasticSCM.Editor.WebApi.CredentialsResponse;

public class SelectTower : MonoBehaviour
{
    public GameObject Tower; // Reference to the canvas panel prefab

    private GameObject towerCanvasInstance; // Instance of the canvas panel

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                ToggleTowerCanvas(Tower);
            }
        }
    }

    private void ToggleTowerCanvas(GameObject towerCanvasPrefab)
    {
        if (towerCanvasInstance == null)
        {
            // Instantiate the tower canvas prefab
            towerCanvasInstance = Instantiate(towerCanvasPrefab) as GameObject;

            // Set the position of the canvas (optional: set it relative to the tower's position)
            towerCanvasInstance.transform.position = transform.position;

            // Customize additional settings if needed
        }
        else
        {
            // Destroy the canvas if it already exists
            Destroy(towerCanvasInstance);
        }
    }
}
