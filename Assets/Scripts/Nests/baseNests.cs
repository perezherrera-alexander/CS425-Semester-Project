using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseNests : MonoBehaviour
{
    public Transform target;
    public GameObject targ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void moveTarget(Transform newTarget)
    {
        float targY = target.position.y;
        float targX = newTarget.position.x;
        float targZ = newTarget.position.z;
        target.position = new Vector3 (targX, targY, targZ);


    }

    public void showTarget()
    {
        
        targ.GetComponent<MeshRenderer>().enabled = true;
    }

    public void hideTarget()
    {
        targ.GetComponent<MeshRenderer>().enabled = false;
    }
}
