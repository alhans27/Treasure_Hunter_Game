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
    public Text CoinOutput;
    private List<ItemInventory> backpackData;

    private InventorySO inventoryData { get; set; }

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
        CoinOutput.text = CoinCollected.ToString();
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
}

