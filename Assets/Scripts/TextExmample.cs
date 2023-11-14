using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextExmample : MonoBehaviour
{

    public string TextValue;
    public TextMeshProUGUI towernameUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        towernameUI.text = TextValue;
    }
}
