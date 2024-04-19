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
        int stun = 1;
        if (stunTimer > 0){
            stun = 0;
            stunTimer -= Time.deltaTime;
        }
        transform.Translate(direction.normalized * speed * stun * slowFactor * Time.deltaTime, Space.World);

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
            //PlayerStatistics.AddMoney(GoldWorth);
            //death.playParts(transform);
            //Destroy(this.gameObject);
            //subtract present enemies count by 1
            //PlayerStatistics.Instance.enemiesPresent--;
            //return;

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
}
