using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<ItemInventory> inventoryItems;

        [field: SerializeField]
        public int Size { get; set; } = 5;

        public void Initialize()
        {
            inventoryItems = new List<ItemInventory>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(ItemInventory.GetEmptyItem());
            }
        }

        public void AddItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = new ItemInventory
                    {
                        item = item,
                        quantity = quantity,
                    };
                }
            }
        }

        public Dictionary<int, ItemInventory> GetCurrentInventoryState()
        {
            Dictionary<int, ItemInventory> returnValue = new Dictionary<int, ItemInventory>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public ItemInventory GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }
    }

    [Serializable]
    public struct ItemInventory
    {
        public int quantity;
        public ItemSO item;
        public bool IsEmpty => item == null;

        public ItemInventory ChangeQuantity(int newQuantity)
        {
            return new ItemInventory
            {
                item = this.item,
                quantity = newQuantity,
            };
        }

        public static ItemInventory GetEmptyItem()
            => new ItemInventory
            {
                item = null,
                quantity = 0,
            };
    }
}
