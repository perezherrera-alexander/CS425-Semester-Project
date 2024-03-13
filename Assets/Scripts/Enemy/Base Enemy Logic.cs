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

    public PlayerStatistics PlayerStatistics;

    public Transform model;

    public float maxHealth = 5;

    public float health = 5;

    private float curSpeed;
    private bool isSlowed = false;
    // float health = 5;
    // Start is called before the first frame update
    public void Start(){

        PlayerStatistics = FindObjectOfType<PlayerStatistics>();
        maxHealth = health;

        //increase number of enemies counter by 1
        PlayerStatistics.Instance.enemiesPresent++;
        //code to deal with the enemy being spawned from another enemy
        if(!differentStart) target = Path.waypoints[0];
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

    public virtual void healthCheck()
    {
        if (health <= 0)
        {
            PlayerStatistics.AddMoney(GoldWorth);
            Destroy(ob);
            //subtract present enemies count by 1
            PlayerStatistics.Instance.enemiesPresent--;
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

    public int waypointindex = 0;

    public bool differentStart = false;

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

    public virtual void GetNextWaypoint(){
        if (waypointindex >= Path.waypoints.Length){ // Enemy reaches end of path
            //decrement player health according to
            float EnemyHealth = getHealth();
            int MoraleLost = (int)EnemyHealth;
            PlayerStatistics.Instance.ReduceMorale(MoraleLost);
            //subtract present enemies count by 1
            PlayerStatistics.Instance.enemiesPresent--;
            Destroy(gameObject);
            return;
        }
        transform.LookAt(Path.waypoints[waypointindex]);
        target = Path.waypoints[waypointindex];
        waypointindex++;
    }
    public virtual void knockback(int knockbackForce){

        //Calculate the distance between the current position and the next waypoint

        float travelDistance = 0;
        //make waypointLengths an array of floats with the length of the number of waypoints
        float[] waypointLengths = new float[waypointindex-1];
        waypointLengths[0] = 0;
        for (int i = 0; i < waypointindex-1; i++){
            travelDistance += Vector3.Distance(Path.waypoints[i].position, Path.waypoints[i + 1].position);
            waypointLengths[i] = travelDistance;
        }
        travelDistance += Vector3.Distance(Path.waypoints[waypointindex].position, transform.position);
        float knockbackLength = knockbackForce * travelDistance * .01f;

        Debug.Log("Distance: " + travelDistance);
        Debug.Log("Knockback length: " + knockbackLength);
        
        //index of the last waypoint that holds a length less than the knockback length
        int newIndex = 0;
        for (int i = 0; i < waypointLengths.Length; i++){
            if (knockbackLength < waypointLengths[i]){
                newIndex = i-1;
                waypointindex = i;
                target = Path.waypoints[newIndex];
                //Debug.Log("New Index: " + newIndex);
                break;
            }
        }
        // Debug.Log("New Index: " + newIndex);
        // Debug.Log("Waypoint index: " + waypointindex);
        // Debug.Log("Length of new knockback length: " + waypointLengths[newIndex]);
        
        
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

