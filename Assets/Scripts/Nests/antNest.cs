using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antNest : baseNests
{
    [Header("Nest Parts")]
    public BoxCollider antArea;
    public Transform center;
    public GameObject ant;

    private float damage = 2f;
    private float fireRate = 1f;
    private float fireCountdown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        nestName = "Ant Nest";
    }

    // Update is called once per frame
    void Update()
    {
        track();
        centerBoxCollider();
        reSizeBox();

        if(fireCountdown <= 0f)
        {
            GameObject ant1 = Instantiate(ant, center);
            antwalking ant2 = ant1.GetComponent<antwalking>();
            if (ant2 != null)
            {
                ant2.seek(target.transform);
            }
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;

    }

    void track()
    {
        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            center.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }

    void centerBoxCollider()
    {
        if (target != null)
        {
            Vector3 midPoint = (center.position + target.transform.position) * 0.5f;
            antArea.transform.position = midPoint;
        }
    }

    void reSizeBox()
    {
        if (target != null)
        {
            float xSize = antArea.size.x;
            float ySize = antArea.size.y;
            float newZ = target.transform.position.z - center.position.z;
            antArea.size = new Vector3(xSize, ySize, newZ);
        }
    }

    public override void moveTarget(Transform newTarget)
    {
        float targY = target.transform.position.y;
        float targX = newTarget.position.x;
        float targZ = newTarget.position.z;
        target.transform.position = new Vector3(targX, targY, targZ);
        centerBoxCollider();
        reSizeBox();
    }

    

    public float getDamage()
    {
        return damage;
    }


}
