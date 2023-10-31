using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseEnemyScript : MonoBehaviour
{
    public Collider objectCollider;
    public GameObject ob;

    float health = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthCheck();
    }

    public void reduceHealth(float damage)
    {
        health = (health - damage);
    }

    public float getHealth()
    {
        return health;
    }

    private void healthCheck()
    {
        if (health <= 0)
        {
            Destroy(ob);
            return;
        }
    }
}
