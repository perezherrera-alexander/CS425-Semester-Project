using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionPanelHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject descriptionPanel;
    private string descriptionPanelName;

    private void Start()
    {
        descriptionPanelName = name.Substring(0, name.Length - 6) + "Description Panel";
        descriptionPanel = GameObject.Find(descriptionPanelName);
    }

    private void Update()
    {
        if (descriptionPanel != null)
        {
            // Have the tower description panel follow the mouse
            Vector3 mousePos = Input.mousePosition;
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(descriptionPanel.transform.parent.GetComponent<RectTransform>(), mousePos, null, out Vector2 localPoint);
            descriptionPanel.transform.localPosition = localPoint;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionPanel.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.transform.GetChild(0).gameObject.SetActive(false);
    }
}
