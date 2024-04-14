using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class grassHopperProjectile : mortarProjectile
{
    private Animator animate;
    public bool waved = false;
    public ParticleSystem shock;
    public bool jumper = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        float distance = Vector3.Distance(startPos, target.position);

        arcHeight = (float)(arcHeight * (0.10 * distance));

        step = speed / distance;

        animate = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(jumper)
        {
            animate.SetBool("Atrtacking", true);
        }
        else
        {
            animate.SetTrigger("Attack");
        }
        
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


        transform.Translate(dir.normalized * distancePerFrame, Space.World);
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var effect = other.GetComponent<Effectable>();

            if (effect != null && data != null)
            {
                effect.applyEffect(data);
            }
            if (bounces > 0)
            {
                other.GetComponent<BaseEnemyLogic>().reduceHealth(directDamage);
                if (waved)
                {
                    shockWave();
                    Instantiate(shock, transform.position, transform.rotation);
                }
                bounces -= 1;
                findNewTarget();
                if (jumper)
                {
                    animate.SetBool("Atrtacking", true);
                }
                else
                {
                    animate.SetTrigger("Attack");
                }

                if (target == null)
                {
                    //shockWave();
                    Destroy(gameObject);

                }
                startPos = transform.position;

                float distance = Vector3.Distance(startPos, target.position);

                arcHeight = (float)(arcHeight * (0.10 * distance));


                step = speed / distance;
                progress = 0;

            }
            else if (bounces == 0)
            {
                //shockWave();
                shockWave();
                Destroy(gameObject);

            }
            //other.GetComponent<BaseEnemyLogic>().reduceHealth(directDamage);
        }


    }

}
