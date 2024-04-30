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
    public int quarterWaypointIndex;
    public int halfWaypointIndex;
    public int threeQuarterWaypointIndex;

    public int childEnemyID1;
    public int childEnemyID2;
    public int childEnemyID3;
    public AudioClip hit;
    public WaveSpawner waveSpawner;

    public float[] waypointDistances;
    public override void Start(){
        waveSpawner = FindObjectOfType<WaveSpawner>();
        if(!differentStart) target = Path.waypoints[0];
        healthbar = Instantiate(healthBarPrefab, this.transform.position, Quaternion.identity);
        healthbar.transform.SetParent(GameObject.Find("PlayerInterface").transform);
        healthbar.maxValue = maxHealth;
        healthbar.value = maxHealth;

        PlayerStatistics = FindObjectOfType<PlayerStatistics>();
        health = maxHealth;

        //increase number of enemies counter by 1
        PlayerStatistics.Instance.enemiesPresent+=13;
        //code to deal with the enemy being spawned from another enemy
        
        curSpeed = speed;
        //calculate the cumulative distance between waypoints
        float distance = 0;
        waypointDistances = new float[Path.waypoints.Length];
        waypointDistances[0] = 0;
        for (int i = 1; i < Path.waypoints.Length - 1; i++){
            distance += Vector3.Distance(Path.waypoints[i].position, Path.waypoints[i + 1].position);
            waypointDistances[i] = distance;
        }
        //record which waypoints indexes on the track correspond to the 25%, 50%, and 75% marks
        float quarter = distance / 4;
        float half = distance / 2;
        float threeQuarter = distance * 3 / 4;
        for (int i = 0; i < waypointDistances.Length; i++){
            if (waypointDistances[i] >= quarter && waypointDistances[i-1] < quarter){
                quarterWaypointIndex = i;
                Debug.Log("25% mark is at waypoint " + i);
            }
            if (waypointDistances[i] >= half && waypointDistances[i-1] < half){
                halfWaypointIndex = i;
                Debug.Log("50% mark is at waypoint " + i);
            }
            if (waypointDistances[i] >= threeQuarter && waypointDistances[i-1] < threeQuarter){
                threeQuarterWaypointIndex = i;
                Debug.Log("75% mark is at waypoint " + i);
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
        
        //Check if we've reached the 25% mark
        if (waypointindex == quarterWaypointIndex && marker1==false){
            //Debug.Log("25% mark reached");
            marker1 = true;
            waveSpawner.SpawnChildren(childEnemyID1, waypointindex, transform);
        }
        //Check if we've reached the 50% mark
        if (waypointindex == halfWaypointIndex && marker2==false){
            //Debug.Log("50% mark reached");
            marker2 = true;
            waveSpawner.SpawnChildren(childEnemyID2, waypointindex, transform);
        }
        //Check if we've reached the 75% mark
        if (waypointindex == threeQuarterWaypointIndex && marker3==false){
            //Debug.Log("75% mark reached");
            marker3 = true;
            waveSpawner.SpawnChildren(childEnemyID3, waypointindex, transform);
        }


        transform.Translate(direction.normalized * speed * slowFactor * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f){
            GetNextWaypoint();
        }
        
    }

    public override void healthCheck()
    {
        if (health <= 0)
        {
            PlayerStatistics.AddMoney(GoldWorth);
            //death.playParts(transform);
            //destroy healthbar
            Destroy(healthbar.gameObject);
            //destroy the game object
            Destroy(this.gameObject);
            //subtract present enemies count by 1
            //PlayerStatistics.Instance.enemiesPresent--;
            return;
        }
        healthbar.value = health;
        healthbar.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = sliderGradient.Evaluate(healthbar.normalizedValue);
        //Calculate the percentage of health left
        float healthPercentage = health / maxHealth;
        //Check if the enemy has reached the 25% mark
        if (healthPercentage <= 0.75 && marker1==false){
            //Debug.Log("25% health mark reached");
            marker1 = true;
            waveSpawner.SpawnChildren(childEnemyID1, waypointindex, transform);
        }
        //Check if the enemy has reached the 50% mark
        if (healthPercentage <= 0.5 && marker2==false){
            //Debug.Log("50% health mark reached");
            marker2 = true;
            waveSpawner.SpawnChildren(childEnemyID2, waypointindex, transform);
        }
        //Check if the enemy has reached the 75% mark
        if (healthPercentage <= 0.25 && marker3==false){
            //Debug.Log("75% health mark reached");
            marker3 = true;
            waveSpawner.SpawnChildren(childEnemyID3, waypointindex, transform);
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
            //Destroy(this.gameObject);
            //subtract present enemies count by 1
            PlayerStatistics.Instance.enemiesPresent--;
            return;

        }
        else
        {
            //Play hit sound
            audioSource.PlayOneShot(hit);
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

