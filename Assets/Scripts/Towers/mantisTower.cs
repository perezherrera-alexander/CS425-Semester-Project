using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class mantisTower : BaseTowerLogic
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    private Animator animate;
    public string id;
    public float sliceDmg = 0.75f;
    public ParticleSystem holy;
    public Transform crown;
    public Material upgrade;
    public List<Material> materials;
    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Mantis Warrior";
        Invoke();
        MakeSphere();
        fireRate = 3f;
        curAttackSpeed = fireRate;
        transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().GetMaterials(materials);
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

    public override void MakeSphere()
    {
        proximitySphere = GetComponent<SphereCollider>();
        proximitySphere.radius = 9.88f;
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
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Mantis Upgrade 1")
            {
                fireRate = 5f;
                Instantiate(holy, crown);
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Mantis Upgrade 2")
            {
                targettingRange = 18f;
                proximitySphere.radius = 17.99f;
                materials[0] = upgrade;
                transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().materials = materials.ToArray();
            }
            count++;
        }
    }
}
