using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.Playables.AnimationPlayableUtilities;

public class meleeTower : BaseTowerLogic
{
    private float directDamage = 5f;

    private float attackRate = 0f;
    private float coolDown = 0f;
    private Animator animate;
    public string id;
    
    

    public int targetingint;
    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Army Ant";
        Invoke();
        MakeSphere();
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
            coolDown += Time.deltaTime;
            attackRate = 1f / fireRate;
            if(coolDown > 0)
            {

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

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<BaseEnemyLogic>().reduceHealth(directDamage);
            //Debug.Log(other.GetComponent<BaseEnemyLogic>().getHealth());
        }
    }*/
    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}
