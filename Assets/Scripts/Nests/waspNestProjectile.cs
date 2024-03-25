using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waspNestProjectile : StandardProjectile
{
    private float projectileDamage = 1f;
    private float speed = 40f;
    private float life = 3f;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        transform.Rotate(Vector3.up, 90f);
    }

    public void Look(Transform target)
    {
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (life > 0)
            {
                life -= Time.deltaTime;
                transform.position += -transform.right * speed * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

                other.GetComponent<BaseEnemyLogic>().reduceHealth(projectileDamage);
                Destroy(gameObject);
            

        }
    }

}
