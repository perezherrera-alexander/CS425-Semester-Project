using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mortarProjectile : MonoBehaviour
{

    private Transform target;

    public float speed = 5f;
    public float arcHeight = 1.0f;
    public float blastRadius = 3f;
    private float radius = 50f;

    Vector3 startPos;
    Transform posOfTarget;
    float step;
    float progress = 0f;

    //float directDamage = 2f;
    float splashDamage = 2.5f;

    bool exists = false;


    public void Seek(Transform newTarget)
    {
        target = newTarget;
        posOfTarget = target;
    }

    void Start()
    {
        startPos = transform.position;

        float distance = Vector3.Distance(startPos, target.position);

        arcHeight = (float)(arcHeight * ( 0.10 * distance));


        step = speed / distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (exists)
            {
                findNewTarget();
                startPos = transform.position;

                float distance = Vector3.Distance(startPos, target.position);

                arcHeight = (float)(arcHeight * (0.10 * distance));


                step = speed / distance;

            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        exists = true;

        Vector3 dir = target.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        progress = Mathf.Min(progress + Time.deltaTime * step, 1.0f);

        float parabola = (float)(1.0f - 4.0f * (progress - 0.5f) * (progress - 0.5));

        Vector3 nextPos = Vector3.Lerp(startPos, target.position, progress);

        nextPos.y += parabola * arcHeight;

        transform.position = nextPos;

        if (dir.magnitude <= distancePerFrame)
        {
            
            targetHit();
            return;
        }

        //transform.Translate(dir.normalized * distancePerFrame, Space.World);

    }

    private void targetHit()
    {
        
        Destroy(gameObject);
        shockWave();
        exists = false;

    }

    private void shockWave()
    {
        Collider[] colliders = Physics.OverlapSphere(target.position, blastRadius);

        foreach(Collider c in colliders)
        {
            if (c.GetComponent<baseEnemyScript>())
            {
                c.GetComponent<baseEnemyScript>().reduceHealth(splashDamage);
                //Debug.Log(c.GetComponent<baseEnemyScript>().getHealth());
            }
            //Debug.Log("Enemy took splash damage");
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<baseEnemyScript>())
        {
            other.GetComponent<baseEnemyScript>().reduceHealth(directDamage);
            //Debug.Log(other.GetComponent<baseEnemyScript>().getHealth());
        }
    }*/

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

        if (closeEnemy != null && shortDis <= radius)
        {
            target = closeEnemy.transform;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
