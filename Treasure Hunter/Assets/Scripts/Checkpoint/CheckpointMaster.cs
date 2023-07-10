using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMaster : MonoBehaviour
{
    private static CheckpointMaster instance;
    public List<int> collect = new List<int>(){0,0};
    public List<string> obj;
    public Vector2 lastCheckpointPos;
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

    public void LoadObject(List<string> Ob) 
    {
        obj = new List<string>(Ob);
    }

    public void LoadItems (int diamond, int coin)
    {
        gm.DiamondCollected = diamond;
        gm.CoinCollected = coin;
        Debug.Log(coin);
    }
}
