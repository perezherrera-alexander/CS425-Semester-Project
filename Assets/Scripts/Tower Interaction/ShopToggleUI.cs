using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopToggleUI : MonoBehaviour
{
    public bool ShopStatus = false;
    public GameObject ShopUI;
    public GameObject ShopButton;
    private float ShopUIWidth;

    public void Start()
    {
        ShopUIWidth = ShopUI.GetComponent<RectTransform>().rect.width;
    }
    public void ToggleShopUI ()
    {
        if (ShopStatus)
        {
            ShopHidden();
        }
        else
        {
            ShopVisible();
        }
    }
    public void ShopVisible ()
    {
        // Move the shop open button to the left
        ShopButton.transform.position = new Vector3(ShopButton.transform.position.x - ShopUIWidth, ShopButton.transform.position.y, ShopButton.transform.position.z);
        ShopUI.SetActive(true);
        ShopStatus = true;
    }

    public void ShopHidden ()
    {
        // Move it back when closing the shop
        ShopButton.transform.position = new Vector3(ShopButton.transform.position.x + ShopUIWidth, ShopButton.transform.position.y, ShopButton.transform.position.z);
        ShopUI.SetActive(false);
        ShopStatus = false;
    }
}
