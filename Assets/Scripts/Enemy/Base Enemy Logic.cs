using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseEnemyLogic : MonoBehaviour, Effectable
{
    private StatusEffects data;
    public Collider objectCollider;
    public GameObject ob;
    public int GoldWorth;

    private PlayerStatistics PlayerStatistics;

    public Transform model;

    public float health = 5;

    // float health = 5;
    // Start is called before the first frame update
    public void Start(){

        PlayerStatistics = FindObjectOfType<PlayerStatistics>();
        target = Path.waypoints[0];

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

    public int waypointindex = 0;

    public virtual void Update (){
        healthCheck();
        effect_check();

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
        if (waypointindex >= Path.waypoints.Length - 1){ // Enemy reaches end of path
            //decrement player health according to
            float EnemyHealth = getHealth();
            int MoraleLost = (int)EnemyHealth;
            PlayerStatistics.Instance.ReduceMorale(MoraleLost);
            Destroy(gameObject);
            PlayerStatistics.Instance.AddEnemiesKilled();
            return;
        }
        transform.LookAt(Path.waypoints[waypointindex]);
        waypointindex++;
        target = Path.waypoints[waypointindex];
    }
    public void knockback(int knockbackForce){

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
        Debug.Log("New Index: " + newIndex);
        Debug.Log("Waypoint index: " + waypointindex);
        Debug.Log("Length of new knockback length: " + waypointLengths[newIndex]);
        
        
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
        this.data = effect;
    }

    public void removeEffect()
    {
        data = null;
    }
}

