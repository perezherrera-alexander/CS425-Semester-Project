using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableEntity : MonoBehaviour
{
    [SerializeField] private string id = string.Empty;

    public string Id => id;

    [ContextMenu("Generate ID")]
    public string GenerateId()
    {
        id = Guid.NewGuid().ToString();
        return id;
    }
    public object CaptureState()
    {
        var state = new Dictionary<string, object>();

        foreach (var saveable in GetComponents<ISaveable>())
        {
            state[saveable.GetType().ToString()] = saveable.CaptureState();
        }

        // Also, store the ID in the state
        state[nameof(Id)] = Id;

        return state;
    }

    public void RestoreState(object state)
    {
        var stateDictionary = (Dictionary<string, object>)state;

        // Restore the ID from the state
        if (stateDictionary.TryGetValue(nameof(Id), out object idValue))
        {
            id = (string)idValue;
        }

        foreach (var saveable in GetComponents<ISaveable>())
        {
            string TypeName = saveable.GetType().ToString();

            if (stateDictionary.TryGetValue(TypeName, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }
}
