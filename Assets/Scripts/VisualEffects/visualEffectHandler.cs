using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualEffectHandler : MonoBehaviour
{
    public ParticleSystem particles;

    void Start()
    {
        gameObject.SetActive(true);
    }
    void Update()
    {
        
    }
    public void playParts(Transform pos)
    {
        
        Instantiate(particles, pos.position, Quaternion.identity);
        
        //StartCoroutine(stopParticles(parts));
    }

}
