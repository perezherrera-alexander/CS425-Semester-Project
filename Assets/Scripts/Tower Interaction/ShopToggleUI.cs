using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopToggleUI : MonoBehaviour
{
    public bool ShopStatus = false;
    public GameObject ShopUI;

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
        ShopUI.SetActive(true);
        ShopStatus = true;
    }

    public void ShopHidden ()
    {
        ShopUI.SetActive(false);
        ShopStatus = false;
    }
}
