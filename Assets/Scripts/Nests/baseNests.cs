using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseNests : MonoBehaviour
{
    public GameObject target;
    public GameObject targ;
    public string nestName;
    // Start is called before the first frame update
    void Start()
    {
        nestName = "Base Nest";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void moveTarget(Transform newTarget)
    {
        float targY = target.transform.position.y;
        float targX = newTarget.position.x;
        float targZ = newTarget.position.z;
        target.transform.position = new Vector3 (targX, targY, targZ);


    }

    public void showTarget()
    {
        
        target.GetComponentInChildren<MeshRenderer>().enabled = true;
    }

    public void hideTarget()
    {
        target.GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    public void changeTarget(GameObject newTarget)
    {
        Destroy(target);
        target = newTarget;
    }
}
