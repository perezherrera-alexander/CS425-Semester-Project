using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectile : MonoBehaviour
{
    private float moveSpeed = 40f;
    private float pierceAmount = 2f;
    private float damage = 1.5f;

    public float lifeTime = 2f;
    public bool isActive = false;


    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        transform.LookAt(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if(lifeTime > 0)
            {
                lifeTime -= Time.deltaTime;
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(pierceAmount > 0)
            {
                other.GetComponent<BaseEnemyLogic>().reduceHealth(damage);
                //knockback
                other.GetComponent<BaseEnemyLogic>().knockback(15);
                pierceAmount = pierceAmount - 1;
            }
            else if(pierceAmount == 0) 
            {
                other.GetComponent<BaseEnemyLogic>().reduceHealth(damage);
                Destroy(gameObject);
            }
                
        }
    }
}
