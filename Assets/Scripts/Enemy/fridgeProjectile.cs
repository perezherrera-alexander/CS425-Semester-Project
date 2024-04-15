using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fridgeProjectile : MonoBehaviour
{
    public Transform target;
    public float speed = 30f;
    public float freeze = 4f;
    public ParticleSystem parts;

    // Update is called once per frame
    public void Seek(Transform newTarget)
    {
        target = newTarget;
    }
    private void Start()
    {
        Instantiate(parts, transform);
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        if(targetTracking() == false)
        {
            Vector3 dir = target.position - transform.position;
            float distancePerFrame = speed * Time.deltaTime;
            move(dir, distancePerFrame);
        }
        else
        {
            target.GetComponent<BaseTowerLogic>().freeze(freeze);
            Destroy (gameObject);
        }


    }

    void move(Vector3 dir, float distancePerFrame)
    {
        transform.Translate(dir.normalized * distancePerFrame, Space.World);
        transform.LookAt(target);
    }

    bool targetTracking()
    {
        float check = Vector3.Distance(transform.position, target.position);
        if (check < 0.1)
        {
            return true;
        }
        return false;
    }
}
