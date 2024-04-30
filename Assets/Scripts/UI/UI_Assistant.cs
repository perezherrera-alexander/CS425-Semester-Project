using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

public class UI_Assistant : MonoBehaviour
{
    [SerializeField] private PartOne partone;
    private void Awake(){
        partone = transform.Find("PartOne").GetComponent<PartOne>();
        //enable partone script after 5 seconds
        Invoke("EnablePartOne", 5f);
        //Application.targetFrameRate = 60;
    }
    private void EnablePartOne(){
        partone.enabled = true;
    }
}
