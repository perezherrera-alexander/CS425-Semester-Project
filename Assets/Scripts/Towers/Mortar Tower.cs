using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class mortarTower : BaseTowerLogic
{


    public string id;

    public int targetingint;


    // Start is called before the first frame update
    void Start()
    {
        towerName = "Mortar Tower";
        Invoke();
        makeSphere();
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
    }

    // Update is called once per frame
    void Update()
    {
        track();
    }

    public override void Shoot()
    {
        GameObject ball1 = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        mortarProjectile ball = ball1.GetComponent<mortarProjectile>();

        if (ball != null)
        {
            ball.Seek(target);
        }
    }


    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}
