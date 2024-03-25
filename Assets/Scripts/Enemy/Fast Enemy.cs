using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class FastEnemy : BaseEnemyLogic
{
    public override void Update (){
        healthCheck();
        if(effects.Count > 0)
        {
            if (effects.First() != null) handleEffect();
        }

        Vector3 direction = target.position - transform.position;

        //Enemy speeds up depending on the damage taken
        transform.Translate(direction.normalized * speed * Time.deltaTime * Math.Min(5, 1/(health/maxHealth)), Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f){
            GetNextWaypoint();
        }
    }
    public override void healthCheck()
    {
        if (health <= 0)
        {
            PlayerStatistics.AddMoney(GoldWorth);
            Destroy(gameObject);
            Debug.Log("Fast enemy Destroyed");
                // Destroy(this.gameObject);
                // Destroy(ob);
            //subtract present enemies count by 1
            PlayerStatistics.Instance.enemiesPresent--;
            return;
        }
    }
}
