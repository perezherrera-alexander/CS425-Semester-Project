using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmingBee : MonoBehaviour
{
    private Transform target;
    private float timer = 0f;
    private float speed = 30f;
    void Update()
    {
        if(target == null)
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
        float x = Mathf.Cos(time);
        float y = transform.position.y;
        float z = Mathf.Sin(time);
        transform.position = new Vector3(x, y, z);
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
}
