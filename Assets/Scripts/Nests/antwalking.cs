//using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antwalking : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float speed = 15f;
    public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f);

    public void seek(Transform newTarget)
    {
        target = newTarget;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, -minScale, 2 * Time.deltaTime);
            if(transform.localScale.x < 0 )
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (checkDistance())
            {
                transform.localScale = Vector3.Lerp(transform.localScale, -minScale, 2 * Time.deltaTime);
                if (transform.localScale.x < 0)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Vector3 dir = target.position - transform.position;
                float distancePerFrame = speed * Time.deltaTime;
                move(dir, distancePerFrame);
            }
        }
    }

    public void move(Vector3 dir, float distancePerFrame)
    {
        transform.Translate(dir.normalized * distancePerFrame, Space.World);
        transform.LookAt(target);
    }

    public bool checkDistance()
    {
        bool nextToTarget = false;

        float check = Vector3.Distance(transform.position, target.position);
        if(check < 0.1)
        {
            nextToTarget = true;
            return nextToTarget;
        }
        return nextToTarget;
    }
}
