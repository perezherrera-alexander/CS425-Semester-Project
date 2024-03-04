using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BeeSwarm : BaseTowerLogic
{
    public string id;
    public List<SwarmingBee> projectiles = new List<SwarmingBee>();

    // Start is called before the first frame update
    void Start()
    {
        towerName = "Bee Swarm";
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
        ListPrune();
    }

    public override void Track()
    {
        if (target == null)
        {
            if(fireCountdown <= 0f)
            {
                Swarm();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
        else
        {
            Shoot();
            removeFromList();
        }
    }
    public override void Shoot()
    {
        foreach (SwarmingBee projectile in projectiles)
        {
            projectile.Seek(target);
        }
    }

    public void removeFromList()
    {
        projectiles.Clear();
    }

    public void Swarm()
    {
        GameObject shot = (GameObject)Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
        SwarmingBee bee = shot.GetComponent<SwarmingBee>();
        projectiles.Add(bee);
    }

    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}
