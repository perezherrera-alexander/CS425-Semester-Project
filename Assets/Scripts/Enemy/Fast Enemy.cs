using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FastEnemy : BaseEnemyLogic
{
    public override void Update(){
        healthCheck();
        //effect_check();
        if(effects.Count > 0)
        {
            if (effects.First() != null) handleEffect();
        }
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * 2 * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f){
            GetNextWaypoint();
        }
    }
}
