using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.Playables.AnimationPlayableUtilities;

public class StraightShooter : BaseTowerLogic
{
    public string id;
    private Animator animate;
    // Start is called before the first frame update
    void Start()
    {
        towerName = "Wasp Tower";
        Invoke();
        animate = GetComponentInChildren<Animator>();
        MakeSphere();
        curAttackSpeed = fireRate;
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
        animate.SetBool("Attacking", true);
        GameObject shot = (GameObject)Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
        
        StandardProjectile sting = shot.GetComponent<StandardProjectile>();
        animate.SetBool("Attacking", false);

    }

    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}
