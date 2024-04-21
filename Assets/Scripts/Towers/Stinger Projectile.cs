using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class stingerScript : MonoBehaviour
{
#pragma warning disable 0414
    [SerializeField] StatusEffects data;
    private Transform target;
    private Transform lastKnown;
    public Collider objCollider;

    public float directDamage = 0.5f;
    public float speed = 30f;

    public float radius = 50f;
    public bool exists = false;
    private float bounce = 2f;

    private Vector3 center;
    private bool centerSet = false;
    private float angle = 0f;
    private float timer = 0f;
    public GameObject wing1;
    public GameObject wing2;
    public bool wingToggle = false;
    public bool once = false;
#pragma warning restore 0414
    public void Seek( Transform newTarget)
    {
        target = newTarget;
        lastKnown = target;
        wing1.GetComponent<MeshRenderer>().enabled = false;
        wing2.GetComponent<MeshRenderer>().enabled = false;

        if (wingToggle == true)
        {
            wing1.GetComponent<MeshRenderer>().enabled = true;
            wing2.GetComponent<MeshRenderer>().enabled = true;

        }

    }
    void setCenter()
    {
        center = transform.position;
        centerSet = true;
    }


    // Update is called once per frame
    void Update()
    {





        if (target == null)
        {
            timer += Time.deltaTime;
            if(centerSet == false)
            {
                setCenter();
            }
            Circle(timer);
            findNewTarget();
            

                return;
        }


        centerSet = false;
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

    public void Circle(float time)
    {

        angle = time * 2f;
        var offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * 2f;
        transform.position = center + offset;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            //Reduce health of enemy
            //other.GetComponent<BaseEnemyLogic>().reduceHealth(directDamage);
            //stun
            //other.GetComponent<BaseEnemyLogic>().stun(2f);
                // //Duration of enemy slow
                // other.GetComponent<BaseEnemyLogic>().slowDownTimer = 2f;
                // //Slow factor
                // other.GetComponent<BaseEnemyLogic>().slowFactor = 0.5f;
            //Damage over time
            //other.GetComponent<BaseEnemyLogic>().dotDamage = 1;
            //other.GetComponent<BaseEnemyLogic>().dotTimer = 2f;

            //knockback
            //other.GetComponent<BaseEnemyLogic>().knockback(75);


            //Destroy projectile

            var effect = other.GetComponent<Effectable>();

            if (effect != null)
            {
                effect.applyEffect(data);
            }




            Destroy(gameObject);
            exists = false;
  
        }

        

        
    }


}
