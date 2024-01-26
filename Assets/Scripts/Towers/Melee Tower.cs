using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeTower : BaseTowerLogic
{
    private float directDamage = 5f;

    private float attackRate = 0f;
    private float coolDown = 0f;

    public int targetingint;
    // Start is called before the first frame update
    void Start()
    {
        Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        Track();
    }

    public override void Track()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        barrelToRotate.rotation = Quaternion.Euler(barrelToRotate.rotation.x, rotation.y, 0f);

        if (attackRate <= 0f)
        {
            coolDown += Time.deltaTime;
            barrelToRotate.rotation = Quaternion.Euler(90f, 0f, 0f);
            attackRate = 1f / fireRate;
            if(coolDown > 0)
            {

            }
        }

        attackRate -= Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<BaseEnemyLogic>().reduceHealth(directDamage);
            //Debug.Log(other.GetComponent<BaseEnemyLogic>().getHealth());
        }
    }
}
