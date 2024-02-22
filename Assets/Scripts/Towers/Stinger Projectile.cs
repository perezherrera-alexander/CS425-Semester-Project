using UnityEngine;

public class stingerScript : MonoBehaviour
{
#pragma warning disable 0414
    [SerializeField] StatusEffects data;
    private Transform target;
    private Transform lastKnown;
    public Collider objCollider;

    public float directDamage = 1f;
    public float speed = 30f;

    public float radius = 50f;
    public bool exists = false;
    private float bounce = 2f;
#pragma warning restore 0414
    public void Seek( Transform newTarget)
    {
        target = newTarget;
        lastKnown = target;

    }

    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            if (exists)
            {
                findNewTarget();
            }
            else

                Destroy(gameObject);
                return;
        }

        

        Vector3 dir = target.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;
        exists = true;



        move(dir, distancePerFrame);
    }

    void move(Vector3 dir, float distancePerFrame)
    {
        transform.Translate(dir.normalized * distancePerFrame, Space.World);
        transform.LookAt(target);
    }


    void findNewTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortDis = Mathf.Infinity;
        GameObject closeEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemyDistance < shortDis)
            {
                shortDis = enemyDistance;
                closeEnemy = enemy;
            }
            
        }

        if (closeEnemy != null && shortDis <= radius)
        {
            target = closeEnemy.transform;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var effect = other.GetComponent<Effectable>();

            if (effect != null)
            {
                effect.applyEffect(data);
            }

            //other.GetComponent<BaseEnemyLogic>().reduceHealth(directDamage);



            Destroy(gameObject);
            exists = false;
  
        }

        

        
    }


}
