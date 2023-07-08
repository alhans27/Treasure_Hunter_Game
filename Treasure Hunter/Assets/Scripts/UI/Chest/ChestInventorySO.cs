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
        public int minValue { get; set; } = 300;

        [field: SerializeField]
        public int maxWeight { get; set; } = 10;

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
            UpdateDataUI?.Invoke(index, newItem);
        }
    }
}