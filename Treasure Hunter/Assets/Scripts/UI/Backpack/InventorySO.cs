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
        private List<ItemInventory> listInventoryItems;

        [field: SerializeField]
        public int Size { get; set; } = 5;

        public event Action<Dictionary<int, ItemInventory>> OnInventoryUpdated;

        public void Initialize()
        {
            listInventoryItems = new List<ItemInventory>();
            for (int i = 0; i < Size; i++)
            {
                listInventoryItems.Add(ItemInventory.GetEmptyItem());
            }
        }

        public void AddItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < listInventoryItems.Count; i++)
            {
                if (listInventoryItems[i].IsEmpty)
                {
                    listInventoryItems[i] = new ItemInventory
                    {
                        item = item,
                        quantity = quantity,
                    };
                    return;
                }
            }
        }
        public void AddItem(ItemInventory item)
        {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, ItemInventory> GetCurrentInventoryState()
        {
            Dictionary<int, ItemInventory> returnValue = new Dictionary<int, ItemInventory>();

            for (int i = 0; i < listInventoryItems.Count; i++)
            {
                if (listInventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = listInventoryItems[i];
            }
            return returnValue;
        }

        public ItemInventory GetItemAt(int itemIndex)
        {
            return listInventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            ItemInventory item1 = listInventoryItems[itemIndex_1];
            listInventoryItems[itemIndex_1] = listInventoryItems[itemIndex_2];
            listInventoryItems[itemIndex_2] = item1;
            InformAboutChange();

        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
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
