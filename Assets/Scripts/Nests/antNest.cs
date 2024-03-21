using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antNest : baseNests
{
    [Header("Nest Parts")]
    public BoxCollider antArea;
    public Transform center;

    private float damage = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        track();
        centerBoxCollider();
        reSizeBox();
    }

    void track()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        center.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void centerBoxCollider()
    {
        Vector3 midPoint = (center.position + target.position) * 0.5f;
        antArea.transform.position = midPoint;
    }

    void reSizeBox()
    {
        float xSize = antArea.size.x;
        float ySize = antArea.size.y;
        float newZ = target.position.z - center.position.z;
        antArea.size = new Vector3(xSize, ySize, newZ);
    }

    public override void moveTarget(Transform newTarget)
    {
        float targY = target.position.y;
        float targX = newTarget.position.x;
        float targZ = newTarget.position.z;
        target.position = new Vector3(targX, targY, targZ);
        centerBoxCollider();
        reSizeBox();
    }

    

    public float getDamage()
    {
        return damage;
    }


}
