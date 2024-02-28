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
    public float attackSpeedPenalty;
    public float attackSpeedIncrease;
    public float lifeTime;
    

    public float initDotAmount;
    public float initTickSpeed;
    public float initMovementPenalty;
    public float initLifeTime;

    public GameObject effectParticles;

}
