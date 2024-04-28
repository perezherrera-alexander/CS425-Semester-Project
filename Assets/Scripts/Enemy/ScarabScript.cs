using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScarabScript : BaseEnemyLogic
{
    public GameObject body;
    public GameObject head;
    public Color[] colors;
    //audio stuff
    public AudioSource audioSource;
    public AudioClip hitSound1;
    public AudioClip hitSound2;
    public AudioClip hitSound3;

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
    public override void reduceHealth(float damage)
    {
        audioSource.volume = (float)SettingsValues.gameVolume / 100.0f;
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
