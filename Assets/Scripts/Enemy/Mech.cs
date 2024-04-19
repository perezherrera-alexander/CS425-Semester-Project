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
        healthbar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 1.5f, 0));
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
}
