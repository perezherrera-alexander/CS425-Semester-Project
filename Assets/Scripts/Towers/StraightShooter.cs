using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShooter : BaseTowerLogic
{
    public string id;
    // Start is called before the first frame update
    void Start()
    {
        towerName = "Wasp Tower";
        Invoke();
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
        GameObject shot = (GameObject)Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
        StandardProjectile sting = shot.GetComponent<StandardProjectile>();

    }
}
