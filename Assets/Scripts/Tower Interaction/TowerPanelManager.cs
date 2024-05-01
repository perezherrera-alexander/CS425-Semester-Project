using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;


public class TowerPanelManager : MonoBehaviour
{
    BaseTowerLogic BaseTowerLogic;
    private static TowerPanelManager instance;
    public static TowerPanelManager Instance { get { return instance; } }

    public GameObject TowerPanelPrefab;
    public GameObject TowerPanelInstance;
    public GameObject SpecialTowerPanelPrefab;
    public GameObject SelectedTower;

    private Material[] originalMaterials;
    public Material highlightMaterial;

    public bool PanelState = false;

    public string TowerID;

    [SerializeField]
    private TowerSaveLoadManager towerSaveLoadManager;

    private void Start()
    {
        towerSaveLoadManager = GameObject.FindObjectOfType<TowerSaveLoadManager>();
        BaseTowerLogic = GameObject.FindObjectOfType<BaseTowerLogic>();

        
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
            TurnOffTowerOutline();

            CurrentSelectedTower(Tower);

            OpenTowerPanel();
            TurnOnTowerOutline();
        }
        // Same tower selected, close tower panel and deselect tower
        else if (SelectedTower == Tower && PanelState == true)
        {
            PanelState = false;
            CloseTowerPanel();
            TurnOffTowerOutline();
        }
        // Same tower selected, open tower panel and select tower
        else if (SelectedTower == Tower && PanelState == false)
        {
            PanelState = true;
            OpenTowerPanel();
            TurnOnTowerOutline();
        }
        // Tower selected open tower panel and select
        else
        {
            PanelState = true;
            CurrentSelectedTower(Tower);
            OpenTowerPanel();
            //GetOriginalMaterial();
            TurnOnTowerOutline();
        }
    }

    private void CurrentSelectedTower(GameObject Tower)
    {
        SelectedTower = Tower;
    }

    private void OpenTowerPanel()
    {
        BaseTowerLogic towerScript = SelectedTower.GetComponentInChildren<BaseTowerLogic>();
        Button deleteButton2;

        if (towerScript.towerName == "Centipede Mother" || towerScript.towerName == "Moth Man")
        {
            TowerPanelInstance = Instantiate(SpecialTowerPanelPrefab);
            TowerPanelInstance.transform.position = SelectedTower.transform.position;
            deleteButton2 = TowerPanelInstance.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Button>();

        }
        else
        {
            TowerPanelInstance = Instantiate(TowerPanelPrefab) as GameObject;
            TowerPanelInstance.transform.position = SelectedTower.transform.position;
            TowerPanelInstance.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Dropdown>().value = (int)towerScript.targetingType;
            deleteButton2 = TowerPanelInstance.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Button>();


        }
        TMP_Dropdown dropdown = TowerPanelInstance.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Dropdown>();

        // Add a dropdown for tower targeting
        // Add a button for tower deletion
        Button deleteButton = TowerPanelInstance.transform.GetChild(0).GetChild(0).GetComponent<Button>();
        // Add a button for tower panel close

        if (towerScript != null)
        {
            Type scriptType = towerScript.GetType();
            string scriptName = scriptType.Name;
            Debug.Log(scriptName);

            TMP_Text output = TowerPanelInstance.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            output.text = "Tower Name: " + towerScript.towerName;


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
        TurnOffTowerOutline();
        Destroy(TowerPanelInstance);
    }

    public void DeleteSelectedTower()
    {
        BaseTowerLogic towerScript = SelectedTower.GetComponentInChildren<BaseTowerLogic>();

        if (towerScript != null)
        {
            int towerCost = towerScript.buildCost;

            PlayerStatistics playerStats = PlayerStatistics.Instance;

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


            if (scriptName == "BeeSwarm")
            {
                
                SelectedTower.transform.GetChild(1).GetComponent<BeeSwarm>().deleteSwarmifDeleted();
            }
        }

        Debug.Log("Deleting tower through player interaction.");
        Destroy(SelectedTower);
        PanelState = false;
        Destroy(TowerPanelInstance);
        SelectedTower = null;
    }

    // The tower parent (SelectedTower) should have an Outline component. These methods simply turn them on and off
    private void TurnOnTowerOutline()
    {
        
        var outline = SelectedTower.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = true;
        }
        var range = SelectedTower.GetComponentInChildren<BaseTowerLogic>().rangeFinder;
        if (range != null)
        {
            range.SetActive(true);
        }
    }

    private void TurnOffTowerOutline()
    {
        var outline = SelectedTower.GetComponent<Outline>();
        if(outline != null)
        {
            outline.enabled = false;
        }

        var range = SelectedTower.GetComponentInChildren<BaseTowerLogic>().rangeFinder;
        if(range != null)
        {
            range.SetActive(false);
        }

    }

    public void TowerTargeting(int selectedTargetingType)
    {
        TargetingTypes castedSelectedTargetingType = (TargetingTypes) selectedTargetingType;
        Debug.Log("Entered tower targeting");
        BaseTowerLogic towerScript = SelectedTower.GetComponentInChildren<BaseTowerLogic>();

        Debug.Log(selectedTargetingType);
        switch (selectedTargetingType)
        {
            case 0:
                towerScript.GetComponentInChildren<BaseTowerLogic>().targetingType = TargetingTypes.First;
                UpdateTowerInfo(castedSelectedTargetingType);
                break;

            case 1:
                towerScript.GetComponentInChildren<BaseTowerLogic>().targetingType = TargetingTypes.Last;
                UpdateTowerInfo(castedSelectedTargetingType);
                break;

            case 2:
                towerScript.GetComponentInChildren<BaseTowerLogic>().targetingType = TargetingTypes.Close;
                UpdateTowerInfo(castedSelectedTargetingType);
                break;

            case 3:
                towerScript.GetComponentInChildren<BaseTowerLogic>().targetingType = TargetingTypes.Strong;
                UpdateTowerInfo(castedSelectedTargetingType);
                break;
        }

    }

    private void UpdateTowerInfo(TargetingTypes selectedTargetingType)
    {
        BaseTowerLogic towerScript = SelectedTower.GetComponentInChildren<BaseTowerLogic>();
        string TowerID;

        if (towerScript != null)
        {
            Type scriptType = towerScript.GetType();
            string scriptName = scriptType.Name;
            Debug.Log(scriptName);

            if (scriptName == "BeeTower")
            {
                SelectedTower.transform.GetChild(1).gameObject.GetComponent<BeeTower>().targetingType = selectedTargetingType;
                TowerID = SelectedTower.transform.GetChild(1).gameObject.GetComponent<BeeTower>().id;
                towerSaveLoadManager.UpdateTargetingType(TowerID, selectedTargetingType);
            }

            if (scriptName == "mortarTower")
            {
                SelectedTower.transform.GetChild(1).gameObject.GetComponent<mortarTower>().targetingType = selectedTargetingType;
                TowerID = SelectedTower.transform.GetChild(1).gameObject.GetComponent<mortarTower>().id;
                towerSaveLoadManager.UpdateTargetingType(TowerID, selectedTargetingType);
            }

            if (scriptName == "tetherTower")
            {
                SelectedTower.transform.GetChild(1).gameObject.GetComponent<tetherTower>().targetingType = selectedTargetingType;
                TowerID = SelectedTower.transform.GetChild(1).gameObject.GetComponent<tetherTower>().id;
                towerSaveLoadManager.UpdateTargetingType(TowerID, selectedTargetingType);
            }
        }
    }
}