using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwarmingBee : MonoBehaviour
{
    public Transform target;
    private float timer = 0f;
    private float speed = 30f;
    private float directDamage = 0.5f;
    private float angle = 0f;
    private float range = 10f;
    private Vector3 center;

    public string id;


    private void Start()
    {
        center = transform.position;

        

    }
    void Update()
    {
        

        if (target == null)
        {
            timer += Time.deltaTime;
            Circle(timer);
        }
        else
        {
            Vector3 dir = target.position - transform.position;
            float distancePerFrame = speed * Time.deltaTime;

            move(dir, distancePerFrame);
        }
    }

    public void Circle(float time)
    {
        //float x = Mathf.Cos(time);
        //float y = transform.position.y;
        //float z = Mathf.Sin(time);
        //transform.position = new Vector3(x, y, z);
        
       angle = time * 1f;
       var offset = new Vector3(Mathf.Sin(angle), 0,  Mathf.Cos(angle)) * 2f;
       transform.position = center + offset;

    }

    public void Seek(Transform target)
    {
        this.target = target;
    }

    public void move(Vector3 dir, float distancePerFrame)
    {
        transform.Translate(dir.normalized * distancePerFrame, Space.World);
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.GetComponent<BaseEnemyLogic>().reduceHealth(directDamage);
            Destroy(gameObject);
        }
    }

    void findNewTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortDis = Mathf.Infinity;
        GameObject closeEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemyDistance < shortDis)
            {
                shortDis = enemyDistance;
                closeEnemy = enemy;
            }

        }

        if (closeEnemy != null && shortDis <= range)
        {
            target = closeEnemy.transform;
        }
    }

    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
}
