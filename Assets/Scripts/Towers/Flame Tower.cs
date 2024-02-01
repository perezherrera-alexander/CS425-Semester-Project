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
    private float damage = 0.0125f;

    // Start is called before the first frame update
    void Start()
    {
        towerName = "Fire Ant";
        Invoke();
        MakeSphere();
        visual.Stop();
        fireArea = GetComponent<CapsuleCollider>();
        isActive = true;
        fireArea.enabled = false;

    }
    public override void MakeSphere()
    {
        proximitySphere = transform.GetComponentInChildren<SphereCollider>();
        proximitySphere.radius = targettingRange * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Track();
        ListPrune();
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
        float dmgPerFrame = damage * Time.deltaTime;
        foreach (BaseEnemyLogic enemy in targets)
        {
            enemy.reduceHealth(dmgPerFrame);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            
            targets.Add(other.GetComponent<BaseEnemyLogic>());
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            
            targets.Remove(other.GetComponent<BaseEnemyLogic>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            targets.Add(other.GetComponent<BaseEnemyLogic>());
        }
    }


    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}
