using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpiderScript : BaseTowerLogic
{
    public GameObject secondRange;
    public StoreTowerUpgradeData storeTowerUpgradeData;
    private Animator animate;
    public string id;
    //private Animator animate;
    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Spider Tower";
        Invoke();
        animate = GetComponentInChildren<Animator>();
        MakeSphere();
        projectilePrefab.GetComponent<spiderweb>().data.movementPenalty = 0.25f;
        AddUpgradeEffects();
    }

    // Update is called once per frame
    void Update()
    {

        if (isActive)
        {
            if(data != null)
            {
                handleEffect();
            }
            Track();
            ListPrune();
        }
   
    }

    public override void Shoot()
    {
        animate.ResetTrigger("Attack");
        GameObject shot = (GameObject)Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
        
        StandardProjectile sting = shot.GetComponent<StandardProjectile>();
        animate.SetTrigger("Attack");

    }

    public override void MakeSphere()
    {
        proximitySphere = transform.GetComponent<SphereCollider>();
        proximitySphere.radius = 41.29f;
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
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Spider Upgrade 1")
            {
                projectilePrefab.GetComponent<spiderweb>().data.movementPenalty = 0.70f;
                projectilePrefab.GetComponent<spiderweb>().damage = 1f;
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Spider Upgrade 2")
            {
                targettingRange = 20f;
                proximitySphere.radius = 59.88f;
                rangeFinder.SetActive(false);
                rangeFinder = secondRange;
            }
            count++;
        }
    }
}
