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
    private bool tripleShot = false;
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
        if (tripleShot)
        {
            Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
            GameObject stung = Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
            stung.transform.Rotate(Vector3.up * 15f);
            GameObject stang = Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
            stang.transform.Rotate(Vector3.up * -15f);

        }
        else
        {
            Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
        }
        

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
        int count = 1;
        while (count <= storeTowerUpgradeData.ListOfUpgradesObtained.Count)
        {
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Wasp Upgrade 1")
            {
                tripleShot = true;
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Wasp Upgrade 2")
            {
                projectilePrefab.GetComponent<StandardProjectile>().damage = 2.5f;
                projectilePrefab.GetComponent<StandardProjectile>().moveSpeed = 70f;
            }
            count++;
        }
    }
}
