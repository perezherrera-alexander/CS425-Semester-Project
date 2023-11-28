using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class beeTower : basicTowerScript
{
    [SerializeField] public float[] towerPosition;


    public string id;



    // Start is called before the first frame update

    void Start()
    {
        towerName = "Bee Tower";
        Invoke();
        makeSphere();
        Debug.Log(targeting);
    }

    // Update is called once per frame
    void Update()
    {
        
        track();
        listPrune();
    }

    public override void Shoot()
    {
        GameObject stinger = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
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
