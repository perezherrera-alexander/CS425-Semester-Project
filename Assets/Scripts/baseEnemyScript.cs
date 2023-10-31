using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class baseEnemyScript : MonoBehaviour
{
    public Collider objectCollider;
    public GameObject ob;

    float health = 5;
    // Start is called before the first frame update
    void Start(){
            target = Wayfind.waypoints[0];
    }

    public void reduceHealth(float damage)
    {
        health = (health - damage);
    }

    public float getHealth()
    {
        return health;
    }

    private void healthCheck()
    {
        if (health <= 0)
        {
            Destroy(ob);
            return;
        }
    }
    // Movement
    public float speed = 10f;

    private Transform target;

    private int wavepointIndex = 0;

    void Update () {
        healthCheck();
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f){
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint(){
        if (wavepointIndex >= Wayfind.waypoints.Length - 1){
            //decrement player health according to
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = Wayfind.waypoints[wavepointIndex];
    }
}
