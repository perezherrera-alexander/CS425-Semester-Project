using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BeeSwarm : BaseTowerLogic
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    public string id;
    public List<SwarmingBee> projectiles = new List<SwarmingBee>();
    private float swarmAmount = 5f;

    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Bee Swarm";
        Invoke();
        MakeSphere();
        curAttackSpeed = fireRate;
        AddUpgradeEffects();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (data != null)
            {
                handleEffect();
            }
            Track();
            ListPrune();
        }
    }

    public override void Track()
    {
        if (target == null)
        {
            if (projectiles.Count <= swarmAmount)
            {
                if (fireCountdown <= 0f)
                {
                    Swarm();
                    fireCountdown = 1f / fireRate;
                }
                fireCountdown -= Time.deltaTime;
            }
        }
        else if(target != null && projectiles.Count > 1)
        {
            Shoot();
            //removeFromList();
        }
        else if(target != null && projectiles.Count == 0)
        {
            if (projectiles.Count <= swarmAmount)
            {
                if (fireCountdown <= 0f)
                {
                    Swarm();
                    fireCountdown = 1f / fireRate;
                }
                fireCountdown -= Time.deltaTime;
            }
            Shoot();
        }
        else
        {
            Shoot();
            //removeFromList();
        }
    }
    public override void Shoot()
    {
        foreach (SwarmingBee projectile in projectiles)
        {
            projectile.Seek(target);
        }
        removeFromList();
    }

    public void removeFromList()
    {
        projectiles.RemoveAll(item => item == null);
    }

    public void Swarm()
    {
        GameObject shot = (GameObject)Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
        SwarmingBee bee = shot.GetComponent<SwarmingBee>();
        projectiles.Add(bee);
    }

    public void deleteSwarmifDeleted()
    {
        foreach(SwarmingBee projectile in projectiles)
        {
            projectile.destroyFromList();
        }
        projectiles.Clear();
    }

    public override void MakeSphere()
    {
        proximitySphere = GetComponent<SphereCollider>();
        proximitySphere.radius = 11.93f;
    }


    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }

    public void AddUpgradeEffects()
    {
        int count = 0;
        while (count <= storeTowerUpgradeData.ListOfUpgradesObtained.Count)
        {
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count] == "Bee Swarm Upgrade 1")
            {
                swarmAmount = 10;
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count] == "Bee Swarm Upgrade 2")
            {
                fireRate = 3f;
            }
            count++;
        }
    }
}
