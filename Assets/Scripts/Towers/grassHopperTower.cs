using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class grassHopperTower : BaseTowerLogic
{
    public string id;
    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Grasshopper Lair";
        Invoke();
        MakeSphere();
        curAttackSpeed = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (data != null)
        {
            handleEffect();
        }
        Track();
    }

    public override void Shoot()
    {
        GameObject ball1 = (GameObject)Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
        mortarProjectile ball = ball1.GetComponent<mortarProjectile>();

        if (ball != null)
        {
            ball.Seek(target);
        }
    }

    public override void MakeSphere()
    {
        proximitySphere = GetComponent<SphereCollider>();
        proximitySphere.radius = 19.92f;
    }

    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}
