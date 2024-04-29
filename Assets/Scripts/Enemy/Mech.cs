using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine;

public class Mech : BaseEnemyLogic
{
    public Slider healthBarPrefab;
    Slider healthbar;
    public Gradient sliderGradient;

    //These markers are used to determine if the enemy has reached a certain point in the path OR its health is below a certain threshold
    public bool marker1 = false;
    public bool marker2 = false;
    public bool marker3 = false;
    public int quarterWaypoint = 0;
    public int halfWaypoint = 0;
    public int threeQuarterWaypoint = 0;
    public float[] waypointDistances;
    public override void Start(){
        if(!differentStart) target = Path.waypoints[0];
        healthbar = Instantiate(healthBarPrefab, this.transform.position, Quaternion.identity);
        healthbar.transform.SetParent(GameObject.Find("PlayerInterface").transform);
        healthbar.maxValue = maxHealth;
        healthbar.value = maxHealth;

        PlayerStatistics = FindObjectOfType<PlayerStatistics>();
        health = maxHealth;

        //increase number of enemies counter by 1
        PlayerStatistics.Instance.enemiesPresent++;
        //code to deal with the enemy being spawned from another enemy
        
        curSpeed = speed;
        //calculate the distance of the entire track
        waypointDistances = new float[Path.waypoints.Length];
        for (int i = 0; i < Path.waypoints.Length - 1; i++){
            waypointDistances[i] = Vector3.Distance(Path.waypoints[i].position, Path.waypoints[i + 1].position);
        }
        waypointDistances[Path.waypoints.Length - 1] = Vector3.Distance(Path.waypoints[Path.waypoints.Length - 1].position, Path.waypoints[0].position);
        //record which waypoints indexes on the track correspond to the 25%, 50%, and 75% marks
        marker1 = false;
        marker2 = false;
        marker3 = false;
        for (int i = 0; i < Path.waypoints.Length; i++){
            if (marker1 == false && waypointDistances[i] >= 0.25f * waypointDistances.Sum()){
                marker1 = true;
                waypointindex = i;
            }
            if (marker2 == false && waypointDistances[i] >= 0.50f * waypointDistances.Sum()){
                marker2 = true;
            }
            if (marker3 == false && waypointDistances[i] >= 0.75f * waypointDistances.Sum()){
                marker3 = true;
            }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        healthCheck();
        healthbar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, -2f, 0));

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

    public override void healthCheck()
    {
        healthbar.value = health;
        healthbar.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = sliderGradient.Evaluate(healthbar.normalizedValue);
        if (health <= 0)
        {
            PlayerStatistics.AddMoney(GoldWorth);
            death.playParts(transform);
            //destroy healthbar
            Destroy(healthbar.gameObject);
            //destroy the game object
            Destroy(this.gameObject);
            //subtract present enemies count by 1
            PlayerStatistics.Instance.enemiesPresent--;
            return;
        }
    }
    public override void reduceHealth(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            //audioSource.PlayOneShot(deathSound);
            PlayerStatistics.AddMoney(GoldWorth);
            //death.playParts(transform);
            Destroy(this.gameObject);
            //subtract present enemies count by 1
            PlayerStatistics.Instance.enemiesPresent--;
            return;

        }
        else
        {
            //int rand = Random.Range(1, 5);
        }
    }
    public override void GetNextWaypoint(){
        if (waypointindex >= Path.waypoints.Length){ // Enemy reaches end of path
            //decrement player health according to
            float EnemyHealth = getHealth();
            if(EnemyHealth < 1f)
            {
                EnemyHealth = 1f;
            }
            int MoraleLost = (int)EnemyHealth;
            PlayerStatistics.Instance.ReduceMorale(MoraleLost);
            //subtract present enemies count by 1
            PlayerStatistics.Instance.enemiesPresent--;
            Destroy(healthbar.gameObject);
            Destroy(gameObject);
            return;
        }
        transform.LookAt(Path.waypoints[waypointindex]);
        target = Path.waypoints[waypointindex];
        waypointindex++;
    }
    public override void knockback(int knockbackForce){
        //transform.Translate(direction.normalized * force * Time.deltaTime, Space.World);
    }
    public override void stun(float stunTime){
        stunTimer = stunTime;
    }
}

