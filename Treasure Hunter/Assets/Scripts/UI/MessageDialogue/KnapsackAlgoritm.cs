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
        Debug.Log("Max Value: " + maxValue);
        Debug.Log(dpTable);

        if (maxValue >= minValue)
        {
            Debug.Log("Solution meets the minimum value requirement.");

            // Mencari item yang dipilih
            int remainingCapacity = maxWeight;
            for (int i = items.Count, j = maxWeight; i > 0 && maxValue > 0; i--)
            {
                if (maxValue != dpTable[i - 1, j])
                {
                    Debug.Log("Selected Item: " + items[i - 1].item.Name);
                    maxValue -= items[i - 1].item.ItemValue;
                    j -= items[i - 1].item.ItemWeight;
                }
            }
        }
        else
        {
            Debug.Log("Solution does not meet the minimum value requirement.");
        }
    }
}
