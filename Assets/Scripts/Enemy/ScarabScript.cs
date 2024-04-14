using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScarabScript : BaseEnemyLogic
{
    public GameObject body;
    public GameObject head;
    public Color[] colors;

    public int recoveryRate = 1;
    
    public override void Update (){
        healthCheck();
        if(health<maxHealth)
        {
            health += Time.deltaTime * recoveryRate;
            body.GetComponent<Renderer>().materials[1].color = Color.Lerp(colors[0],colors[1],health/maxHealth);
            head.GetComponent<Renderer>().materials[1].color = Color.Lerp(colors[0],colors[1],health/maxHealth);

        }else{
            health = maxHealth;
            body.GetComponent<Renderer>().materials[1].color = colors[1];
            head.GetComponent<Renderer>().materials[1].color = colors[1];
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
