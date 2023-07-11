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
        GameObject player = GameObject.Find("Player");
        GameManager gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        List<string> c = Checkpoint.goName;
        CheckpointMaster cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointMaster>();

        string path = Application.persistentDataPath + SAVE_SUB;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(player, gm, cm, c);

        formatter.Serialize(stream, data);
        stream.Close();
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

            var op = SceneManager.LoadSceneAsync(data.activeScene);
            op.completed += (x) => {
                Health health = GameObject.Find("Player").GetComponent<Health>();
                CheckpointMaster cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointMaster>();

                health.LoadHealth(data.health);
                cm.Load(data);
            };

        }
    }
}