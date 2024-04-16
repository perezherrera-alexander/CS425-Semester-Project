using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DroneScript : BaseEnemyLogic
{
    public override void GetNextWaypoint(){
        if (waypointindex >= Path.waypoints.Length){ // Enemy reaches end of path
            //decrement player health according to
            float EnemyHealth = getHealth();
            if (EnemyHealth < 1f)
            {
                EnemyHealth = 1f;
            }
            int MoraleLost = (int)EnemyHealth;
            PlayerStatistics.Instance.ReduceMorale(MoraleLost);
            //subtract present enemies count by 1
            PlayerStatistics.Instance.enemiesPresent--;
            Destroy(gameObject);
            return;
        }
        int length = Path.waypoints.Length;
        int rand;
        if (length <= 10)
        {
            rand = Random.Range(3, 4);
        }
        else
        {
            rand = Random.Range(8, 10);
        }

        if (rand + waypointindex >= Path.waypoints.Length)
        {
            waypointindex = Path.waypoints.Length;
        }
        else
        {
            waypointindex += rand;
        }

        transform.LookAt(Path.waypoints[waypointindex - 1]);
        target = Path.waypoints[waypointindex - 1];



    }
}
