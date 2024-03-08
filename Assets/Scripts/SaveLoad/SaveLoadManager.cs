using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour
{
    private string SavePath => $"{Application.persistentDataPath}/save.txt";

    [ContextMenu("Save")]
    public void Save()
    {
        var State = LoadFile();
        CaptureState(State);
        // Delete the old save file if it exists
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }
        // Save the new state
        SaveFile(State);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        var State = LoadFile();
        RestoreState(State);
    }

    private void SaveFile(object state)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    private Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("DOESN'T EXIST");
            return new Dictionary<string, object>();
        }

        using (FileStream file = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(file);
        }
    }

    private void CaptureState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.CaptureState();
        }
    }

    private void RestoreState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if (state.TryGetValue(saveable.Id, out object value))
            {
                saveable.RestoreState(value);
            }
        }
    }
}
