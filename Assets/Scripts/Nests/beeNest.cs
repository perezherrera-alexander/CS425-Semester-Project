using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beeNest : baseNests
{

    public bool abilityActive = false;
    public float abilityTimer = 10f;
    // Start is called before the first frame update
    void Start()
    {
        nestName = "Bee Nest";
        
    }

    // Update is called once per frame
    void Update()
    {
        if (abilityActive)
        {
            ability();
        }
        else
        {

        }
    }

    public void abilityActivate()
    {
        abilityActive = true;
    }

    public void abilityDeactivate() 
    {  
        abilityActive = false; 
    }

    public void ability()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                if (enemy.GetComponent<BaseEnemyLogic>().goldIncreased == false)
                {
                    int gold = enemy.GetComponent<BaseEnemyLogic>().GoldWorth;
                    gold = gold * 2;
                    enemy.GetComponent<BaseEnemyLogic>().newGoldCost(gold);
                }
            }
        }
    }


}
