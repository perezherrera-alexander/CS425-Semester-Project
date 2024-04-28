using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DroneScript : BaseEnemyLogic
{
    public AudioClip hitSound1;
    public AudioClip hitSound2;
    public AudioClip hitSound3;

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
    public override void reduceHealth(float damage)
    {
        // audioSource.volume = (float)SettingsValues.gameVolume / 100.0f;
        health -= damage;
        if (health <= 0)
        {
            PlayerStatistics.AddMoney(GoldWorth);
            Destroy(gameObject);
            //subtract present enemies count by 1
            PlayerStatistics.Instance.enemiesPresent--;
            return;
        }
        else
        {
            //Play a random hit sound
            int random = Random.Range(1, 4);
            switch (random)
            {
                case 1:
                    audioSource.PlayOneShot(hitSound1);
                    break;
                case 2:
                    audioSource.PlayOneShot(hitSound2);
                    break;
                case 3:
                    audioSource.PlayOneShot(hitSound3);
                    break;
            }
        }
    }
}
