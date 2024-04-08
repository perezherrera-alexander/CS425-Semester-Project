using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaspMelee : BaseTowerLogic
{ 
    public string id;
    private Animator animate;

    private float directDamage = 5f;
    private float attackRate = 0f;


    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Tarantula Wasp";
        Invoke();
        MakeSphere();
        fireRate = 0.3f;
        curAttackSpeed = fireRate;
        animate = GetComponentInChildren<Animator>();
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

    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}
