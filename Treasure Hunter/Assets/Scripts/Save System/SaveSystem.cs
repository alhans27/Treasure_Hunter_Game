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

        string path = Application.persistentDataPath + SAVE_SUB;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(player, gm, c);

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

            // SceneManager.LoadScene(data.activeScene);

            var op = SceneManager.LoadSceneAsync(data.activeScene);
            op.completed += (x) => {
                // PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
                Health health = GameObject.Find("Player").GetComponent<Health>();
                // Checkpoint c = GameObject.Find("Checkpoint").GetComponent<Checkpoint>();
                CheckpointMaster cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointMaster>();

                Vector3 position;
                position.x = data.position[0];
                position.y = data.position[1];
                position.z = data.position[2];
                // player.LoadPos(position);
                health.LoadHealth(data.health);
                cm.Load(data.gameObjectName, data.diamond, data.coin, position);
                // cm.LoadObject();
            };

        }
    }
}