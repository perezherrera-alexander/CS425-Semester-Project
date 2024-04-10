using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nestSpawn : MonoBehaviour
{
    public GameObject beeNest;
    public GameObject waspNest;
    public GameObject antNest;
    public Generals activeGeneral;
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        activeGeneral = playerData.activeGeneral;
        if(activeGeneral == Generals.Bee)
        {
            Instantiate(beeNest, transform.position, transform.rotation);
        }
        else if(activeGeneral == Generals.Wasp)
        {
            Instantiate(waspNest, transform.position, transform.rotation);
        }
        else if(activeGeneral == Generals.Ant)
        {
            Instantiate(antNest, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
