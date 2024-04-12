using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaspMelee : BaseTowerLogic
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    public string id;
    private Animator animate;

    private float directDamage = 0.5f;
    private float attackRate = 0f;


    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Tarantula Wasp";
        Invoke();
        MakeSphere();
        fireRate = 1.5f;
        curAttackSpeed = fireRate;
        animate = GetComponentInChildren<Animator>();
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
        ListPrune();
    }

    public override void Track()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        barrelToRotate.rotation = Quaternion.Euler(barrelToRotate.rotation.x, rotation.y, 0f);

        if (attackRate <= 0f)
        {
            Shoot();
            attackRate = 1f / fireRate;
        }

        attackRate -= Time.deltaTime;

    }

    public override void Shoot()
    {
        //animate.SetBool("attack", true);
        animate.SetTrigger("attack");
 
        target.GetComponent<BaseEnemyLogic>().reduceHealth(directDamage);
        //animate.SetBool("attack", false);
    }
    public override void MakeSphere()
    {
        proximitySphere = GetComponent<SphereCollider>();
        proximitySphere.radius = 3.57f;
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
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Wasp Melee Upgrade 1")
            {
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Wasp Melee Upgrade 2")
            {
            }
            count++;
        }
    }
}
