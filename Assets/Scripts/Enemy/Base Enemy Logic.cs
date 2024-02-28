using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BaseEnemyLogic : MonoBehaviour, Effectable
{
    private StatusEffects data;
    [SerializeField] List<StatusEffects> effects;
    public Collider objectCollider;
    public GameObject ob;
    public int GoldWorth;

    private PlayerStatistics PlayerStatistics;

    public Transform model;

    public float health = 5;

    private float curSpeed;
    private bool isSlowed = false;
    // float health = 5;
    // Start is called before the first frame update
    public void Start(){

        PlayerStatistics = FindObjectOfType<PlayerStatistics>();
        target = Path.waypoints[0];
        curSpeed = speed;
    }

    public void reduceHealth(float damage)
    {
        health = (health - damage);
    }

    public float getHealth()
    {
        return health;
    }

    public void healthCheck()
    {
        if (health <= 0)
        {
            PlayerStatistics.AddMoney(GoldWorth);
            Destroy(ob);
            PlayerStatistics.Instance.AddEnemiesKilled();
            return;
        }
    }
    // Movement
    public float speed = 10f;

    public float slowFactor = 1;

    public float slowDownTimer = 0;

    //Damage over time effect
    public float dotDamage = 0;
    public float dotTimer = 0;

    public Transform target;

    public int wavepointIndex = 0;

    public virtual void Update (){
        healthCheck();


        if(effects.Count > 0)
        {
            if (effects.First() != null) handleEffect();
        }
        
        

        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * slowFactor * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f){
            GetNextWaypoint();
        }
    }

    public virtual void effect_check()
    {
        if (slowDownTimer > 0){
            slowDownTimer -= Time.deltaTime;
        } else {
            slowFactor = 1;
        }
        damageOverTime();
    }

    public virtual void damageOverTime()
    {
        if (dotTimer > 0){
            dotTimer -= Time.deltaTime;
            reduceHealth(dotDamage * Time.deltaTime);
        }
    }

    public void GetNextWaypoint(){
        if (wavepointIndex >= Path.waypoints.Length - 1){ // Enemy reaches end of path
            //decrement player health according to
            float EnemyHealth = getHealth();
            int MoraleLost = (int)EnemyHealth;
            PlayerStatistics.Instance.ReduceMorale(MoraleLost);
            Destroy(gameObject);
            PlayerStatistics.Instance.AddEnemiesKilled();
            return;
        }
        transform.LookAt(Path.waypoints[wavepointIndex]);
        wavepointIndex++;
        target = Path.waypoints[wavepointIndex];
    }
    public void knockback(Vector3 direction, float force)
    {
        wavepointIndex = wavepointIndex - 1;
        target = Path.waypoints[wavepointIndex];

    }



    public void slowDown(float slowFactor)
    {
        speed = speed * slowFactor;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CapsuleCollider>())
        {
            if(other.GetComponent<FlameTower>() != null)
            {
                float damage = other.GetComponent<FlameTower>().getDamage();
                float dmgFrame = damage * Time.deltaTime;
                reduceHealth(dmgFrame);
            }

        }
    }


    public void applyEffect(StatusEffects effect)
    {
        //this.data = effect;
        effects.Add(effect);
        Instantiate(effect.effectParticles, transform);
        effect.lifeTime = effect.initLifeTime;

        

    }


    private float currentEffectTime = 0f;
    private float lastTickTime = 0f;
    public void removeEffect(int ind)
    {
        //data = null;
        effects.RemoveAt(ind);
        if (effects.Count == 0)
        {
            currentEffectTime = 0;
            lastTickTime = 0;
        }


        
    }

    public void handleEffect()
    {


       
        currentEffectTime += Time.deltaTime;
        /*if (effects.Count > 1)
        {
            var effectCount = effects.Count;
        }
        if (currentEffectTime > effects.First().lifeTime) 
            removeEffect(0);
        if (effects.First() == null)
            return;
        if (effects.First().dotAmount != 0 && currentEffectTime > lastTickTime)
        {
            lastTickTime += effects.First().tickSpeed;
            health -= effects.First().dotAmount;
        }*/

        var j = 0;
        
        if (effects.Count > 1)
        {
            foreach (StatusEffects effect in effects)
            {
                if (effects.Count < 1)
                {
                    break;
                }
                if (currentEffectTime > effect.lifeTime)
                {
                    if (effect.Name == "Slow")
                    {
                        isSlowed = false;
                        speed = curSpeed;
                    }
                    removeEffect(j);
                    break;
                }
                if (effects.Count == 0)
                {
                    return;
                }
                if (effect.dotAmount != 0 && currentEffectTime > lastTickTime)
                {
                    lastTickTime += effect.tickSpeed;
                    health -= effect.dotAmount;
                }
                if (effect.movementPenalty != 0 && isSlowed != true)
                {
                    speed = speed * (1f - effect.movementPenalty);
                    isSlowed = true;
                }


                j++;
            }
        }
        else
        {
            if (currentEffectTime > effects.First().lifeTime)
            {
                if(effects.First().Name == "Slow")
                {
                    speed = curSpeed;
                    isSlowed = false;
                }
                removeEffect(0);
            }
                
            if (effects.Count == 0)
                return;
            if (effects.First().dotAmount != 0 && currentEffectTime > lastTickTime)
            {
                lastTickTime += effects.First().tickSpeed;
                health -= effects.First().dotAmount;
            }
            if (effects.First().movementPenalty != 0 && isSlowed != true)
            {
                speed = speed * (1 - effects.First().movementPenalty);
                isSlowed = true;
            }
        }

    }
}

