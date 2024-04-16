using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class BeetleMelee : BaseTowerLogic
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    private Animator animate;
    public string id;
    public float damage = 0.5f;
    public bool toss = false;
    public bool hunger = false;
    public Material upgrade;
    public Material upgrade2;
    public List<Material> mat;
    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Stag Staller";
        Invoke();
        MakeSphere();
        fireRate = 0.5f;
        curAttackSpeed = fireRate;
        animate = GetComponentInChildren<Animator>();
        transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().GetMaterials(mat);
        AddUpgradeEffects();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive == false)
        {
            return;
        }
        if (data != null)
        {
            handleEffect();
        }
        Track();
        ListPrune();
    }

    public override void Track()
    {
        if(target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        barrelToRotate.rotation = Quaternion.Euler(barrelToRotate.rotation.x, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    public override void Shoot()
    {
        animate.SetTrigger("Attack");
        if (toss)
        {
            if (hunger)
            {
                if(target.GetComponent<RobotScript>() != null) 
                {
                    target.GetComponent<BaseEnemyLogic>().reduceHealth(5);
                }
                else
                {
                    target.GetComponent<BaseEnemyLogic>().knockbackByPass(100);
                }
            }
            else
            {
                target.GetComponent<BaseEnemyLogic>().knockback(100);
            }
            
        }
        else
        {
            target.GetComponent<BaseEnemyLogic>().knockback(75);
        }
       


    }

    public override void MakeSphere()
    {
        proximitySphere = GetComponent<SphereCollider>();
        proximitySphere.radius = 5.41f;
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
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Stag Beetle Upgrade 1")
            {
                fireRate = 0.75f;
                toss = true;
                mat[2] = upgrade;
                transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().materials = mat.ToArray();
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Stag Beetle Upgrade 2")
            {
                hunger = true;
                mat[4] = upgrade2;
                transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().materials = mat.ToArray();
            }
            count++;
        }
    }
}
