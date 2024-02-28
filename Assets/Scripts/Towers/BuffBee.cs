using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBee : MonoBehaviour
{
    [SerializeField] StatusEffects data;
    private Transform target;

    public float speed = 15f;
    // Start is called before the first frame update
    
    public void Seek(Transform newTarget)
    {
        target = newTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject); 
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;
        move(dir, distancePerFrame);
    }

    void move(Vector3 dir, float distancePerFrame)
    {
        transform.Translate(dir.normalized * distancePerFrame, Space.World);
        transform.LookAt(target);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BoxCollider>())
        {

            if(other.GetComponentInChildren<BaseTowerLogic>() != null)
            {
                var effect = other.GetComponentInChildren<Effectable>();
                if (effect != null)
                {
                    effect.applyEffect(data);
                    Destroy(gameObject);
                }
            }
        }
    }
}
