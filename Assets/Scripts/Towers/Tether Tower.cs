using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class tetherTower : BaseTowerLogic
{
    public LineRenderer laser;
    private float dot = 0.5f;
    #pragma warning disable 0414
    private float slowDownFactor = 0.5f;
    #pragma warning restore 0414
    public string id;
    void Start()
    {
        towerName = "Tether Tower";
        Invoke();
        MakeSphere();
    }

    void Update()
    {
        Track();
    }

    public override void Track()
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
        barrelToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);


        Shoot();
    }

    public override void Shoot()
    {
        if (!laser.enabled)
            laser.enabled = true;
        laser.SetPosition(0, locationToFireFrom.position);
        laser.SetPosition(1, target.position);

        float damagePerFrame = dot * Time.deltaTime;
        target.GetComponent<BaseEnemyLogic>().reduceHealth(damagePerFrame);
        //target.GetComponent<BaseEnemyLogic>().slowDown(slowDownFactor);


    }


    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}
