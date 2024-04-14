using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class mortarTower : BaseTowerLogic
{
    public GameObject secondRange;
    public StoreTowerUpgradeData storeTowerUpgradeData;
    public string id;
    public GameObject grenades;
    public Transform bandolier;
    public ParticleSystem nuclear;

    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Grenadier Ant";
        Invoke();
        MakeSphere();
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
        proximitySphere.radius = 11.99f;
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
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Nuclear Payload")
            {
                projectilePrefab.GetComponent<MortarLikeProjectile>().splashDamage = 5f;
                Instantiate(nuclear, transform);
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Project Orion")
            {
                targettingRange = 30f;
                proximitySphere = GetComponent<SphereCollider>();
                proximitySphere.radius = 17.98f;
                rangeFinder.SetActive(false);
                rangeFinder = secondRange;
                projectilePrefab.GetComponent<MortarLikeProjectile>().speed = 45f; ;
                Instantiate(grenades, bandolier);

            }
            count++;
        }
    }
}
