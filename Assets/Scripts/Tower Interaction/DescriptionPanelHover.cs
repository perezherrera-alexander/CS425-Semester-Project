using System.Collections;
using System.Collections.Generic;
//using PlasticGui.WorkspaceWindow;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DescriptionPanelHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject descriptionPanel;
    private string descriptionPanelName;
    private bool isHovering = false;

    private void Start()
    {
        descriptionPanelName = name.Substring(0, name.Length - 6) + "Description Panel";
        descriptionPanel = GameObject.Find(descriptionPanelName);
    }

    private void Update()
    {
        if (descriptionPanel != null)
        {
            if(isHovering)
            {
                // Have the tower description panel follow the mouse
                Vector3 mousePos = Input.mousePosition;
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(descriptionPanel.transform.parent.GetComponent<RectTransform>(), mousePos, null, out Vector2 localPoint);
                descriptionPanel.transform.localPosition = localPoint;
            }
            else {
                //descriptionPanel.transform.GetChild(0).gameObject.SetActive(false);
            
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Hovering over " + name + "!");
        isHovering = true;
        descriptionPanel.transform.GetChild(0).gameObject.SetActive(true);
        //descriptionPanel = GameObject.Find(descriptionPanelName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("No longer hovering over " + name + "!");
        isHovering = false;
        descriptionPanel.transform.GetChild(0).gameObject.SetActive(false);
        //descriptionPanel = null;
    }
}
