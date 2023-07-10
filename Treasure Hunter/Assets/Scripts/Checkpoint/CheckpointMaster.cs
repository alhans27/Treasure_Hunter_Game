using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMaster : MonoBehaviour
{
    private static CheckpointMaster instance;
    public static bool isLoaded = false;
    public List<int> collect = new List<int>();
    public List<int> collect_load = new List<int>();
    public List<string> obj;
    public List<string> obj_load;
    public List<string> checkpos;
    public Vector2 lastCheckpointPos;
    public Vector3 loadCheckpointPos;
    public GameManager gm;


    // Start is called before the first frame update
    private void Awake() {
        if(instance == null){
            instance = this;
            if (collect.Count == 0){
                collect.Add(0);
                collect.Add(0);
            }
            gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
            DontDestroyOnLoad(instance);
        } else {
            Destroy(gameObject);
        }

    }

    public void SaveCollect(int diamond, int coin)
    {
        collect.Clear();
        collect.Insert(0, diamond);
        collect.Insert(1, coin);
    }

    public void SaveObj()
    {
        obj = new List<string>(Checkpoint.goName);
    }

    public void Load(SaveData data) 
    {
        obj.Clear();
        obj_load.Clear();
        collect_load.Clear();
        collect = new List<int>(data.collectCheckpoint);
        obj = new List<string>(data.objectCheckpoint);
        obj_load = new List<string>(data.gameObjectName);
        checkpos = new List<string>(data.checkpointName);

        collect_load.Insert(0, data.diamond);
        collect_load.Insert(1, data.coin);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        loadCheckpointPos = position;

        Vector2 pos;
        pos.x = data.positionCheckpoint[0];
        pos.y = data.positionCheckpoint[1];
        lastCheckpointPos = pos;

        isLoaded = true;
    }
}
