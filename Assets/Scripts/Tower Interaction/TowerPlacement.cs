using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    public ShopLogic shopLogic; // This reference is used for purhcase cancelation (This isn't the best way to do this but it works for now)
    private GameObject CurrentPlacingTower;
    private GameObject Tower;
    private string towerName;
    private TargetingTypes savedTargettingType;

    [SerializeField] TowerSaveLoadManager towerSaveLoadManager;

    // Property to check if a tower is currently being placed
    public bool IsPlacingTower { get { return Tower != null; } }

    void Start()
    {
        Tower = CurrentPlacingTower;
        towerSaveLoadManager = GameObject.FindObjectOfType<TowerSaveLoadManager>();
    }

    void Update()
    {
        if (Tower != null)
        {
            Ray CameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit HitInfo;

            if (Physics.Raycast(CameraRay, out HitInfo, 100f))
            {
                Tower.transform.position = HitInfo.point;
            }

            if (Input.GetMouseButtonDown(0) && HitInfo.collider.CompareTag("Floor"))
            {
                CreatePlacementEffect();
                Tower.GetComponentInParent<BoxCollider>().enabled = true;
                
                //Tower.transform.GetChild(1).GetComponentInChildren<SphereCollider>().enabled = true;
                Tower.transform.Find("Rotate").GetComponentInChildren<SphereCollider>().enabled = true;

                // Call the ActivateTower function of the tower's script
                Tower.transform.GetComponentInChildren<BaseTowerLogic>().ActivateTower(); // We don't actually need the towername, we can just do it this way
                Debug.Log("Tower activated");

                if (towerName == "Bee Tower")
                {
                    string ID = Tower.transform.GetChild(1).gameObject.GetComponent<BeeTower>().GenerateId().ToString();
                    savedTargettingType = Tower.transform.GetChild(1).gameObject.GetComponent<BeeTower>().targetingType;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, savedTargettingType);
                }

                if (towerName == "Mortar Tower")
                {
                    string ID = Tower.transform.GetChild(1).gameObject.GetComponent<mortarTower>().GenerateId().ToString();
                    savedTargettingType = Tower.transform.GetChild(1).gameObject.GetComponent<mortarTower>().targetingType;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, savedTargettingType);
                }

                if (towerName == "Tether Tower")
                {
                    string ID = Tower.transform.GetChild(1).gameObject.GetComponent<tetherTower>().GenerateId().ToString();
                    savedTargettingType = Tower.transform.GetChild(1).gameObject.GetComponent<tetherTower>().targetingType;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, savedTargettingType);
                }

                if (towerName == "Fire Ant") // Previously Flame Tower
                {
                    string ID = Tower.transform.GetChild(0).gameObject.GetComponent<FlameTower>().GenerateId().ToString();
                    savedTargettingType = Tower.transform.GetChild(0).gameObject.GetComponent<FlameTower>().targetingType;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, savedTargettingType);
                }

                if (towerName == "Army Ant") // Previously Melee Tower
                {
                    string ID = Tower.transform.GetChild(0).gameObject.GetComponent<meleeTower>().GenerateId().ToString();
                    savedTargettingType = Tower.transform.GetChild(0).gameObject.GetComponent<meleeTower>().targetingType;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, savedTargettingType);
                }

                if (towerName == "Mortar Ant")
                {
                    string ID = Tower.transform.GetChild(0).gameObject.GetComponent<mortarTower>().GenerateId().ToString();
                    savedTargettingType = Tower.transform.GetChild(0).gameObject.GetComponent<mortarTower>().targetingType;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, savedTargettingType);
                }

                if (towerName == "Attack Bee")
                {
                    string ID = Tower.transform.GetChild(1).gameObject.GetComponent<BeeTower>().GenerateId().ToString();
                    savedTargettingType = Tower.transform.GetChild(1).gameObject.GetComponent<BeeTower>().targetingType;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, savedTargettingType);
                }

                if (towerName == "Bee Swarm")
                {
                    string ID = Tower.transform.GetChild(1).gameObject.GetComponent<BeeSwarm>().GenerateId().ToString();
                    savedTargettingType = Tower.transform.GetChild(1).gameObject.GetComponent<BeeSwarm>().targetingType;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, savedTargettingType);
                }

                if (towerName == "Buffing Bee")
                {
                    string ID = Tower.transform.GetChild(1).gameObject.GetComponent<BuffingBees>().GenerateId().ToString();
                    savedTargettingType = Tower.transform.GetChild(1).gameObject.GetComponent<BuffingBees>().targetingType;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, savedTargettingType);
                }

                if (towerName == "Wasp Melee")
                {
                    string ID = Tower.transform.GetChild(0).gameObject.GetComponent<WaspMelee>().GenerateId().ToString();
                    savedTargettingType = Tower.transform.GetChild(0).gameObject.GetComponent<WaspMelee>().targetingType;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, savedTargettingType);
                }

                if (towerName == "Wasp Tower")
                {
                    string ID = Tower.transform.GetChild(0).gameObject.GetComponent<StraightShooter>().GenerateId().ToString();
                    savedTargettingType = Tower.transform.GetChild(0).gameObject.GetComponent<StraightShooter>().targetingType;
                    Vector3 pos = Tower.transform.position;
                    towerSaveLoadManager.AddTower(ID, pos, towerName, savedTargettingType);
                }

                Tower = null;
            }

            if(Input.GetKeyDown(KeyCode.C))
            {
                ProcessRefund();
                Destroy(Tower);
                Tower = null;
            }
        }
    }

    public void PlaceTower(GameObject towerToPlace)
    {
        // Save the name of the tower's prefab so we can later call it's script
        towerName = towerToPlace.transform.gameObject.GetComponentInChildren<BaseTowerLogic>().towerName;
        Tower = GameObject.Instantiate(towerToPlace, Vector3.zero, Quaternion.identity);
        // Also start the particle effect
        Tower.transform.Find("Rotate").Find("TowerParticleSystem").GetComponent<ParticleSystem>().Stop();
    }

    private void CreatePlacementEffect()
    {
        // We must do this song and dance to grab the particle component
        Tower.transform.Find("Rotate").Find("TowerParticleSystem").GetComponent<ParticleSystem>().Play();
        // Stop the particle system atfer 3 seconds
        StartCoroutine(StopParticleEffect(Tower.transform.Find("Rotate").Find("TowerParticleSystem").GetComponent<ParticleSystem>()));
    }

    IEnumerator StopParticleEffect(ParticleSystem particleSystem)
    {
        yield return new WaitForSeconds(3f);
        particleSystem.Stop();
    }

    private void ProcessRefund() // Refund the player the cost of the tower
    {
        shopLogic.ReverseMoneyCheck(Tower.GetComponentInChildren<BaseTowerLogic>().buildCost);
    }
}
