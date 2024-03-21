using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class nestPanelManager : MonoBehaviour
{
    baseNests waspNest;
    private static nestPanelManager instance;

    public static nestPanelManager Instance {  get { return instance; } }

    public GameObject selectedNest;
    public GameObject nestPanelInstance;
    public GameObject nestPanelPrefab;

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
            //turnOffOutline
        }
        else if(selectedNest == nest && panelState == false)
        {
            panelState = true;
            openNestPanel();
            //turnOffOutline
        }
        else
        {
            panelState = true;
            nestSelected(nest);
            openNestPanel();
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

        if (waspNest != null)
        {
            Type scriptType = waspNest.GetType();
            string scriptName = scriptType.Name;
            Debug.Log(scriptName);

            if(targetButton != null)
            {
                Debug.Log("Null");
                targetButton.onClick.AddListener(moveTarget);
            }


        }

    }

    public void moveTarget()
    {
        Debug.Log("Hello");
    }
}
