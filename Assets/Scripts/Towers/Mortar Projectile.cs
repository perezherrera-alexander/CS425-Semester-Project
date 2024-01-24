using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mortarProjectile : MonoBehaviour
{
#pragma warning disable 0414
    private Transform target;

    public float speed = 10f;
    public float arcHeight = 1.0f;
    public float blastRadius = 3f;
    private float radius = 50f;

    Vector3 startPos;
    Vector3 posOfTarget;
    float step;
    float progress = 0f;

    float directDamage = 2f;
    float splashDamage = 2.5f;
    float bounces = 1f;

    bool exists = false;
#pragma warning restore 0414


    public void Seek(Transform newTarget)
    {
        target = newTarget;
        posOfTarget = target.position;
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

                Destroy(gameObject);
                return;
            
        }

        exists = true;

        Vector3 dir = target.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        progress = Mathf.Min(progress + Time.deltaTime * step, 1.0f);

        float parabola = (float)(1.0f - 4.0f * (progress - 0.5f) * (progress - 0.5));

        Vector3 nextPos = Vector3.Lerp(startPos, target.position, progress);

        nextPos.y += parabola * arcHeight;

        transform.position = nextPos;


        transform.Translate(dir.normalized * distancePerFrame, Space.World);

    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.GetComponent<BaseEnemyLogic>().reduceHealth(directDamage);
        }

        if (bounces > 0)
        {

            bounces -= 1;
            findNewTarget();
            if(target == null)
            {
                shockWave();
                Destroy(gameObject);
                exists = false;
            }
            startPos = transform.position;

            float distance = Vector3.Distance(startPos, target.position);

            arcHeight = (float)(arcHeight * (0.10 * distance));


            step = speed / distance;
            progress = 0;

        }
        else if (bounces == 0)
        {
            shockWave();
            Destroy(gameObject);
            exists = false;
        }
    }

    private void shockWave()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach(Collider c in colliders)
        {
            if (c.GetComponent<BaseEnemyLogic>())
            {
                c.GetComponent<BaseEnemyLogic>().reduceHealth(splashDamage);
                //Debug.Log(c.GetComponent<BaseEnemyLogic>().getHealth());
            }
            //Debug.Log("Enemy took splash damage");
        }
    }


    void findNewTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortDis = Mathf.Infinity;
        GameObject closeEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.position == target.position)
            {
                
            }
            else
            {
                float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
                if (enemyDistance < shortDis)
                {
                    shortDis = enemyDistance;
                    closeEnemy = enemy;
                }
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
