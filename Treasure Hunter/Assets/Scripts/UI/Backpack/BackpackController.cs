using System;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

namespace Inventory
{
    public class BackpackController : MonoBehaviour
    {
        [SerializeField]
        private BackpackInventory inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        public void Start()
        {
            PrepareUI();
            // inventoryData.Initialize(); --> Disable because the data was Updated
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
            throw new NotImplementedException();
        }

        private void HandleDragging(int itemIndex)
        {
            throw new NotImplementedException();
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