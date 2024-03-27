using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class mantisTower : BaseTowerLogic
{

    private Animator animate;
    public string id;
    public float sliceDmg = 0.75f;
    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Mantis Warrior";
        Invoke();
        MakeSphere();
        fireRate = 3f;
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

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    public override void Shoot()
    {
        animate.SetTrigger("Attack");

        var targ = target.GetComponent<BaseEnemyLogic>();
        if(targ == null)
        {
            Debug.Log("Scarab");
        }
        else
        {
            targ.reduceHealth(sliceDmg);
        }

    }

    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}
