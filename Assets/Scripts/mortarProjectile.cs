using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mortarProjectile : MonoBehaviour
{

    private Transform target;

    public float speed = 5f;
    public float arcHeight = 1.0f;

    Vector3 startPos;
    float step;
    float progress = 0f;
    public void Seek(Transform newTarget)
    {
        target = newTarget;
    }

    void Start()
    {
        startPos = transform.position;

        float distance = Vector3.Distance(startPos, target.position);

        arcHeight = (float)(arcHeight * ( 0.25 * distance));

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

    void targetHit()
    {
        Destroy(gameObject);
    }
}
