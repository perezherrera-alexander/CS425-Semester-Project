using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.Playables.AnimationPlayableUtilities;

public class StraightShooter : BaseTowerLogic
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    public string id;
    private Animator animate;
    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Wasp Tower";
        Invoke();
        animate = GetComponentInChildren<Animator>();
        MakeSphere();
        curAttackSpeed = fireRate;
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

    public override void Shoot()
    {
        animate.SetTrigger("attack");
        GameObject shot = (GameObject)Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
        
        StandardProjectile sting = shot.GetComponent<StandardProjectile>();
 

    }

    public override void MakeSphere()
    {
        proximitySphere = transform.GetComponent<SphereCollider>();
       proximitySphere.radius = targettingRange * 3f;
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
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count] == "Wasp Upgrade 1")
            {
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count] == "Wasp Upgrade 2")
            {
            }
            count++;
        }
    }
}
