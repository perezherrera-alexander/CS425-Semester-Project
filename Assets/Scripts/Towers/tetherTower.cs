using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class tetherTower : basicTowerScript
{
#pragma warning disable 0414
    public LineRenderer laser;

    private float dot = 0.5f;
    private float slowDownFactor = 0.5f;
#pragma warning restore 0414

    public string id;

    // Start is called before the first frame update
    void Start()
    {
        towerName = "Tether Tower";
        Invoke();
        makeSphere();
    }

    // Update is called once per frame
    void Update()
    {
        track();
    }

    public override void track()
    {
        if (target == null)
        {
            if(laser.enabled)
                laser.enabled = false;
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        part.rotation = Quaternion.Euler(0f, rotation.y, 0f);


        Shoot();
    }

    public override void Shoot()
    {
        if (!laser.enabled)
            laser.enabled = true;
        laser.SetPosition(0, firePoint.position);
        laser.SetPosition(1, target.position);

        float damagePerFrame = dot * Time.deltaTime;
        target.GetComponent<baseEnemyScript>().reduceHealth(damagePerFrame);
        //target.GetComponent<baseEnemyScript>().slowDown(slowDownFactor);


    }


    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}
