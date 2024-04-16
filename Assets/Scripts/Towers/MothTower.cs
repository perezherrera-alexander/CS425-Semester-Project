using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MothTower : BaseTowerLogic
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    public Transform center;
    public ParticleSystem particles;
    private float timer = 0f;
    private float angle = 0f;
    public bool stronger = false;
    public bool bigger = false;
    public Material upgrade;
    public List<Material> materials;


    // Start is called before the first frame update
    void Start()
    {
        fireRate = 0.3f;
        towerName = "Moth Man";
        transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().GetMaterials(materials);
        AddUpgradeEffects();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        circle(timer);

        if(isActive == true)
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                Instantiate(particles, transform);
                fireCountdown = 1f / fireRate;
            }
        }
        else
        {

        }

            
        
        fireCountdown -= Time.deltaTime;
    }

    public override void Shoot()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, targettingRange);

        foreach(Collider c in colliders)
        {
            if (c.GetComponent<BaseEnemyLogic>())
            {
                if (bigger)
                {
                    c.GetComponent<BaseEnemyLogic>().stunByPass(4f);
                }
                else
                {
                    if (stronger)
                    {
                        c.GetComponent<BaseEnemyLogic>().stun(4f);
                    }
                    else
                    {
                        c.GetComponent<BaseEnemyLogic>().stun(2f);
                    }
                }

                
                //Have this tower do knockback for now to test functionality, this tower will stun enemies when the stun effect is implemented
            }
        }
    }

    void circle(float Time)
    {
        angle = Time * 1f;
        var offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * 15f;
        transform.position = center.position + offset;
        transform.LookAt(center);
    }

    public void AddUpgradeEffects()
    {
        int count = 1;
        while (count <= storeTowerUpgradeData.ListOfUpgradesObtained.Count)
        {
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Moth Upgrade 1")
            {
                stronger = true;
                materials[5] = upgrade;
                transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().materials = materials.ToArray();
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Moth Upgrade 2")
            {
                bigger = true;
                transform.GetChild(0).transform.localScale = new Vector3(2.04f,2.04f,2.04f);
            }
            count++;
        }
    }
}
