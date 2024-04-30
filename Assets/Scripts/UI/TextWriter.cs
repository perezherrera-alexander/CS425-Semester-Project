using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private TextMeshProUGUI uiText;
    private string message;
    private int characterIndex;
    private float speed;
    private float timer;
    private bool invisible;
    public void AddText(TextMeshProUGUI text, string message, float speed, bool invisible)
    {
        this.uiText = text;
        this.message = message;
        this.speed = speed;
        this.invisible = invisible;

        characterIndex = 0;
    }
    private void Update()
    {
        if (uiText!= null)
        {
            timer -= Time.deltaTime;
            //int characters = (int)timer;
            while (timer <=0f)
            {
                timer += speed;
                characterIndex++;
                string text = message.Substring(0, characterIndex);
                if (invisible)
                {
                    text += "<color=#00000000>" + message.Substring(characterIndex) + "</color>";
                }
                //else
                uiText.text = text;

                if (characterIndex >= message.Length)
                {
                    uiText = null;
                    return;
                }
            }
            
        }
    }
}
