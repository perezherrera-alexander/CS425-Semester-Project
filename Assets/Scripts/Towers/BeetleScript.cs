using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleScript : BaseTowerLogic
{
    public StoreTowerUpgradeData storeTowerUpgradeData;
    //private Animator animate;
    public string id;
    //public float damage = 0.5f;
    public Transform home;
    public GameObject beetle;
    //private float speed = 25f;
    public bool isHome = true;
    private bool isAttacking = false;
    public float bound = 1f;
    // Start is called before the first frame update
    void Start()
    {
        createOutline();
        towerName = "Beetle Buzzer";
        Invoke();
        MakeSphere();
        fireRate = 1f;
        curAttackSpeed = fireRate;
        //animate = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(data != null)
        {
            handleEffect();
        }
        Track();
        ListPrune();
        AddUpgradeEffects();
    }

    public override void Track()
    {
        //bool isDestroyed = ((object)target) != null && !target;
        //bool isReallyNull = ((object)target) == null;
        if(target == null)
        {
            beetle.GetComponent<BeetleProjectile>().seek(home);
            return;
        }
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        barrelToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (isHome && isAttacking == false)
        {
            //have beetle fly
  
            isAttacking = true;
            isHome = false;
            beetle.GetComponent<BeetleProjectile>().seek(target);
        }

        else if (isHome == false && isAttacking == true)
        {

            isAttacking = false;
            return;
        }
        else if (isHome == false && isAttacking == false)
        {
            isHome = true;
            return;
        }
    }

    public override void MakeSphere()
    {
        proximitySphere = transform.GetComponentInParent<SphereCollider>();
        proximitySphere.radius = 4.47f;
    }

    IEnumerator Timer()
    {
        Debug.Log("Timer start");
        yield return new WaitForSeconds(10);
        Debug.Log("Timer end");
        yield return null;
    }


    public void AddUpgradeEffects()
    {
        int count = 0;
        while (count <= storeTowerUpgradeData.ListOfUpgradesObtained.Count)
        {
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count] == "Beetle Upgrade 1")
            {
            }
            if (storeTowerUpgradeData.ListOfUpgradesObtained[count] == "Beetle Upgrade 2")
            {
            }
            count++;
        }
    }
}
