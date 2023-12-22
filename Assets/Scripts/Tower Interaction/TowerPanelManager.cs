using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;


public class TowerPanelManager : MonoBehaviour
{
    private static TowerPanelManager instance;
    public static TowerPanelManager Instance { get { return instance; } }

    public GameObject TowerPanelPrefab;
    public GameObject TowerPanelInstance;
    public GameObject SelectedTower;

    private Material[] originalMaterials;
    public Material highlightMaterial;

    private bool PanelState = false;

    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleTowerPanel (GameObject Tower)
    {
        // New tower selected, close previous tower panel and select new tower and open new panel
        if (SelectedTower != null && SelectedTower != Tower)
        {
            PanelState = true;

            CloseTowerPanel(Tower);
            SetDeselectedTowerMaterial();

            CurrentSelectedTower(Tower);

            OpenTowerPanel();
            SetSelectedTowerMaterial();
        }
        // Same tower selected, close tower panel and deselect tower
        else if (SelectedTower == Tower && PanelState == true)
        {
            PanelState = false;
            CloseTowerPanel(Tower);
            SetDeselectedTowerMaterial();
        }
        // Same tower selected, open tower panel and select tower
        else if (SelectedTower == Tower && PanelState == false)
        {
            PanelState = true;
            OpenTowerPanel();
            SetSelectedTowerMaterial();
        }
        // Tower selected open tower panel and select
        else
        {
            PanelState = true;
            CurrentSelectedTower(Tower);
            OpenTowerPanel();
            GetOriginalMaterial();
            SetSelectedTowerMaterial();
        }
    }

    private void CurrentSelectedTower (GameObject Tower)
    {
        SelectedTower = Tower;
    }

    private void OpenTowerPanel ()
    {
        basicTowerScript towerScript = SelectedTower.GetComponentInChildren<basicTowerScript>();

        TowerPanelInstance = Instantiate(TowerPanelPrefab) as GameObject;

        TowerPanelInstance.transform.position = SelectedTower.transform.position;

        if (towerScript != null)
        {
            Type scriptType = towerScript.GetType();
            string scriptName = scriptType.Name;
            Debug.Log(scriptName);

            TMP_Text output = TowerPanelInstance.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            output.text = "Tower Name: " + scriptName;
        }
    }

    private void CloseTowerPanel (GameObject Tower)
    {
        Destroy(TowerPanelInstance);
    }

    private void GetOriginalMaterial()
    {
        // Store the original materials of the tower's children
        Renderer[] childRenderers = SelectedTower.GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[childRenderers.Length];
        for (int i = 0; i < childRenderers.Length; i++)
        {
            originalMaterials[i] = childRenderers[i].material;
        }
    }

    private void SetSelectedTowerMaterial()
    {
        Renderer[] childRenderers = SelectedTower.GetComponentsInChildren<Renderer>();
        foreach (Renderer childRenderer in childRenderers)
        {
            childRenderer.material = highlightMaterial;
        }
    }

    private void SetDeselectedTowerMaterial()
    {
        Renderer[] childRenderers = SelectedTower.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < childRenderers.Length; i++)
        {
            childRenderers[i].material = originalMaterials[i];
        }
    }
}