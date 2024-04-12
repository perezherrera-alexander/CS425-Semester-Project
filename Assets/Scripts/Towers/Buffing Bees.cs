using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffingBees : BaseTowerLogic
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    public string id;
    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Bee Buffer";
        enemyTag = "Bee";
        fireRate = 0.2f;
        Invoke();
        MakeSphere();
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

    public override void Track()
    {
        if (target == null)
        {
            
            return;
        }

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    public override void Invoke()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    public override void UpdateTarget()
    {
        if(isActive == false)
        {
            return;
        }

        GameObject[] towers = GameObject.FindGameObjectsWithTag(enemyTag);
        //float shortestDistance = Mathf.Infinity;
        GameObject unBuffed = null;

        foreach (GameObject tower in towers)
        {
            
            float towerDistance = Vector3.Distance(transform.position, tower.transform.position);

            if (towerDistance < targettingRange && tower.GetComponentInChildren<BaseTowerLogic>().isActive == true)
            {

                if (tower.GetComponentInChildren<BaseTowerLogic>().getIsBuffed() == false)
                {
                    if (tower.GetComponentInChildren<BuffingBees>() != null)
                    {

                    }
                    else
                    {
                        unBuffed = tower;
                        break;
                    }

                }
            }
        }

        if (unBuffed != null)
        {
            
            target = unBuffed.transform;
        }
        else
        {
            target = null;
        }
    }

    public override void Shoot()
    {
        
        GameObject buff = (GameObject)Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
        BuffBee bee = buff.GetComponent<BuffBee>();

        if (bee != null)
        {
            bee.Seek(target);
        }
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
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Buffing Bee Upgrade 1")
            {
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Buffing Bee Upgrade 2")
            {
            }
            count++;
        }
    }
}
