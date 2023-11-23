using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beeTower : basicTowerScript
{
    // Property to override the TowerName in the base class
    public override string TowerName => "Bee Tower";

    // Make the prefab reference static
    public static beeTower Instance;




    private void Awake()
    {
        Instance = this;
        base.Awake();  // Call the base class's Awake method
        Debug.Log(TowerName);
        base.ActivateTower();
    }



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
