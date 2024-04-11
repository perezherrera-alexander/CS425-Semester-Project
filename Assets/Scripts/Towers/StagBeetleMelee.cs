using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BeetleMelee : BaseTowerLogic
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    private Animator animate;
    public string id;
    public float damage = 0.5f;
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
        AddUpgradeEffects();
    }

    // Update is called once per frame
    void Update()
    {
        if(data != null)
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
        animate.ResetTrigger("Attack");

        target.GetComponent<BaseEnemyLogic>().reduceHealth(damage);
        target.GetComponent<BaseEnemyLogic>().knockback(75);

        animate.SetTrigger("Attack");
    }

    public override void MakeSphere()
    {
        proximitySphere = GetComponent<SphereCollider>();
        proximitySphere.radius = 3.7f;
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
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count] == "Stag Beetle Upgrade 1")
            {
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count] == "Stag Beetle Upgrade 2")
            {
            }
            count++;
        }
    }
}
