using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEditor.AssetImporters;

public class nestPanelManager : MonoBehaviour
{
    baseNests waspNest;
    private static nestPanelManager instance;

    public static nestPanelManager Instance {  get { return instance; } }

    public GameObject selectedNest;
    public GameObject nestPanelInstance;
    public GameObject nestPanelPrefab;
    public GameObject targetPrefab;

    [SerializeField] public nestTargetPlacement targetPlacement;

    private bool panelState = false;

    // Start is called before the first frame update
    void Start()
    {
        waspNest = GameObject.FindAnyObjectByType<baseNests>();
    }

    // Update is called once per frame
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleNestPanel(GameObject nest)
    {
        if(selectedNest == nest && panelState == true)
        {
            panelState = false;
            closeNestPanel();
            turnOffTarget();
            //turnOffOutline
        }
        else if(selectedNest == nest && panelState == false)
        {
            panelState = true;
            openNestPanel();
            turnOnTarget();
            //turnOffOutline
        }
        else
        {
            panelState = true;
            nestSelected(nest);
            openNestPanel();
            turnOnTarget();
            //turnOnOutline
        }
    }

    public void nestSelected(GameObject nest)
    {
        selectedNest = nest;
    }

    private void closeNestPanel()
    {
        Destroy(nestPanelInstance);

    }

    private void openNestPanel()
    {
        baseNests waspNest = selectedNest.GetComponentInChildren<baseNests>();

        nestPanelInstance = Instantiate(nestPanelPrefab) as GameObject;

        nestPanelInstance.transform.position = selectedNest.transform.position;

        Button targetButton = nestPanelInstance.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>();

        Button deletePanel = nestPanelInstance.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Button>();

        if (waspNest != null)
        {
            Type scriptType = waspNest.GetType();
            string scriptName = scriptType.Name;
            Debug.Log(scriptName);

            TMP_Text output = nestPanelInstance.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            output.text = "Nest Name: " + scriptName;

        }

        if (targetButton != null)
        {
            targetButton.onClick.AddListener(moveTarget);
        }

        if (deletePanel != null)
        {
            deletePanel.onClick.AddListener(deletePanelFunction);
        }

    }

    public void moveTarget()
    {
        targetPlacement.placeTarget(targetPrefab,selectedNest);
    }

    private void deletePanelFunction()
    {
        panelState = false;
        turnOffTarget();
        Destroy(nestPanelInstance);
    }

    private void turnOffTarget()
    {
        var targ = selectedNest.GetComponentInChildren<baseNests>();
        targ.hideTarget();
    }

    private void turnOnTarget()
    {
        var targ = selectedNest.GetComponentInChildren<baseNests>();
        targ.showTarget();
    }
}
