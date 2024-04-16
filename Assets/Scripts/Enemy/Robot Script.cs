using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : BaseEnemyLogic
{
    public bool healthTier1 = true;
    public bool healthTier2 = true;
    public bool healthTier3 = true;
    public GameObject armL;
    public GameObject armR;
    public GameObject head;

    public AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip hitSound;
    public override void healthCheck()
    {   
        if (health <= 0){
            PlayerStatistics.AddMoney(GoldWorth);
            death.playParts(transform);
            Destroy(this.gameObject);
            //subtract present enemies count by 1
            PlayerStatistics.Instance.enemiesPresent--;
            return;
        }
        //Calculate the percentage of health left
        float healthPercentage = health / maxHealth;

        if(healthPercentage < .75 && healthTier1){
            // Transform firstChild = this.gameObject.Find("Robot");
            // Transform secondChild = firstChild.Find("Root");
            // Transform thirdChild = secondChild.Find("Body");
            // Transform prey = thirdChild.Find("UpperArm_L");
            // DestroyImmediate(prey.gameObject,true);
           // DestroyImmediate(armL);
            healthTier1 = false;
            DestroyImmediate(armL);
        }
        if(healthPercentage < .50 && healthTier2){
         //   DestroyImmediate(armR);
            healthTier2 = false;
            DestroyImmediate(armR);
        }
        if(healthPercentage < .25 && healthTier3){
         //   DestroyImmediate(head);
            healthTier3 = false;
            DestroyImmediate(head);
        }
    }
}
        

//Destroy(model.GetChild(0).gameObject)