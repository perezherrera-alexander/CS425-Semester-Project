using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nestTargetPlacement : MonoBehaviour
{
    [SerializeField] private Camera playerCam;
    private GameObject target;
    private GameObject currentTarget;
    // Start is called before the first frame update
    void Start()
    {
        target = currentTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Ray cameraRay = playerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(cameraRay, out hitInfo, 100f)) 
            {
                target.transform.position = hitInfo.point;
            }

            if(Input.GetMouseButtonDown(0) && hitInfo.collider.CompareTag("Floor"))
            {
                target.transform.GetChild(1).gameObject.GetComponent<baseNests>().moveTarget(target.transform);
            }
        }
    }
}
