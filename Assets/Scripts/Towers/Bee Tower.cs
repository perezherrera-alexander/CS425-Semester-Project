using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BeeTower : BaseTowerLogic
{
    //public float[] towerPosition; // This variable is seeming unused
    public string id;
    //public Outline outline;
    void Start()
    {
        createOutline();
        towerName = "Bee Tower";
        Invoke();
        MakeSphere();
        Debug.Log("Targetting Type: " + targetingType);
        curAttackSpeed = fireRate;
    }

    void Update()
    {
        if (data != null)
        {
            handleEffect();
        }
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
