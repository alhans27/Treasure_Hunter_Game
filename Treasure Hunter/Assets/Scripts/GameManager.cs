using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Inventory.Model;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int CoinCollected = 0;
    private List<ItemInventory> backpackData = new List<ItemInventory>();

    private InventorySO inventoryData { get; set; }

    public int LevelIndex { get; set; }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GameManager";
                    instance = obj.AddComponent<GameManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    private void Update()
    {
        LevelIndex = GetLevelIndex();
        backpackData = GetBackpack();
    }

    public void CoinCollection()
    {
        CoinCollected++;
    }

    public List<ItemInventory> GetBackpack()
    {
        return backpackData;
    }

    public void SetBackpack(List<ItemInventory> x)
    {
        backpackData = x;
    }
    public void ResetBackpack()
    {
        backpackData = new List<ItemInventory>();
    }

    public int GetLevelIndex()
    {
        return LevelIndex;
    }

    public void SetLevelIndex(int index)
    {
        LevelIndex = index;
    }
}

