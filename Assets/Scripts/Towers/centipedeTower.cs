using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centipedeTower : BaseTowerLogic
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    public int waypointIndex = 0;
    private Animator animate;
    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        enemyTag = "waypoint";
        towerName = "Centipede Mother";
        //Invoke();
        //MakeSphere();
        curAttackSpeed = fireRate;
        for (int i = 0; i < Path.waypoints.Length; i++)
        {
            if (Vector3.Distance(transform.position, Path.waypoints[i].position) < Vector3.Distance(transform.position, Path.waypoints[waypointIndex].position))
            {
                waypointIndex = i;
            }
            else
            {
                waypointIndex++;
            }
        }
        target = Path.waypoints[waypointIndex - 1];
        fireRate = 0.3f;
        animate = GetComponentInChildren<Animator>();
        AddUpgradeEffects();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive == false)
        {
            return;
        }
        if (data != null)
        {
            handleEffect();
        }
        Track();
        //ListPrune();
    }

    public override void Track()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        barrelToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    public override void Shoot()
    {
        animate.SetTrigger("attack");
        GameObject stinger = Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
        CentipedeSeeker seek = stinger.GetComponent<CentipedeSeeker>();
    }

    public void AddUpgradeEffects()
    {
        int count = 1;
        while (count <= storeTowerUpgradeData.ListOfUpgradesObtained.Count)
        {
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Centipede Upgrade 1")
            {
                projectilePrefab.GetComponent<CentipedeSeeker>().lifeTime = 20f;
                fireRate = 0.6f;
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count - 1] == "Centipede Upgrade 2")
            {
                projectilePrefab.GetComponent<CentipedeSeeker>().damage = 3;
                projectilePrefab.GetComponent<CentipedeSeeker>().upgraded = true;
            }
            count++;
        }
    }
}
