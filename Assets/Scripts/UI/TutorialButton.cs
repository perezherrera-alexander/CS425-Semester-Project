using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    public Button closeButton;

    void Start()
    {
        closeButton.onClick.AddListener(ButtonClosed);
    }

    void ButtonClosed()
    {
        // Delete itself
        Destroy(gameObject);
    }

}
