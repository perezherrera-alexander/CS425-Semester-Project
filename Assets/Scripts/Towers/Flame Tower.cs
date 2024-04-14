using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FlameTower : BaseTowerLogic
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    public string id;
    private bool firing = false;
    public ParticleSystem visual;
    public CapsuleCollider fireArea;
    private float damage = 0.333f;
    public Transform tank;
    public GameObject flametan;

    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Fire Ant";
        Invoke();
        MakeSphere();
        visual.Pause();
        fireArea = GetComponent<CapsuleCollider>();
        fireArea.enabled = false;
        AddUpgradeEffects();
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

    public override void MakeSphere()
    {
        proximitySphere = GetComponent<SphereCollider>();
        proximitySphere.radius = 7.81f;
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
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Flame Ant Upgrade 1")
            {
                fireArea.radius = 4.32f;
                fireArea.height = 13.14f;
                fireArea.center = new Vector3(0f, 0.71f, 8.63f);
                visual.transform.localScale = new Vector3(7.38f, 7.38f, 3.5f);
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Flame Ant Upgrade 2")
            {
                damage = 1.5f;
                Instantiate(flametan, tank);
            }
            count++;
        }
    }
}
