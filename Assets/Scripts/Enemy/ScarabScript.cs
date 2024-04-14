using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScarabScript : BaseEnemyLogic
{
    public int recoveryRate = 1;
    //Slider for changing material colors based on health
    
    public override void Update (){
        healthCheck();
        if(health<maxHealth)
        {
            health += Time.deltaTime * recoveryRate;
        }
        if(effects.Count > 0)
        {
            if (effects.First() != null) handleEffect();
        }
        
        Vector3 direction = target.position - transform.position;

        //Enemy speeds up depending on the damage taken
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f){
            GetNextWaypoint();
        }
    }
}
