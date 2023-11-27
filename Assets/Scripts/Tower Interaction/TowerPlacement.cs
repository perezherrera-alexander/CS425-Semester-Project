using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    private GameObject CurrentPlacingTower;
    private GameObject Tower;
    private string towerName;

    [SerializeField]
    TowerManager towerManager;

    // Start is called before the first frame update
    void Start()
    {
        Tower = CurrentPlacingTower;
        towerManager = GameObject.FindObjectOfType<TowerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Tower != null)
        {
            Ray CameraRay = PlayerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit HitInfo;

            if (Physics.Raycast(CameraRay, out HitInfo, 100f))
            {
                Tower.transform.position = HitInfo.point;
            }

            if (Input.GetMouseButtonDown(0) && HitInfo.collider.CompareTag("Floor"))
            {
                Tower.GetComponentInParent<BoxCollider>().enabled = true;
                // Call the ActivateTower function of the tower's script
                Tower.transform.GetComponentInChildren<basicTowerScript>().ActivateTower(); // We don't actually need the towername, we can just do it this way
                if (towerName == "beeTurret 1")
                {
                    string ID = Tower.transform.GetChild(1).gameObject.GetComponent<beeTower>().GenerateId().ToString();

                    Vector3 pos = Tower.transform.position;
                    towerManager.AddTower(ID, pos);
                }
                //else Debug.Log("Tower name not found");
                Tower = null;
            }
        }
    }

    public void PlaceTower(GameObject tower, string towerNombre)
    {
        towerName = towerNombre; // Save the name of the tower's prefab so we can later call it's script
        Tower = GameObject.Instantiate(tower, Vector3.zero, Quaternion.identity);
        Tower.GetComponentInChildren<SphereCollider>().enabled = false;
    }
}