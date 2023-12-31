using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField]
    private Camera PlayerCamera;
    private GameObject CurrentPlacingTower;
    private GameObject Tower;
    private string towerName;
    private int targetint;

    [SerializeField]
    TowerSaveLoadManager towerSaveLoadManager;

    // Property to check if a tower is currently being placed
    public bool IsPlacingTower { get { return Tower != null; } }

    // Start is called before the first frame update
    void Start()
    {
        Tower = CurrentPlacingTower;
        towerSaveLoadManager = GameObject.FindObjectOfType<TowerSaveLoadManager>();
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
                CreatePlacementEffect();
                Tower.GetComponentInParent<BoxCollider>().enabled = true;
                
                Tower.transform.GetChild(1).GetComponentInChildren<SphereCollider>().enabled = true;

                // Call the ActivateTower function of the tower's script
                Tower.transform.GetComponentInChildren<basicTowerScript>().ActivateTower(); // We don't actually need the towername, we can just do it this way
                if (towerName == "beeTurret")
                {
                    string ID = Tower.transform.GetChild(1).gameObject.GetComponent<beeTower>().GenerateId().ToString();
                    targetint = Tower.transform.GetChild(1).gameObject.GetComponent<beeTower>().targetingint;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, targetint);
                }

                if (towerName == "mortarTurret")
                {
                    string ID = Tower.transform.GetChild(1).gameObject.GetComponent<mortarTower>().GenerateId().ToString();
                    targetint = Tower.transform.GetChild(1).gameObject.GetComponent<mortarTower>().targetingint;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, targetint);
                }

                if (towerName == "tetherTower")
                {
                    string ID = Tower.transform.GetChild(1).gameObject.GetComponent<tetherTower>().GenerateId().ToString();
                    targetint = Tower.transform.GetChild(1).gameObject.GetComponent<tetherTower>().targetingint;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, targetint);
                }
                Tower = null;
            }
        }
    }

    public void PlaceTower(GameObject tower, string towerNombre)
    {
        towerName = towerNombre; // Save the name of the tower's prefab so we can later call it's script
        Tower = GameObject.Instantiate(tower, Vector3.zero, Quaternion.identity);
        Tower.transform.GetChild(1).GetComponentInChildren<ParticleSystem>().Stop();
    }

    private void CreatePlacementEffect()
    {
        Tower.transform.GetChild(1).GetChild(2).GetComponent<ParticleSystem>().Play();
        // Stop the particle system atfer 3 seconds
        StartCoroutine(StopParticleEffect(Tower.transform.GetChild(1).GetChild(2).GetComponent<ParticleSystem>()));
    }

    IEnumerator StopParticleEffect(ParticleSystem particleSystem)
    {
        yield return new WaitForSeconds(3f);
        particleSystem.Stop();
    }
}
