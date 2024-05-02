using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;
using TMPro;

public class UI_Assistant : MonoBehaviour
{
    private TextMeshProUGUI messageText;
    private TextWriter.TextWriterSingle textWriterSingle;
    
    private void Awake(){
        messageText = transform.Find("Message").GetComponent<TextMeshProUGUI>();
        //  transform.Find("Message").getComponent<Button_UI>().ClickFunc = () => {
            if (textWriterSingle != null && textWriterSingle.IsActive()){
                textWriterSingle.WriteDestroy();
            }else{
                string[] msgArr = new string[]{
                    "Hello, I am your assistant. I will guide you through the game.",
                    "You should really use shooter towers on straight lines to hit multiple enemies",
                    "Don't forget to use towers with special abilities",
                    "You can upgrade your towers by clicking spending tokens earned from beating maps",
                    "You can also sell towers for some of the price",
                    "Good luck!"
                };
                string msg = msgArr[Random.Range(0, msgArr.Length)];
                textWriterSingle = TextWriter.AddText_Static(messageText, msg, .1f, true, true);
            };
        // };
    }

    private void Start(){
        TextWriter.AddText_Static(messageText, "Hello, I am your assistant. I will guide you through the game.", .1f, true, true);
    }
    // private void Awake(){
    //     partone = transform.Find("PartOne").GetComponent<PartOne>();
    //     //enable partone script after 5 seconds
    //     Invoke("EnablePartOne", 5f);
    //     //Application.targetFrameRate = 60;
    // }
    // private void EnablePartOne(){
    //     partone.enabled = true;
    // }
}
