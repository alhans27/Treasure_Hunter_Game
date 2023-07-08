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
        private List<InventoryItem> listInventoryItems;

        [field: SerializeField]
        public int Size { get; set; } = 3;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public void Initialize()
        {
            listInventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                listInventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public void AddItem(InventoryItem item)
        {
            // AddItem(item.item, item.quantity);
            Debug.Log("Item ditambahkan");
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;

        public bool IsEmpty => item == null;

        public static InventoryItem GetEmptyItem()
            => new InventoryItem
            {
                item = null,
                quantity = 0,
            };
    }

}