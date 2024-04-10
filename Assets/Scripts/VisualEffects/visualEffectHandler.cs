using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualEffectHandler : MonoBehaviour
{
    public ParticleSystem particles;
    Quaternion spawnRotation; 
    void Start()
    {
        spawnRotation = particles.transform.rotation;
    }
    void Update()
    {
        
    }
    public void playParts(Transform pos)
    {
        if (particles.name == "CFXR2 WW Enemy Explosion")
        {
            Instantiate(particles, pos.position, Quaternion.Euler(-90,0,0));
        }
        else
        {
            Instantiate(particles, pos.position, spawnRotation);
        }
        //StartCoroutine(stopParticles(parts));
    }

}
