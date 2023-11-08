using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    private GameObject CurrentPlacingTower;
    private string towerName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentPlacingTower != null)
        {
            Ray CameraRay = PlayerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(CameraRay, out RaycastHit HitInfo, 100f))
            {
                CurrentPlacingTower.transform.position = HitInfo.point;
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Call the ActivateTower function of the tower's script
                if(towerName == "beeTurret 1") CurrentPlacingTower.transform.GetChild(1).gameObject.GetComponent<beeTower>().ActivateTower();
                else if(towerName == "mortarTurret") CurrentPlacingTower.transform.GetChild(1).gameObject.GetComponent<mortarTower>().ActivateTower();
                else if(towerName == "tetherTower") CurrentPlacingTower.transform.GetChild(1).gameObject.GetComponent<tetherTower>().ActivateTower();
                else Debug.Log("Tower name not found");
                CurrentPlacingTower = null;
            }
        }
    }

    public void PlaceTower(GameObject tower, string towerNombre)
    {
        towerName = towerNombre; // Save the name of the tower's prefab so we can later call it's script
        CurrentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
    }
}