using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class BaseTowerLogic : MonoBehaviour, Effectable
{
    [Header("Tower Assembly")]
    public Transform barrelToRotate;
    public Transform locationToFireFrom;
    public GameObject projectilePrefab;
    public string enemyTag = "Enemy";
    public float outlineThickness = 10.0f;
    [Header("Tower Stats")]
    public int buildCost;
    [TextArea]
    public string towerDescription = "Lorem ipsum dolor sit amet.";
    public Texture2D towerImage; // Image of the tower as displayed in the UI
    [Range(0f, 30f)]
    public float targettingRange = 20f;
    public float fireRate = 1f;
    public float fireCountdown = 0f; // Cooldown between shots
    public bool isActive = false;
    [Header("Targeting")]
    public Transform target;
    public TargetingTypes targetingType = TargetingTypes.First;
    public List<BaseEnemyLogic> targets = new List<BaseEnemyLogic>(); 
    protected SphereCollider proximitySphere;
    public string towerName; // Name of the tower as displayed in the UI and used to figure out what tower a gameObject is when it's not obvious.
    // I feel like there has to be a better way to do this though
    [Header("Status Effects")]
    protected StatusEffects data;

    public float curAttackSpeed;
    private float currentEffectTime = 0f;
    public bool isBuffed = false;
    public GameObject rangeFinder;
    void Start()
    {
        Invoke();
        MakeSphere();
        curAttackSpeed = fireRate;
        // I thought it was weired we had both targettingRadius and a sphere collider that seemed to be made dynamically
        // But turns out we are using targettingRadius to make a sphere of said radius so that way we can set the targetting radius in the inspector
        // Making the sphere collider protected since there is no reason to mess with it in the inspector

    }

    // Update is called once per frame
    void Update()
    {
        if(data != null)
        {
            handleEffect();
        }
        Track();
        ListPrune();
    }
    public virtual void Invoke()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    public void freeze(float time)
    {
        isActive = false;
        StartCoroutine(restart(time));

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
        //Justin C: This block of code helps the model of the tower point towards the selected target

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
    public virtual void UpdateTarget()
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

    public bool getIsBuffed()
    {
        return isBuffed;
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

    public virtual void MakeSphere()
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

    public void applyEffect(StatusEffects effect)
    {
        this.data = effect;
        Instantiate(effect.effectParticles, transform);
    }


    public void removeEffect(int ind)
    {
        if(data.Name == "AttackSpeed")
        {
            fireRate = curAttackSpeed;
            isBuffed = false;
            Destroy(transform.Find("CFX4 Aura Bubble C(Clone)").gameObject);
        }
        data = null;
        currentEffectTime = 0;
        //lastTickTime = 0;
    }

    public void handleEffect()
    {
        currentEffectTime += Time.deltaTime;

        if(currentEffectTime > data.lifeTime)
        {
            removeEffect(0);
            return;
        }

        if(data.attackSpeedIncrease != 0 && isBuffed != true)
        {
            fireRate = fireRate * (1 + data.attackSpeedIncrease);
            isBuffed = true;
        }
    }

    protected void createOutline() // This needs to be called by the Start method of any tower that wants an outline
    {
        GameObject parent = transform.parent.gameObject;
        Outline outline = parent.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineWidth = outlineThickness;
        outline.enabled = false;
    }

    IEnumerator restart(float time)
    {
        yield return new WaitForSeconds(time);
        isActive = true;
    }
}
