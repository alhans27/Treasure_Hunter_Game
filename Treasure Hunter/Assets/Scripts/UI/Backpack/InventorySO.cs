using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
        {
            ItemInventory newItem = new ItemInventory
            {
                item = item,
                quantity = quantity
            };

            for (int i = 0; i < listInventoryItems.Count; i++)
            {
                if (listInventoryItems[i].IsEmpty)
                {
                    listInventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }
        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < listInventoryItems.Count; i++)
            {
                if (listInventoryItems[i].IsEmpty)
                    continue;
                if (listInventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake = listInventoryItems[i].item.MaxStackSize - listInventoryItems[i].quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        listInventoryItems[i] = listInventoryItems[i].ChangeQuantity(listInventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        listInventoryItems[i] = listInventoryItems[i].ChangeQuantity(listInventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }

        private bool IsInventoryFull()
            => listInventoryItems.Where(item => item.IsEmpty).Any() == false;

        public void AddItem(ItemInventory item)
        {
            AddItem(item.item, item.quantity);
        }

        public int AddItem(ItemSO item, int quantity)
        {
            if (item.IsStackable == false)
            {
                for (int i = 0; i < listInventoryItems.Count; i++)
                {
                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1);
                    }
                    InformAboutChange();
                    return quantity;
                }
            }
            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

        // Array Assosiation dengan Key berupa Integer dan Value berupa struck Class ItemInventory
        public Dictionary<int, ItemInventory> GetCurrentInventoryState()
        {
            // Mendefinisikan sebuah Dictionary bernama returnValue
            Dictionary<int, ItemInventory> returnValue = new Dictionary<int, ItemInventory>();

            for (int i = 0; i < listInventoryItems.Count; i++)
            {
                // Jika Item dalam List Item pada Model Inventory Kosong, maka tidak terjadi apa-apa
                if (listInventoryItems[i].IsEmpty)
                    continue;

                // Jika ada Slot Item Bergambar pada Model Inventory, maka dimasukkan ke dalam Dictionary returnValue
                returnValue[i] = listInventoryItems[i];
            }
            return returnValue;
        }

        // Mengambil Item tertentu dari List Inventory berdasarkan index
        public ItemInventory GetItemAt(int itemIndex)
        {
            return listInventoryItems[itemIndex];
        }

        public List<ItemInventory> GetAllItems()
        {
            List<ItemInventory> items = new List<ItemInventory>();
            for (int i = 0; i < listInventoryItems.Count; i++)
            {
                if (listInventoryItems[i].IsEmpty)
                    continue;
                items.Add(listInventoryItems[i]);
            }
            return items;
        }

        // Menukar Index dari Item yang di Swap
        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            ItemInventory item1 = listInventoryItems[itemIndex_1];
            listInventoryItems[itemIndex_1] = listInventoryItems[itemIndex_2];
            listInventoryItems[itemIndex_2] = item1;

            // Update Tampilan UI Backpack Inventory
            InformAboutChange();
        }

        // Mengupdate Tampilan UI Backpack Inventory
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

        // Jika item bernilai NULL maka nilai dari property IsEmpty adalah TRUE
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
