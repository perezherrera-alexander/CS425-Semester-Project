using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetherTower : basicTowerScript
{
    public LineRenderer laser;

    private float dot = 0.1f;
    private float slowDownFactor = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke();
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
        target.GetComponent<baseEnemyScript>().reduceHealth(dot);
        target.GetComponent<baseEnemyScript>().slowDown(slowDownFactor);

        if (!laser.enabled)
            laser.enabled = true;
        laser.SetPosition(0, firePoint.position);
        laser.SetPosition(1, target.position);
    }
}
