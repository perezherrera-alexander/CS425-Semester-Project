using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.Playables.AnimationPlayableUtilities;
using Codice.CM.Common;

public class meleeTower : BaseTowerLogic
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    private float directDamage = 5f;

    private float attackRate = 0f;
    private float speed = 30f;
    private Animator animate;
    public string id;
    public Transform center;
    private bool attacking;
    
    

    public int targetingint;
    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Army Ant";
        Invoke();
        MakeSphere();
        attacking = true;
        fireRate = 0.2f;
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
            transform.position = center.position;

            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        barrelToRotate.rotation = Quaternion.Euler(barrelToRotate.rotation.x, rotation.y, 0f);
        

        if (attackRate <= 0f)
        {
            attacking = false;
            attackRate = 1f / fireRate;
 
        }
        else if(attackRate > 0f && attacking == false)
        {
            if (checkDistance())
            {
                Shoot();
                transform.position = center.position;
                attacking = true;
            }
            else 
            {

                float distancePerFrame = speed * Time.deltaTime;
                transform.Translate(dir.normalized * distancePerFrame, Space.World);
            }


        }

        attackRate -= Time.deltaTime;

    }

    public override void Shoot()
    {
        //pending animation/sound/visual effects code
        //animate.Play("Base Layer.New Animation");
        target.GetComponent<BaseEnemyLogic>().reduceHealth(directDamage);


    }
    public bool checkDistance()
    {
        bool nextToTarget = false;

        float check = Vector3.Distance(transform.position, target.position);
        if (check < 0.1)
        {
            nextToTarget = true;
            return nextToTarget;
        }
        return nextToTarget;
    }
    public override void MakeSphere()
    {
        proximitySphere = GetComponent<SphereCollider>();
        proximitySphere.radius = 5.46f;
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
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Melee Ant Upgrade 1")
            {
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Melee Ant Upgrade 2")
            {
            }
            count++;
        }
    }
}
