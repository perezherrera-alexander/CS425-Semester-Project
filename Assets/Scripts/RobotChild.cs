using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotChild : RobotScript
{
    public override void Start(){

        PlayerStatistics = FindObjectOfType<PlayerStatistics>();
        maxHealth = health;

        //increase number of enemies counter by 1
        //PlayerStatistics.Instance.enemiesPresent++;
        //code to deal with the enemy being spawned from another enemy
        if(!differentStart) target = Path.waypoints[0];
        curSpeed = speed;
        audioSource.volume = 1;
    }
}
