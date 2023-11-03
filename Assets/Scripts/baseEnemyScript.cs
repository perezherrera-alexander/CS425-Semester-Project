using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class baseEnemyScript : MonoBehaviour
{
    public Collider objectCollider;
    public GameObject ob;
    public int GoldWorth;

    private PlayerStats PlayerStatistics;

    public Transform model;

    public float health = 5;

    // float health = 5;
    // Start is called before the first frame update
    void Start(){

        PlayerStatistics = FindObjectOfType<PlayerStats>();
        target = Path.waypoints[0];

    }

    public void reduceHealth(float damage)
    {
        health = (health - damage);
    }

    public float getHealth()
    {
        return health;
    }

    public void healthCheck()
    {
        if (health <= 0)
        {
            PlayerStatistics.AddMoney(GoldWorth);
            Destroy(ob);
            return;
        }
    }
    // Movement
    public float speed = 10f;

    public Transform target;

    public int wavepointIndex = 0;

    public void Update () {
        healthCheck();
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f){
            GetNextWaypoint();
        }
    }

    public void GetNextWaypoint(){
        if (wavepointIndex >= Path.waypoints.Length - 1){
            //decrement player health according to
            Destroy(gameObject);
            return;
        }
        transform.LookAt(Path.waypoints[wavepointIndex], Vector3.left);
        wavepointIndex++;
        target = Path.waypoints[wavepointIndex];
    }
}
