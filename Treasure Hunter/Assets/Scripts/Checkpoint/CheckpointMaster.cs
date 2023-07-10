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
    public Vector2 lastCheckpointPos;
    public Vector3 loadCheckpointPos;
    public GameManager gm;


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (collect.Count == 0)
            {
                collect.Add(0);
            }
            gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void SaveCollect(int coin)
    {
        collect.Clear();
        collect.Insert(0, coin);
    }

    public void SaveObj()
    {
        obj = new List<string>(Checkpoint.goName);
    }

    public void Load(List<string> Ob, int diamond, int coin, Vector3 pos)
    {
        obj_load.Clear();
        obj_load = new List<string>(Ob);
        collect_load.Clear();
        collect_load.Insert(0, coin);
        loadCheckpointPos = pos;
        isLoaded = true;
    }
}
