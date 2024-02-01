using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameArea : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    private float damage;

    private void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void setDamage(float newDamage)
    {
        damage = newDamage;
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        if (other.tag == "Enemy")
        {
            

        }
    }
}
