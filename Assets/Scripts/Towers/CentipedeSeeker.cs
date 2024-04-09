using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeSeeker : MonoBehaviour
{
    public Transform target;
    public float speed = 7f;
    public int waypointIndex = 0;
    public int damage = 2;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Path.waypoints.Length; i++)
        {
            if (Vector3.Distance(transform.position, Path.waypoints[i].position) < Vector3.Distance(transform.position, Path.waypoints[waypointIndex].position))
            {
                waypointIndex = i;
            }
        }
        transform.position = Path.waypoints[waypointIndex].position;
        target = Path.waypoints[waypointIndex-1];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.4f){
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex <= 0)
        {
            Destroy(gameObject);
            return;
        }
        transform.LookAt(Path.waypoints[waypointIndex]);
        waypointIndex--;
        target = Path.waypoints[waypointIndex];
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            //Reduce health of enemy
            other.GetComponent<BaseEnemyLogic>().reduceHealth(damage);
            Destroy(gameObject);
            //exists = false;
  
        }  
    }
}
