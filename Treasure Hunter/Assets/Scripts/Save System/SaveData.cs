using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    public float[] position;
    public float health;
    public int diamond;
    public int coin;
    public int activeScene;
    public List<string> gameObjectName = new List<string>();

    public SaveData(GameObject player, GameManager gm, List<string> c)
    {
        Health h = player.GetComponent<Health>();

        diamond = gm.DiamondCollected;
        coin = gm.CoinCollected;
        gameObjectName = new List<string>(c);
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        health = h.currentHealth;
        activeScene = SceneManager.GetActiveScene().buildIndex;
    }
}
