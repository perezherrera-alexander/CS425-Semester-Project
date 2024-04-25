using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarrier : BaseEnemyLogic
{
    //set enemy to Robot1
    public GameObject enemy;
    public ParticleSystem smoke;

    public override void Start(){

        PlayerStatistics = FindObjectOfType<PlayerStatistics>();
        maxHealth = health;

        //increase number of enemies counter by 1
        PlayerStatistics.Instance.enemiesPresent += 1;
        //code to deal with the enemy being spawned from another enemy
        if(!differentStart) target = Path.waypoints[0];
        curSpeed = speed;
    }
    public override void healthCheck()
    {
        if (health <= 0)
        {
            //PlayerStatistics.AddMoney(GoldWorth);

            // If the enemy is a carrier, spawn 4 Robot1 entities headed towards the current waypoint
            //enemy.GetComponent<EnemyMovement>().waypointindex = waypointindex;
            //PlayerStatistics.Instance.enemiesPresent += 4;

            // for (int i = 0; i < 4; i++)
            // {
            //     GameObject troop = Instantiate(enemy, new Vector3(transform.position.x + i * 1.0f, transform.position.y, transform.position.z + i * 1.0f), Quaternion.identity);
            //     troop.GetComponent<BaseEnemyLogic>().differentStart = true;
            //     troop.GetComponent<BaseEnemyLogic>().waypointindex = waypointindex;
            //     //change target aswell
            //     troop.GetComponent<BaseEnemyLogic>().target = Path.waypoints[waypointindex];
            // }

            //Create 4 troops one by one
            GameObject troop1 = Instantiate(enemy, new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z + 1.0f), Quaternion.identity);
            troop1.GetComponent<BaseEnemyLogic>().differentStart = true;
            troop1.GetComponent<BaseEnemyLogic>().waypointindex = waypointindex;
            troop1.GetComponent<BaseEnemyLogic>().target = Path.waypoints[waypointindex];

            GameObject troop2 = Instantiate(enemy, new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z - 1.0f), Quaternion.identity);
            troop2.GetComponent<BaseEnemyLogic>().differentStart = true;
            troop2.GetComponent<BaseEnemyLogic>().waypointindex = waypointindex;
            troop2.GetComponent<BaseEnemyLogic>().target = Path.waypoints[waypointindex];

            GameObject troop3 = Instantiate(enemy, new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z - 1.0f), Quaternion.identity);
            troop3.GetComponent<BaseEnemyLogic>().differentStart = true;
            troop3.GetComponent<BaseEnemyLogic>().waypointindex = waypointindex;
            troop3.GetComponent<BaseEnemyLogic>().target = Path.waypoints[waypointindex];

            GameObject troop4 = Instantiate(enemy, new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z + 1.0f), Quaternion.identity);
            troop4.GetComponent<BaseEnemyLogic>().differentStart = true;
            troop4.GetComponent<BaseEnemyLogic>().waypointindex = waypointindex;
            troop4.GetComponent<BaseEnemyLogic>().target = Path.waypoints[waypointindex];

            
            
            //subtract present enemies count by 1
            death.playParts(transform);
            PlayerStatistics.Instance.enemiesPresent--;
            Destroy(this.gameObject);

            //PlayerStatistics.Instance.AddEnemiesKilled();
            
            return;
        }
        //Start progressively playing the smoke effect as the enemy's health decreases
        //if(health <= some value)
    }
    public override void knockback(int knockbackforce)
    {
        //base.knockback();
    }

    public override void stun(float stunTime)
    {
        
    }
}