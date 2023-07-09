using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMaster : MonoBehaviour
{
    private static CheckpointMaster instance;
    public List<int> collect = new List<int>(){0,0};
    public List<string> obj;
    public Vector2 lastCheckpointPos;


    // Start is called before the first frame update
    private void Awake() {
        if(instance == null){
            instance = this;
            if (collect.Count == 0){
                collect.Add(0);
                collect.Add(0);
            }
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
}
