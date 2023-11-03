using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mortarTower : basicTowerScript
{
    public int BuildCost = 2;
    // Start is called before the first frame update

    void Start()
    {
        Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        track();
    }

    public override void Shoot()
    {
        GameObject ball1 = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        mortarProjectile ball = ball1.GetComponent<mortarProjectile>();

        if (ball != null)
        {
            ball.Seek(target);
        }
    }
}
