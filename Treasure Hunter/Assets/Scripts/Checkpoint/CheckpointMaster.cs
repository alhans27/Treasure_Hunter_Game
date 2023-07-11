using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMaster : MonoBehaviour
{
    private static CheckpointMaster instance;
    public static bool isLoaded = false;
    public int coin;
    public int coin_load;
    public List<string> obj;
    public List<string> obj_load;
    public List<string> checkpos;
    public Vector2 lastCheckpointPos;
    public Vector3 loadCheckpointPos;
    public GameManager gm;


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void SaveCoin(int coins)
    {
        coin = coins;
    }

    public void SaveObj()
    {
        obj = new List<string>(Checkpoint.goName);
    }

    public void Load(SaveData data)
    {
        obj.Clear();
        obj_load.Clear();

        obj = new List<string>(data.objectCheckpoint);
        obj_load = new List<string>(data.gameObjectName);
        checkpos = new List<string>(data.checkpointName);

        coin = data.coinCheckpoint;
        coin_load = data.coin;

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
