using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    private GameObject CurrentPlacingTower;

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
                CurrentPlacingTower = null;
            }
        }
    }

    public void PlaceTower(GameObject tower)
    {
        CurrentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
    }
}