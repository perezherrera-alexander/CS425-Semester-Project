using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;


public class TowerPanelManager : MonoBehaviour
{
    basicTowerScript BasicTowerScript;
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
        BasicTowerScript = GameObject.FindObjectOfType<basicTowerScript>();

        
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

    public void ToggleTowerPanel(GameObject Tower)
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

    private void CurrentSelectedTower(GameObject Tower)
    {
        SelectedTower = Tower;
    }

    private void OpenTowerPanel()
    {

        basicTowerScript towerScript = SelectedTower.GetComponentInChildren<basicTowerScript>();

        TowerPanelInstance = Instantiate(TowerPanelPrefab) as GameObject;

        TowerPanelInstance.transform.position = SelectedTower.transform.position;

        // Add a dropdown for tower targeting
        TMP_Dropdown dropdown = TowerPanelInstance.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Dropdown>();
        // Add a button for tower deletion
        Button deleteButton = TowerPanelInstance.transform.GetChild(0).GetChild(0).GetComponent<Button>();
        // Add a button for tower panel close
        Button deleteButton2 = TowerPanelInstance.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Button>();

        if (towerScript != null)
        {
            Type scriptType = towerScript.GetType();
            string scriptName = scriptType.Name;
            Debug.Log(scriptName);

            TMP_Text output = TowerPanelInstance.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            output.text = "Tower Name: " + scriptName;

            if (scriptName == "beeTower")
            {
                Debug.Log("Saved targeting option is : " + SelectedTower.transform.GetChild(1).gameObject.GetComponent<beeTower>().targetingint);
                dropdown.value = SelectedTower.transform.GetChild(1).gameObject.GetComponent<beeTower>().targetingint;
                Debug.Log("Targting option is :" + dropdown.value);
            }

            if (scriptName == "mortarTower")
            {
                dropdown.value = SelectedTower.transform.GetChild(1).gameObject.GetComponent<mortarTower>().targetingint;
            }

            if (scriptName == "tetherTower")
            {
                dropdown.value = SelectedTower.transform.GetChild(1).gameObject.GetComponent<tetherTower>().targetingint;
            }
        }

        if (deleteButton != null)
        {
            deleteButton.onClick.AddListener(DeleteSelectedTower);
        }

        if (deleteButton2 != null)
        {
            deleteButton2.onClick.AddListener(DeleteTowerPanel);
        }

        if (dropdown != null)
        {
            Debug.Log("Listening");
            dropdown.onValueChanged.RemoveListener(TowerTargeting);
            dropdown.onValueChanged.AddListener(TowerTargeting);
        }
    }

    private void CloseTowerPanel()
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

    public void TowerTargeting(int selectedindex)
    {
        Debug.Log("Entered tower targeting");
        basicTowerScript towerScript = SelectedTower.GetComponentInChildren<basicTowerScript>();

        Debug.Log(selectedindex);
        switch (selectedindex)
        {
            case 0:
                towerScript.GetComponentInChildren<basicTowerScript>().targeting = "first";
                UpdateTowerInfo(selectedindex);
                break;

            case 1:
                towerScript.GetComponentInChildren<basicTowerScript>().targeting = "last";
                UpdateTowerInfo(selectedindex);
                break;

            case 2:
                towerScript.GetComponentInChildren<basicTowerScript>().targeting = "close";
                UpdateTowerInfo(selectedindex);
                break;

            case 3:
                towerScript.GetComponentInChildren<basicTowerScript>().targeting = "strong";
                UpdateTowerInfo(selectedindex);
                break;
        }

    }

    private void UpdateTowerInfo(int selectedindex)
    {
        basicTowerScript towerScript = SelectedTower.GetComponentInChildren<basicTowerScript>();
        string TowerID;

        if (towerScript != null)
        {
            Type scriptType = towerScript.GetType();
            string scriptName = scriptType.Name;
            Debug.Log(scriptName);

            if (scriptName == "beeTower")
            {
                SelectedTower.transform.GetChild(1).gameObject.GetComponent<beeTower>().targetingint = selectedindex;
                TowerID = SelectedTower.transform.GetChild(1).gameObject.GetComponent<beeTower>().id;
                towerSaveLoadManager.UpdateTargetingint(TowerID, selectedindex);
            }

            if (scriptName == "mortarTower")
            {
                SelectedTower.transform.GetChild(1).gameObject.GetComponent<mortarTower>().targetingint = selectedindex;
                TowerID = SelectedTower.transform.GetChild(1).gameObject.GetComponent<mortarTower>().id;
                towerSaveLoadManager.UpdateTargetingint(TowerID, selectedindex);
            }

            if (scriptName == "tetherTower")
            {
                SelectedTower.transform.GetChild(1).gameObject.GetComponent<tetherTower>().targetingint = selectedindex;
                TowerID = SelectedTower.transform.GetChild(1).gameObject.GetComponent<tetherTower>().id;
                towerSaveLoadManager.UpdateTargetingint(TowerID, selectedindex);
            }
        }
    }
}