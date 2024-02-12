using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseEnemyLogic : MonoBehaviour
{
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

    public int wavepointIndex = 0;

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
        // public void slowDown(float slow,float slowTimer)
        // {
        //     slowFactor = slow;
        //     slowDownTimer = slowTimer;
        // }
}