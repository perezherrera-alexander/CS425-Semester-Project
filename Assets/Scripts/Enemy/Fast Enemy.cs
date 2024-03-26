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
    // public override void healthCheck()
    // {
    //     if (health <= 0)
    //     {
    //         PlayerStatistics.AddMoney(GoldWorth);
    //         Destroy(gameObject);
    //         Debug.Log("Fast enemy Destroyed");
    //             // Destroy(this.gameObject);
    //             // Destroy(ob);
    //         //subtract present enemies count by 1
    //         PlayerStatistics.Instance.enemiesPresent--;
    //         return;
    //     }
    // }
    // public virtual void handleEffect()
    // {


       
    //     currentEffectTime += Time.deltaTime;
    //     /*if (effects.Count > 1)
    //     {
    //         var effectCount = effects.Count;
    //     }
    //     if (currentEffectTime > effects.First().lifeTime) 
    //         removeEffect(0);
    //     if (effects.First() == null)
    //         return;
    //     if (effects.First().dotAmount != 0 && currentEffectTime > lastTickTime)
    //     {
    //         lastTickTime += effects.First().tickSpeed;
    //         health -= effects.First().dotAmount;
    //     }*/

    //     var j = 0;
        
    //     if (effects.Count > 1)
    //     {
    //         foreach (StatusEffects effect in effects)
    //         {
    //             if (effects.Count < 1)
    //             {
    //                 break;
    //             }
    //             // if (currentEffectTime > effect.lifeTime)
    //             // {
    //             //     // if (effect.Name == "Slow")
    //             //     // {
    //             //     //     isSlowed = false;
    //             //     //     speed = curSpeed;
    //             //     // }
    //             //     // removeEffect(j);
    //             //     // break;
    //             // }
    //             if (effects.Count == 0)
    //             {
    //                 return;
    //             }
    //             if (effect.dotAmount != 0 && currentEffectTime > lastTickTime)
    //             {
    //                 lastTickTime += effect.tickSpeed;
    //                 health -= effect.dotAmount;
    //             }
    //             // if (effect.movementPenalty != 0 && isSlowed != true)
    //             // {
    //             //     speed = speed * (1f - effect.movementPenalty);
    //             //     isSlowed = true;
    //             // }


    //             j++;
    //         }
    //     }
    //     else
    //     {
    //         if (currentEffectTime > effects.First().lifeTime)
    //         {
    //             // if(effects.First().Name == "Slow")
    //             // {
    //             //     speed = curSpeed;
    //             //     isSlowed = false;
    //             // }
    //             removeEffect(0);
    //         }
                
    //         if (effects.Count == 0)
    //             return;
    //         if (effects.First().dotAmount != 0 && currentEffectTime > lastTickTime)
    //         {
    //             lastTickTime += effects.First().tickSpeed;
    //             health -= effects.First().dotAmount;
    //         }
    //         // if (effects.First().movementPenalty != 0 && isSlowed != true)
    //         // {
    //         //     speed = speed * (1 - effects.First().movementPenalty);
    //         //     isSlowed = true;
    //         // }
    //     }

    // }
}
