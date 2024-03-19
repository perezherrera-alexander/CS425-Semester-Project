using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nestPanelManager : MonoBehaviour
{
    waspNestScript waspNest;
    private static nestPanelManager instance;

    public static nestPanelManager Instance {  get { return instance; } }

    public GameObject selectedNest;
    public GameObject nestPanelInstance;
    public GameObject nestPanelPrefab;

    private bool panelState = false;

    // Start is called before the first frame update
    void Start()
    {
        waspNest = GameObject.FindAnyObjectByType<waspNestScript>();
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
        waspNestScript waspNest = selectedNest.GetComponentInChildren<waspNestScript>();

        nestPanelInstance = Instantiate(nestPanelPrefab) as GameObject;
    }
}
