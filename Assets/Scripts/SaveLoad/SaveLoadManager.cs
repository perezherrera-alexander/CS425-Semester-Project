using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour
{
    private string SavePath0 => $"{Application.persistentDataPath}/MenuSave.txt";
    private string SavePath1 => $"{Application.persistentDataPath}/WorldMapSave.txt";

    [ContextMenu("Save")]
    public void Save(int choice)
    {
        if (choice == 0)
        {
            var State = LoadFile(choice);
            CaptureState(State);
            // Delete the old save file if it exists
            if (File.Exists(SavePath0))
            {
                File.Delete(SavePath0);
            }
            // Save the new state
            SaveFile(State, choice);
        }
        else
        {
            var State = LoadFile(choice);
            CaptureState(State);
            // Delete the old save file if it exists
            if (File.Exists(SavePath1))
            {
                File.Delete(SavePath1);
            }
            // Save the new state
            SaveFile(State, choice);
        }
    }

    [ContextMenu("Load")]
    public void Load(int choice)
    {
        if (choice == 0)
        {
            var State = LoadFile(choice);
            RestoreState(State);
        }
        else
        {
            var State = LoadFile(choice);
            RestoreState(State);
        }
    }

    private void SaveFile(object state, int choice)
    {
        if (choice == 0)
        {
            using (var stream = File.Open(SavePath0, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }
        else
        {
            using (var stream = File.Open(SavePath1, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }
    }

    private Dictionary<string, object> LoadFile(int choice)
    {
        if (choice == 0)
        {
            if (!File.Exists(SavePath0))
            {
                Debug.Log("DOESN'T EXIST");
                return new Dictionary<string, object>();
            }

            using (FileStream file = File.Open(SavePath0, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(file);
            }
        }
        else
        {
            if (!File.Exists(SavePath1))
            {
                Debug.Log("DOESN'T EXIST");
                return new Dictionary<string, object>();
            }

            using (FileStream file = File.Open(SavePath1, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(file);
            }
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
