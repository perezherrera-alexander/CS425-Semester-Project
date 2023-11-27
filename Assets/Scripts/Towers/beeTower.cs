using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class beeTower : basicTowerScript//, ISaveable
{
    [SerializeField] public float[] towerPosition;


    public string id;



    // Start is called before the first frame update

    void Start()
    {
        towerName = "Bee Tower";
        Invoke();
        targeting = "close";
        makeSphere();
    }

    // Update is called once per frame
    void Update()
    {
        
        track();
        listPrune();
    }

    public override void Shoot()
    {
        GameObject stinger = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        stingerScript sting = stinger.GetComponent<stingerScript>();

        if (sting != null)
        {
            sting.Seek(target);
        }
        else if (sting.exists && target == null)
        {

        }
    }


    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }

    /*

    public object CaptureState()
    {
        towerPosition = new float[] { transform.position.x, transform.position.y, transform.position.z };
        Debug.Log("Managed to capture state for tower");
        return new SaveData
        {
            TowerName = towerName,
            TowerPosition = towerPosition,
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        towerName = saveData.TowerName;
        // Retrieve the position from the float array
        Vector3 loadedPosition = new Vector3(saveData.TowerPosition[0], saveData.TowerPosition[1], saveData.TowerPosition[2]);
        towerPosition[0] = loadedPosition.x;
        towerPosition[1] = loadedPosition.y;
        towerPosition[2] = loadedPosition.z;
    }

    [Serializable]
    private struct SaveData
    {
        public string TowerName;
        public float[] TowerPosition;
    }

    */
}
