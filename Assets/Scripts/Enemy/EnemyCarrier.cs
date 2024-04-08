using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarrier : BaseEnemyLogic
{
    //set enemy to Robot1
    public GameObject enemy;
    public override void healthCheck()
    {
        if (health <= 0)
        {
            //PlayerStatistics.AddMoney(GoldWorth);

            // If the enemy is a carrier, spawn 4 Robot1 entities headed towards the current waypoint
            //enemy.GetComponent<EnemyMovement>().waypointindex = waypointindex;
            for (int i = 0; i < 4; i++)
            {
                GameObject troop = Instantiate(enemy, new Vector3(transform.position.x + i * 1.0f, transform.position.y, transform.position.z + i * 1.0f), Quaternion.identity);
                troop.GetComponent<BaseEnemyLogic>().differentStart = true;
                troop.GetComponent<BaseEnemyLogic>().waypointindex = waypointindex;
                //change target aswell
                troop.GetComponent<BaseEnemyLogic>().target = Path.waypoints[waypointindex];
            }
            //subtract present enemies count by 1
            death.playParts(transform);
            PlayerStatistics.Instance.enemiesPresent--;
            Destroy(ob);

            //PlayerStatistics.Instance.AddEnemiesKilled();
            

            return;
        }
    }
    public override void knockback(int knockbackforce)
    {
        //base.knockback();
    }
}