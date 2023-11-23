using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour
{
    const string player_path = "/player.data";
    const string tower_path = "/tower.data";
    const string tower_count_path = "/tower.count";


    [SerializeField]
    GameObject beeTurretPrefab;

    // List to store all towers
    public static List<basicTowerScript> towers = new List<basicTowerScript>();



    public static void SaveStats(PlayerStats stats)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + player_path;

        FileStream file = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(stats);

        formatter.Serialize(file, data);

        file.Close();
    }

    public static PlayerData loadStats()
    {
        string path = Application.persistentDataPath + player_path;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream file = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(file) as PlayerData;

            file.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);

            return null;
        }
    }

    public void SaveTower()
    {
        Debug.Log("ANYONE HOME?");
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + tower_path;

        string countpath = Application.persistentDataPath + tower_count_path;

        FileStream countfile = new FileStream(countpath, FileMode.Create);

        formatter.Serialize(countfile, towers.Count);

        countfile.Close();

        for (int i = 0; i < towers.Count; i++)
        {
            string filePath = path + i.ToString(); // Use ToString to convert index to string

            FileStream file = new FileStream(filePath, FileMode.Create);

            TurretData data = new TurretData(towers[i]);

            formatter.Serialize(file, data);

            file.Close();

            Debug.Log(towers.Count);

            Debug.Log(towers[i]);
        }

    }

    public void LoadTower()
    {
        Debug.Log("I opened");

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + tower_path;

        string countpath = Application.persistentDataPath + tower_count_path;

        int towercount = 0;

        if (File.Exists(countpath))
        {
            FileStream countfile = new FileStream(countpath, FileMode.Open);

            towercount = (int)formatter.Deserialize(countfile);

            Debug.Log(towercount);

            countfile.Close();
        }
        else
        {
            Debug.LogError("Path not found in " + countpath);
        }

        for (int i = 0; i < towercount; i++)
        {
            string filePath = path + i.ToString(); // Use ToString to convert index to string

            if (File.Exists(filePath))
            {
                Debug.Log("IM IN");
                FileStream file = new FileStream(filePath, FileMode.Open);

                TurretData data = formatter.Deserialize(file) as TurretData;

                file.Close();

                Vector3 position = new Vector3(data.Position[0], data.Position[1], data.Position[2]);

                Debug.Log(data.TurretName);

                if (data.TurretName == "Rotate")
                {
                    Instantiate(beeTurretPrefab, position, Quaternion.identity);
                }
            }
            else
            {
                Debug.LogError("Path not found in " + filePath);
            }
        }

    }
}
