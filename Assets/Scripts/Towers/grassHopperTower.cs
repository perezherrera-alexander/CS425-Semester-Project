using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class grassHopperTower : BaseTowerLogic
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    public GameObject grassHop1;
    public GameObject grassHop2;
    public string id;
    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Grasshopper Lair";
        Invoke();
        MakeSphere();
        grassHop1.SetActive(false);
        grassHop2.SetActive(false);
        curAttackSpeed = fireRate;
        AddUpgradeEffects();
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

    public void AddUpgradeEffects()
    {
        int count = 1;
        while (count <= storeTowerUpgradeData.ListOfUpgradesObtained.Count)
        {
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Jumping Jacks")
            {
                grassHop1.SetActive(true);
                projectilePrefab.GetComponent<grassHopperProjectile>().bounces = 4f;
                projectilePrefab.GetComponent<grassHopperProjectile>().jumper = true;
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Locust Swarm")
            {
                grassHop2.SetActive(true);
                projectilePrefab.GetComponent<grassHopperProjectile>().waved = true;
            }
            count++;
        }
    }
}
