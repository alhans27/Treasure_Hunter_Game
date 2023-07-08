using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ChestInventory.Model;
using Inventory.Model;
using UnityEngine;

public class KnapsackAlgoritm : MonoBehaviour
{
    // public Item[] items;
    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField]
    private ChestInventorySO chestInventoryData;

    private List<ItemInventory> items;

    private int minValue;
    private int maxWeight;

    public int resultmaxValue { get; private set; }

    private List<int> resultItems;

    private int[,] dpTable;

    void Awake()
    {
        items = inventoryData.GetAllItems();
        minValue = chestInventoryData.minValue;
        maxWeight = chestInventoryData.maxWeight;

        dpTable = new int[items.Count + 1, maxWeight + 1];
        SolveKnapsack();
    }

    public void SolveKnapsack()
    {
        // Inisialisasi baris pertama dengan 0
        for (int i = 0; i <= maxWeight; i++)
        {
            dpTable[0, i] = 0;
        }

        // Mengisi tabel DP untuk setiap item
        for (int i = 1; i <= items.Count; i++)
        {
            for (int j = 0; j <= maxWeight; j++)
            {
                if (items[i - 1].item.ItemWeight <= j)
                {
                    dpTable[i, j] = Mathf.Max(items[i - 1].item.ItemValue + dpTable[i - 1, j - items[i - 1].item.ItemWeight], dpTable[i - 1, j]);
                }
                else
                {
                    dpTable[i, j] = dpTable[i - 1, j];
                }
            }
        }

        // Menampilkan hasil
        int maxValue = dpTable[items.Count, maxWeight];
        resultmaxValue = maxValue;
        // List<int> resultItems = new List<int>();

        // if (maxValue >= minValue)
        // {
        //     // Mencari item yang dipilih
        //     int remainingCapacity = maxWeight;
        //     for (int i = items.Count, j = maxWeight; i > 0 && maxValue > 0; i--)
        //     {
        //         if (maxValue != dpTable[i - 1, j])
        //         {
        //             resultItems.Add(items[i - 1].item.ID);
        //             int itemID = items[i - 1].item.ID;
        //             maxValue -= items[i - 1].item.ItemValue;
        //             j -= items[i - 1].item.ItemWeight;
        //         }
        //     }
        // }
        // else
        // {
        // }
        Debug.Log(resultmaxValue);
    }

    public List<int> GetResultItem()
    {
        return resultItems;
    }
}
