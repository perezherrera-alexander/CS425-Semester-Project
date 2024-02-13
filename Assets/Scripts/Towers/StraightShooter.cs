using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
