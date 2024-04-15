using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FridgeBot : BaseEnemyLogic
{
    public string enemyTag;
    public float targettingRange = 20f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public Transform towerTarget;
    public Transform locationToFireFrom;
    public GameObject projectilePrefab;
    public override void Update()
    {
        healthCheck();

        if (effects.Count > 0)
        {
            if (effects.First() != null) handleEffect();
        }
        Vector3 direction = target.position - transform.position;
        int stun = 1;
        if (stunTimer > 0)
        {
            stun = 0;
            stunTimer -= Time.deltaTime;
        }
        transform.Translate(direction.normalized * speed * stun * slowFactor * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
        Track();
        if(fireCountdown <= 0f && towerTarget != null)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, locationToFireFrom.position, locationToFireFrom.rotation);
        fridgeProjectile freeze = projectile.GetComponent<fridgeProjectile>();
        if (freeze != null)
        {
            freeze.Seek(towerTarget);
        }
    }

    public void Track()
    {
        BaseTowerLogic[] towers = GameObject.FindObjectsOfType<BaseTowerLogic>();
        float shortestDistance = Mathf.Infinity;
        BaseTowerLogic closeEnemy = null;

        foreach (BaseTowerLogic enemy in towers)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemyDistance < shortestDistance)
            {
                shortestDistance = enemyDistance;
                closeEnemy = enemy;
            }
        }

        if (closeEnemy != null && shortestDistance <= targettingRange)
        {
            towerTarget = closeEnemy.transform;
        }
        else
        {
            towerTarget = null;
        }
    }


}
