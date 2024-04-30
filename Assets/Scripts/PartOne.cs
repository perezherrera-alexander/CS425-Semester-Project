using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PartOne : MonoBehaviour
{
    [SerializeField] private TextWriter textWriter;
    public TextMeshProUGUI messagetext;
    private void Awake(){
        messagetext = transform.Find("Message").GetComponent<TextMeshProUGUI>();
        //Application.targetFrameRate = 3;
    }
    private void Start(){
        textWriter.AddText(messagetext, "Hello my baby, hello my honey, hello my ragtime gal", 0.1f, true);
        
    }
}
