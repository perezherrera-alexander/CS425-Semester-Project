using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BeetleMelee : BaseTowerLogic
{
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

    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}