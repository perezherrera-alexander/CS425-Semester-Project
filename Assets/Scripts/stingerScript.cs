using UnityEngine;

public class stingerScript : MonoBehaviour
{

    private Transform target;
    public Collider objCollider;

    public float directDamage = 1f;
    public float speed = 30f;
    public void Seek( Transform newTarget)
    {
        target = newTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distancePerFrame)
        {
            targetHit();
            return;
        }

        transform.Translate (dir.normalized * distancePerFrame, Space.World);
        transform.LookAt(target);
    }

    void targetHit()
    {

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<baseEnemyScript>().reduceHealth(directDamage);
            //Debug.Log(other.GetComponent<baseEnemyScript>().getHealth());
        }
    }


}
