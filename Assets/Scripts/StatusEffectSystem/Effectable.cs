using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Effectable 
{
    public void applyEffect(StatusEffects effect);
    public void removeEffect(int ind);

    public void handleEffect();
}
