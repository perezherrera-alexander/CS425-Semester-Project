using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class BaseTowerLogic : MonoBehaviour
{
    [Header("Tower Assembly")]
    public Transform barrelToRotate;
    public Transform locationToFireFrom;
    public GameObject projectilePrefab;
    public string enemyTag = "Enemy";
    [Header("Tower Stats")]
    public int buildCost;
    public float targettingRange = 20;
    public float fireRate = 1f;
    private float fireCountdown = 0f; // Cooldown between shots
    private bool isActive = false;
    [Header("Targeting")]
    public Transform target;
    public TargetingTypes targetingType = TargetingTypes.First;
    public List<BaseEnemyLogic> targets = new List<BaseEnemyLogic>(); 
    protected SphereCollider proximitySphere;
    [System.NonSerialized] public string towerName; // Name of the tower as displayed in the UI and used to figure out what tower a gameObject is when it's not obvious.
    // I feel like there has to be a better way to do this though
    void Start()
    {
        Invoke();
        MakeSphere();
        // I thought it was weired we had both targettingRadius and a sphere collider that seemed to be made dynamically
        // But turns out we are using targettingRadius to make a sphere of said radius so that way we can set the targetting radius in the inspector
        // Making the sphere collider protected since there is no reason to mess with it in the inspector

    }

    // Update is called once per frame
    void Update()
    {
        Track();
        ListPrune();
    }
    public virtual void Invoke()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }
    public virtual void Track()
    {
        if (target == null)
        {
            return;
        }

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



        switch (targetingType)
        {
            case TargetingTypes.First:
                FirstTargeting();
                break;

            case TargetingTypes.Last:
                LastTargeting();
                break;

            case TargetingTypes.Close:
                CloseTargeting();
                break;

            case TargetingTypes.Strong:
                StrongTargeting();
                break;

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targettingRange);
    }

    private void CloseTargeting()
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

        if (closeEnemy != null && shortestDistance <= targettingRange)
        {
            target = closeEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    private void LastTargeting()
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
        ListPrune();
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

    private void FirstTargeting()
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
                if (collider.GetComponent<BaseEnemyLogic>())
                {
                    target = colliders.First().transform;
                    
                }

            }
        }
        else
        {
            target = null;
        }*/
        ListPrune();

        if (targets.Count > 0)
        {
            target = targets.First().transform;
        }
        else
        {
            target = null;
        }
    }

    private void StrongTargeting()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float enemyDistance = Mathf.Infinity;
        float enemyHealth = 0;
        BaseEnemyLogic strongEnemy = null;

        foreach (BaseEnemyLogic enemy in targets)
        {
            if (enemy.getHealth() > enemyHealth)
            {
                enemyHealth = enemy.getHealth();
                strongEnemy = enemy;
            }
            enemyDistance = Vector3.Distance(transform.position, strongEnemy.transform.position);
        }

        if (strongEnemy != null && enemyDistance <= targettingRange)
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

    public void MakeSphere()
    {
        proximitySphere = transform.GetComponent<SphereCollider>();
        proximitySphere.radius = targettingRange * 0.5f;
    }

    public void ListPrune()
    {
        targets.RemoveAll(item => item == null);
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Enemy")
        {
            targets.Add(other.GetComponent<BaseEnemyLogic>());
            UpdateTarget();


        }
        
    }

    private void OnTriggerExit(Collider other)
    {
   
        if (other.gameObject.tag == "Enemy")
        {
            targets.Remove(other.GetComponent<BaseEnemyLogic>());
            UpdateTarget();

        }
    }
}
