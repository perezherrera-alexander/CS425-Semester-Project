using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Status Effect")]
public class StatusEffects : ScriptableObject
{
    public string Name;
    public float dotAmount;
    public float tickSpeed;
    public float movementPenalty;
    public float lifeTime;

    public GameObject effectParticles;

}
