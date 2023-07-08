using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

namespace ChestInventory.Model
{
    [CreateAssetMenu]
    public class ChestInventorySO : ScriptableObject
    {
        [SerializeField]
        private List<ItemInventory> listItemInventory;

        [field: SerializeField]
        public int Size { get; set; } = 3;

        [field: SerializeField]
        public int minValue { get; set; } = 1000;

        [field: SerializeField]
        public int maxWeight { get; set; } = 20;

        public int totalValue { get; set; }
        public int totalWeight { get; set; }

        public event Action<Dictionary<string, int>> OnCurrentValueUpdated;

        public event Action<int, ItemInventory> UpdateDataUI;

        public void Initialize()
        {
            listItemInventory = new List<ItemInventory>();
            for (int i = 0; i < Size; i++)
            {
                listItemInventory.Add(ItemInventory.GetEmptyItem());
            }
        }

        public void AddItem(int index, ItemInventory item)
        {
            ItemInventory newItem = new ItemInventory
            {
                item = item.item,
                quantity = 1
            };
            listItemInventory[index] = newItem;
            UpdateInformData();
            UpdateDataUI?.Invoke(index, newItem);
        }

        public Dictionary<string, int> GetTotalValueWeightItem()
        {
            Dictionary<string, int> returnValue = new Dictionary<string, int>();

            // resetTotalValue and TotalWeight
            totalValue = 0;
            totalWeight = 0;

            for (int i = 0; i < listItemInventory.Count; i++)
            {
                if (listItemInventory[i].IsEmpty)
                    continue;
                totalValue += listItemInventory[i].item.ItemValue;
                totalWeight += listItemInventory[i].item.ItemWeight;
            }
            returnValue["value"] = totalValue;
            returnValue["weight"] = totalWeight;
            return returnValue;
        }

        private void UpdateInformData()
        {
            OnCurrentValueUpdated?.Invoke(GetTotalValueWeightItem());
        }

        public int GetDataLength()
        {
            int totalItem = 0;
            for (int i = 0; i < listItemInventory.Count; i++)
            {
                if (listItemInventory[i].IsEmpty)
                    continue;
                totalItem++;
            }
            return totalItem;
        }
    }
}