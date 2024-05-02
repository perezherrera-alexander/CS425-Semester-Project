using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWriter : MonoBehaviour
{
    public static TextWriter Instance { get; private set; }
    private List<TextWriterSingle> textWriterSingleList;

    private void Awake()
    {
        textWriterSingleList = new List<TextWriterSingle>();
        Instance = this;
    }
    public TextWriterSingle AddText(TextMeshProUGUI text, string message, float speed, bool invisible)
    {
        TextWriterSingle textWriterSingle = new TextWriterSingle(text, message, speed, invisible);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }
    public static TextWriterSingle AddText_Static(TextMeshProUGUI text, string message, float speed, bool invisible, bool removeWriter)
    {
        if (removeWriter)
        {
            Instance.RemoveWriter(text);
        }
        return Instance.AddText(text, message, speed, invisible);

    }
    private void Update()
    {
        //Debug.Log("textWriterSingleList.Count: " + textWriterSingleList.Count);
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            bool destroyInstance = false;
            textWriterSingleList[i].Update();
            if (textWriterSingleList[i] == null)
            {
                destroyInstance = true;
            }
            if (destroyInstance)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }
    private void RemoveWriter(TextMeshProUGUI uiText)
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            if (textWriterSingleList[i].GetUIText() == uiText)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    public static void RemoveWriter_Static(TextMeshProUGUI uiText)
    {
        Instance.RemoveWriter(uiText);
    }

    public class TextWriterSingle{
        private TextMeshProUGUI uiText;
        private string message;
        private int characterIndex;
        private float speed;
        private float timer;
        private bool invisible;
        public TextWriterSingle(TextMeshProUGUI text, string message, float speed, bool invisible)
        {
            this.uiText = text;
            this.message = message;
            this.speed = speed;
            this.invisible = invisible;

            characterIndex = 0;
        }
        public bool Update()
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
                    return true;
                }
            }
            return false;
        }

        public TextMeshProUGUI GetUIText()
        {
            return uiText;
        }

        public bool IsActive()
        {
            return uiText != null;
        }

        public void WriteDestroy(){
            uiText.text = message;
            TextWriter.RemoveWriter_Static(uiText);
            characterIndex = message.Length;
        }
    }
}
