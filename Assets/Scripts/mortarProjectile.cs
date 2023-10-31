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

    Vector3 startPos;
    Vector3 posOfTarget;
    float step;
    float progress = 0f;

    //float directDamage = 2f;
    float splashDamage = 2.5f;


    public void Seek(Transform newTarget)
    {
        target = newTarget;
    }

    void Start()
    {
        startPos = transform.position;

        float distance = Vector3.Distance(startPos, target.position);

        arcHeight = (float)(arcHeight * ( 0.10 * distance));

        posOfTarget = target.position;

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

        Vector3 dir = posOfTarget - transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        progress = Mathf.Min(progress + Time.deltaTime * step, 1.0f);

        float parabola = (float)(1.0f - 4.0f * (progress - 0.5f) * (progress - 0.5));

        Vector3 nextPos = Vector3.Lerp(startPos, posOfTarget, progress);

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

    }

    private void shockWave()
    {
        Collider[] colliders = Physics.OverlapSphere(posOfTarget, blastRadius);

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
