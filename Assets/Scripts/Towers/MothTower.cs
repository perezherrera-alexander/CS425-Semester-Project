using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MothTower : BaseTowerLogic
{
    public Transform center;
    public ParticleSystem particles;
    private float timer = 0f;
    private float angle = 0f;


    // Start is called before the first frame update
    void Start()
    {
        fireRate = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        circle(timer);

            if (fireCountdown <= 0f)
            {
                Shoot();
                Instantiate(particles, transform);
                fireCountdown = 1f / fireRate;
            }
            
        
        fireCountdown -= Time.deltaTime;
    }

    public override void Shoot()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, targettingRange);

        foreach(Collider c in colliders)
        {
            if (c.GetComponent<BaseEnemyLogic>())
            {
                c.GetComponent<BaseEnemyLogic>().stun(2f);
                //Have this tower do knockback for now to test functionality, this tower will stun enemies when the stun effect is implemented
            }
        }
    }

    void circle(float Time)
    {
        angle = Time * 1f;
        var offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * 15f;
        transform.position = center.position + offset;
        transform.LookAt(center);
    }
}
