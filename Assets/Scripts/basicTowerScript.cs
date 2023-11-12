using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicTowerScript : MonoBehaviour
{
    public Transform target;
    public float range = 20;

    public string enemyTag = "Enemy";

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    public Transform part;
    public int BuildCost;
    protected bool isActive = false;
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
    public virtual void Invoke()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }
    public virtual void track()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        part.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    public virtual void Shoot()
    {
        Debug.Log("shooting");
    }
    void UpdateTarget()
    {
        if(isActive == false) // If the tower is not active (hasn't been placed yet), don't do anything
        {
            return;
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject closeEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemyDistance < shortestDistance)
            {
                shortestDistance = enemyDistance;
                closeEnemy = enemy;
                closeEnemy.GetComponent<baseEnemyScript>().getHealth();
            }
        }

        if (closeEnemy != null && shortestDistance <= range)
        {
            target = closeEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void ActivateTower()
    {
        isActive = true;
    }
}
