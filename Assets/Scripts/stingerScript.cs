using UnityEngine;

public class stingerScript : MonoBehaviour
{

    private Transform target;

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
}
