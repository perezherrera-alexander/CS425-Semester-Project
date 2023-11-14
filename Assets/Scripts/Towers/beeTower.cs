using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beeTower : basicTowerScript
{
    public static string Name = "Bee Tower";
    // Start is called before the first frame update

    void Start()
    {
        Invoke();
        targeting = "close";
        makeSphere();
    }

    // Update is called once per frame
    void Update()
    {
        
        track();
        listPrune();
    }

    public override void Shoot()
    {
        GameObject stinger = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        stingerScript sting = stinger.GetComponent<stingerScript>();

        if (sting != null)
        {
            sting.Seek(target);
        }
        else if (sting.exists && target == null)
        {

        }
    }
}
