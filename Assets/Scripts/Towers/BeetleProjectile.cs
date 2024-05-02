//using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleProjectile : MonoBehaviour
{
    public Transform target;
    private float speed = 25;
    private Animator animate;
    public float damage = 3f;
    public bool attacking = false;
    public AudioSource audioSource;
    public AudioClip attack;   


    private void Start()
    {
        animate = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //transform.rotation =  Quaternion.Euler(0,transform.eulerAngles.y, 0);
        if (target == null)
        {
            animate.SetBool("Attacking", false);
            attacking = false;
            return;
        }
        animate.SetBool("Attacking", true);
        attacking = true;
        float distancePerFrame = speed * Time.deltaTime;

        move(distancePerFrame);
    }

    void move(float distancePerFrame)
    {
        //transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        transform.position = Vector3.MoveTowards(transform.position, target.position, distancePerFrame);
        
        if(Vector3.Distance(transform.position, target.position) < 0.0001f)
        {
            return;
        }
    }

    public void seek(Transform newTarget)
    {
        target = newTarget;
    }

    public Transform returnTarget()
    {
        return target;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            float dam = damage * Time.deltaTime;
            other.GetComponent<BaseEnemyLogic>().beetleDamage(dam);
            //play attack sound in intervals
            if (Time.time % 1 > 0.9 && Time.time % 1 < 1)
            {
                audioSource.PlayOneShot(attack);
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Mortar")
        {
            other.GetComponent<BeetleScript>().isHome = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mortar")
        {
            other.GetComponent<BeetleScript>().isHome = true;
        }

    }


}
