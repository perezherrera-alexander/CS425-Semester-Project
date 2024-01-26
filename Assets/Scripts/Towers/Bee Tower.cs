using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BeeTower : BaseTowerLogic
{
    public float[] towerPosition;

    public string id;

    public int targetingint = 0;

    void Start()
    {
        towerName = "Bee Tower";
        Invoke();
        MakeSphere();
        switch (targetingint)
        {
            case 0:
                targeting = "first";
                break;
            case 1:
                targeting = "last";
                break;
            case 2:
                targeting = "close";
                break;
            case 3:
                targeting = "strong";
                break;
        }
        Debug.Log("Targetting Type: " + targeting);
    }

    void Update()
    {
        
        Track();
        ListPrune();
    }

    public override void Shoot()
    {
        GameObject stinger = (GameObject)Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
        stingerScript sting = stinger.GetComponent<stingerScript>();

        if (sting != null)
        {
            sting.Seek(target);
        }
        else if (sting.exists && target == null)
        {

        }
    }


    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}
