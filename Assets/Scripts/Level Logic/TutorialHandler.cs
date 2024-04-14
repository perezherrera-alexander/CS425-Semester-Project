using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public GameObject tutorialPanelPrefab;
    [TextArea(3, 10)]
    public List<string> tutorialTexts;
    public List<Vector2> tutorialPanelPositions;
    private List<GameObject> gameObjects = new List<GameObject>();
    private GameObject playerInterface;
    public int indexOfCurrentlyActivePanel = 0;
    // Acount for canvas scaling

    private void Start()
    {
        playerInterface = GameObject.Find("PlayerInterface");
        int i = 0;
        foreach (string text in tutorialTexts)
        {
            AddTutorialPanel(text, tutorialPanelPositions[i++]);
        }
    }

    void AddTutorialPanel(string text, Vector2 position)
    {
        GameObject tutorialPanel = Instantiate(tutorialPanelPrefab, playerInterface.transform);
        //Debug.Log("Creating at position: " + position);
        tutorialPanel.transform.localPosition = new Vector3(position.x - 960, position.y + 540, 0);
        //tutorialPanel.transform.localPosition = new Vector3(position.x, position.y, 0);
        //Debug.Log("Created at position: " + tutorialPanel.transform.localPosition);
        tutorialPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = text;
        tutorialPanel.SetActive(false);
        gameObjects.Add(tutorialPanel);
    }

    public void ShowTutorialPanel(int index)
    {
        gameObjects[index].SetActive(true);
    }

    public void HideTutorialPanel(int index)
    {
        gameObjects[index].SetActive(false);
    }

    void Update()
    {
        if(indexOfCurrentlyActivePanel >= gameObjects.Count)
        {
            return;
        }
        // Find a tutorial panel that is active
        if(gameObjects[indexOfCurrentlyActivePanel] == null)
        {
            indexOfCurrentlyActivePanel++;
        }

        if(indexOfCurrentlyActivePanel >= gameObjects.Count)
        {
            return;
        }

        if (!gameObjects[indexOfCurrentlyActivePanel].activeSelf)
        {
            gameObjects[indexOfCurrentlyActivePanel].SetActive(true);
        }
    }


}
