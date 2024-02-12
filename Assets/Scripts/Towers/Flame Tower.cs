using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FlameTower : BaseTowerLogic
{
    public string id;
    private bool firing = false;
    public ParticleSystem visual;
    public CapsuleCollider fireArea;
    private float damage = 0.333f;

    // Start is called before the first frame update
    void Start()
    {
        towerName = "Fire Ant";
        Invoke();
        MakeSphere();
        visual.Stop();
        fireArea = GetComponent<CapsuleCollider>();
        fireArea.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Track();
        ListPrune();
    }

    public float getDamage()
    {
        return damage;
    }

    public override void Track()
    {
        if (target == null)
        {
            if (firing == true)
                firing = false;
                fireArea.enabled = false;
                visual.Stop();
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        barrelToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);


        Shoot();
    }


    public override void Shoot()
    {

        if (!firing){
            firing = true;
            fireArea.enabled = true;
            visual.Play();
        }

    }



    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}
