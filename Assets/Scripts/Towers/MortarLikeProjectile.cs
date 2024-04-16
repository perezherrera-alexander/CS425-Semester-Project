using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MortarLikeProjectile : mortarProjectile
{
    // Start is called before the first frame update
    public visualEffectHandler particles;
    void Start()
    {
        startPos = transform.position;
       

        float distance = Vector3.Distance(startPos, posOfTarget);

        arcHeight = (float)(arcHeight * (0.10 * distance));


        step = speed / distance;
    }

    // Update is called once per frame
    void Update()
    {

        float check = Vector3.Distance(transform.position, posOfTarget);

        Vector3 dir = posOfTarget - transform.position;

        float distancePerFrame = speed * Time.deltaTime;

        progress = Mathf.Min(progress + Time.deltaTime * step, 1.0f);

        float parabola = (float)(1.0f - 4.0f * (progress - 0.5f) * (progress - 0.5));

        Vector3 nextPos = Vector3.Lerp(startPos, posOfTarget, progress);

        nextPos.y += parabola * arcHeight;

        transform.position = nextPos;


        transform.Translate(dir.normalized * distancePerFrame, Space.World);
    }

    public override void Seek(Transform newTarget)
    {
        posOfTarget = newTarget.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            shockWave();
            particles.playParts(transform);
            Destroy(gameObject);
        }
    }

}
