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

    public string TowerID;

    [SerializeField]
    private TowerSaveLoadManager towerSaveLoadManager;

    private void Start()
    {
        towerSaveLoadManager = GameObject.FindObjectOfType<TowerSaveLoadManager>();
    }
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

            CloseTowerPanel();
            SetDeselectedTowerMaterial();

            CurrentSelectedTower(Tower);

            OpenTowerPanel();
            SetSelectedTowerMaterial();
        }
        // Same tower selected, close tower panel and deselect tower
        else if (SelectedTower == Tower && PanelState == true)
        {
            PanelState = false;
            CloseTowerPanel();
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

        // Add a button for tower deletion
        Button deleteButton = TowerPanelInstance.transform.GetChild(0).GetChild(0).GetComponent<Button>();
        if (deleteButton != null)
        {
            deleteButton.onClick.AddListener(DeleteSelectedTower);
        }

        // Add a button for tower panel close
        Button deleteButton2 = TowerPanelInstance.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Button>();
        if (deleteButton2 != null)
        {
            deleteButton2.onClick.AddListener(DeleteTowerPanel);
        }
    }

    private void CloseTowerPanel ()
    {
        Destroy(TowerPanelInstance);
    }

    private void DeleteTowerPanel()
    {
        PanelState = false;
        SetDeselectedTowerMaterial();
        Destroy(TowerPanelInstance);
    }

    public void DeleteSelectedTower()
    {
        basicTowerScript towerScript = SelectedTower.GetComponentInChildren<basicTowerScript>();

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
                TowerID = SelectedTower.transform.GetChild(1).gameObject.GetComponent<beeTower>().id;
                Debug.Log(TowerID);
                towerSaveLoadManager.RemoveTower(TowerID);
            }

            if (scriptName == "mortarTower")
            {
                TowerID = SelectedTower.transform.GetChild(1).gameObject.GetComponent<mortarTower>().id;
                Debug.Log(TowerID);
                towerSaveLoadManager.RemoveTower(TowerID);
            }

            if (scriptName == "tetherTower")
            {
                TowerID = SelectedTower.transform.GetChild(1).gameObject.GetComponent<tetherTower>().id;
                Debug.Log(TowerID);
                towerSaveLoadManager.RemoveTower(TowerID);
            }
        }

        Debug.Log("Deleting tower through player interaction.");
        Destroy(SelectedTower);
        Destroy(TowerPanelInstance);
        SelectedTower = null;
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