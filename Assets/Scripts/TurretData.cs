using System;
using UnityEngine;

[Serializable]
public class TurretData
{
    public string TurretName;
    public float[] Position;

    public TurretData(basicTowerScript Tower)
    {
        TurretName = Tower.name;
        
        Vector3 TowerPosition = Tower.transform.position;

        Position = new float[]
        {
            TowerPosition.x, TowerPosition.y, TowerPosition.z
        };
    }

    public override string ToString()
    {
        return $"TurretType: {TurretName}, Position: ({Position[0]}, {Position[1]}, {Position[2]})";
    }
}