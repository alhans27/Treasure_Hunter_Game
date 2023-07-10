using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    public float[] position;
    public float[] positionCheckpoint;
    public float health;
    public int diamond;
    public int coin;
    public int activeScene;
    public List<int> collectCheckpoint;
    public List<string> gameObjectName;
    public List<string> checkpointName;
    public List<string> objectCheckpoint;

    public SaveData(GameObject player, GameManager gm, CheckpointMaster cm, List<string> c)
    {
        Health h = player.GetComponent<Health>();
        coin = gm.CoinCollected;
        collectCheckpoint = new List<int>(cm.collect);
        gameObjectName = new List<string>(c);
        checkpointName = new List<string>(cm.checkpos);
        objectCheckpoint = new List<string>(cm.obj);
        
        positionCheckpoint = new float[2];
        positionCheckpoint[0] = cm.lastCheckpointPos.x;
        positionCheckpoint[1] = cm.lastCheckpointPos.y;
        
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        health = h.currentHealth;
        activeScene = SceneManager.GetActiveScene().buildIndex;
    }
}
