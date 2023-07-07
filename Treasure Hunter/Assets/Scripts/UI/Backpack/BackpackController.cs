using System;
using System.Collections.Generic;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

namespace Inventory
{
    public class BackpackController : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryBackpack inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        public List<ItemInventory> intialItems = new List<ItemInventory>();

        public void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }
        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (ItemInventory item in intialItems)
            {
                if (item.IsEmpty)
                    continue;
                this.inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, ItemInventory> inventoryState)
        {
            this.inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                this.inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            this.inventoryUI.OnDescRequested += HandleDescRequest;
            this.inventoryUI.OnStartDragging += HandleDragging;
            this.inventoryUI.OnSwapItems += HandleSwapItems;
            this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;

        }

        private void HandleItemActionRequest(int itemIndex)
        {
            throw new NotImplementedException();
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDragging(int itemIndex)
        {
            ItemInventory inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            this.inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleDescRequest(int itemIndex)
        {
            ItemInventory inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            this.inventoryUI.UpdateDesc(itemIndex, item.ItemImage, item.name, item.Description);
            {

            }

        }

        public void Update()
        {
            if (Input.GetButtonDown("Backpack"))
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                }
                else
                {
                    inventoryUI.Hide();
                }
            }
        }
    }
}