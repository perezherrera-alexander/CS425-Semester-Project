using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    private bool isActive = false;

    public string targeting = "first";
    public List<baseEnemyScript> targets = new List<baseEnemyScript>(); 

    public SphereCollider radius;

    [System.NonSerialized] public string towerName;

    // Start is called before the first frame update
    void Start()
    {
        Invoke();
        makeSphere();

    }

    // Update is called once per frame
    void Update()
    {
        track();
        listPrune();

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
    public void UpdateTarget()
    {
        if(isActive == false) // If the tower is not active (hasn't been placed yet), don't do anything
        {
            return;
        }



        switch (targeting)
        {
            case "first":
                firstTargeting();
                break;

            case "last":
                lastTargeting();
                break;

            case "close":
                closeTargeting();
                break;

            case "strong":
                strongTargeting();
                break;

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void changingTargeting(string targets)
    {
        targeting = targets;
    }
    private void closeTargeting()
    {
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

    private void lastTargeting()
    {
        /*GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject lastEnemy = null;

      
        int length = enemies.Length;
        lastEnemy = enemies[length - 1];
        shortestDistance = Vector3.Distance(transform.position, lastEnemy.transform.position);
        

        if (lastEnemy != null && shortestDistance <= range)
        {
            target = lastEnemy.transform;
        }
        else
        {
            target = null;
        }*/
        listPrune();
        if (targets.Count > 0)
        {
            if (targets.Last() != null)
            {
                target = targets.Last().transform;
            }

        }
        else
        {
            target = null;
        }
    }

    private void firstTargeting()
    {
        /*GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject firstEnemy = null;


       
        firstEnemy = enemies[0];
        shortestDistance = Vector3.Distance(transform.position, firstEnemy.transform.position);
        

        if (firstEnemy != null && shortestDistance <= range)
        {
            target = firstEnemy.transform;
        }
        else
        {
            target = null;
        }*/

        /*Collider[] colliders = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("enemy"));
        if (colliders.Length > 0) {
           
            foreach (Collider collider in colliders)
            {
                if (collider.GetComponent<baseEnemyScript>())
                {
                    target = colliders.First().transform;
                    
                }

            }
        }
        else
        {
            target = null;
        }*/
        listPrune();
        if (targets.Count > 0)
        {
            if(targets.First() != null)
            {
                target = targets.First().transform;
            }
            
        }
        else
        {
            target = null;
        }
    }

    private void strongTargeting()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float enemyDistance = Mathf.Infinity;
        float enemyHealth = 0;
        baseEnemyScript strongEnemy = null;

        foreach (baseEnemyScript enemy in targets)
        {
            if (enemy.getHealth() > enemyHealth)
            {
                enemyHealth = enemy.getHealth();
                strongEnemy = enemy;
            }
            enemyDistance = Vector3.Distance(transform.position, strongEnemy.transform.position);
        }

        if (strongEnemy != null && enemyDistance <= range)
        {
            target = strongEnemy.transform;
        }
        else
        {
            enemyHealth = 0;
            target = null;
        }
    }
    public void ActivateTower()
    {
        isActive = true;
    }

    public void makeSphere()
    {
        radius = transform.GetComponent<SphereCollider>();
        radius.radius = range * 0.5f;
    }

    public void listPrune()
    {
        targets.RemoveAll(item => item == null);
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Enemy")
        {
            targets.Insert(0,other.GetComponent<baseEnemyScript>());
            UpdateTarget();


        }
        
    }

    private void OnTriggerExit(Collider other)
    {
   
        if (other.gameObject.tag == "Enemy")
        {
            targets.Remove(other.GetComponent<baseEnemyScript>());
            UpdateTarget();

        }
    }
}
