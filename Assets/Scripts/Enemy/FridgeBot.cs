using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FridgeBot : BaseEnemyLogic
{
    public string enemyTag;
    public float targettingRange = 20f;
    public float fireRate = 0.5f;
    public float fireCountdown = 0f;
    public Transform towerTarget;
    public Transform locationToFireFrom;
    public GameObject projectilePrefab;
    public ParticleSystem charge;
    public ParticleSystem fire;
    public ParticleSystem control;
    public AudioClip hitSound1;
    public AudioClip hitSound2;
    public AudioClip hitSound3;

    public override void Start()
    {
        control = Instantiate(charge, locationToFireFrom);

        PlayerStatistics = FindObjectOfType<PlayerStatistics>();
        maxHealth = health;

        //increase number of enemies counter by 1
        PlayerStatistics.Instance.enemiesPresent++;
        //code to deal with the enemy being spawned from another enemy
        if (!differentStart) target = Path.waypoints[0];
        curSpeed = speed;
    }
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
            control.Stop();
            Instantiate(fire, locationToFireFrom);
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        control.Play();
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
                if(enemy.isActive == true)
                {
                    shortestDistance = enemyDistance;
                    closeEnemy = enemy;
                }

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

    public override void beetleDamage(float damage)
    {
        health = (health - damage);
        Instantiate(particles, transform);
        //Play a random hit sound every 2 seconds
        
        
        if(Time.time % beetleInterval == 0)
        {   
            int random = Random.Range(1, 4);
            switch (random){
                case 1:
                    audioSource.PlayOneShot(hitSound1);
                    break;
                case 2:
                    audioSource.PlayOneShot(hitSound2);
                    break;
                case 3:
                    audioSource.PlayOneShot(hitSound3);
                    break;
            }
        }
    }
}
