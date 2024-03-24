using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class waspNestScript : baseNests
{
    [Header("Nest Parts")]
    public Transform barrelToRotate;
    public Transform locationToFireFrom;
    public GameObject projectilePrefab;
    [Header("Nest Stats")]
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    public float curAttackSpeed;


    // Start is called before the first frame update
    void Start()
    {
        curAttackSpeed = fireRate;   
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            track();
        }
    }

    void track()
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        barrelToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        float rand = Random.Range(1f, 5f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f * rand;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        //float randx = Random.Range(-0.5f, 0.5f);
        //float y = locationToFireFrom.position.y;
        //float z = locationToFireFrom.position.z;
        Transform firePos = locationToFireFrom;
        //firePos.position = new Vector3(randx, y, z);
        GameObject wasp = (GameObject)Instantiate(projectilePrefab, firePos.position, locationToFireFrom.rotation);
        waspNestProjectile sting = wasp.GetComponent<waspNestProjectile>();
        if (sting != null)
        {
            sting.Look(target.transform);
        }
    }
}
