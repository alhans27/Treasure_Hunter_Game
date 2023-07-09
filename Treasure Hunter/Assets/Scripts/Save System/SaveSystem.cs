using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    const string SAVE_SUB = "/treasure_hunter";

    public static void SaveGame()
    {
        GameManager gm = GameObject.Find("Game Manager").GetComponent<GameManager>();

        string path = Application.persistentDataPath + SAVE_SUB;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(gm);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log(File.Exists(path));
    }

    public static void LoadGame()
    {
        string path = Application.persistentDataPath + SAVE_SUB;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log(data);            
        }
        
    }
}
