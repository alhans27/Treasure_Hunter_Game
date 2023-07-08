using System;
using System.Collections;
using System.Collections.Generic;
using ChestInventory.Model;
using ChestInventory.UI;
using Inventory;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

namespace ChestInventory
{
    public class ChestController : MonoBehaviour
    {
        [SerializeField]
        private UIChestPage chestInventoryUI;

        [SerializeField]
        private ChestInventorySO chestInventoryData;

        [SerializeField]
        private BackpackController backpack;

        private BoxCollider2D coll;

        private void Awake()
        {
            HideChest();
            PrepareUI();
            PrepareInventoryData();
            coll = GetComponent<BoxCollider2D>();
        }

        private void PrepareUI()
        {
            this.chestInventoryUI.InitializeInventoryUI(chestInventoryData.Size, chestInventoryData.minValue, chestInventoryData.maxWeight);


            this.chestInventoryUI.OnDropItems += HandleDropItem;
        }

        private void PrepareInventoryData()
        {
            this.chestInventoryData.Initialize();
            this.chestInventoryData.UpdateDataUI += UpdateUI;
            this.chestInventoryData.OnCurrentValueUpdated += UpdateCurrentValueWeight;
        }

        private void UpdateUI(Dictionary<int, ItemInventory> inventoryState)
        {
            // Menjadikan Slot Item yang sebelumnya dipindah menjadi Slot Item Kosong
            this.chestInventoryUI.ResetAllItems();

            // Memperbaharui UI Backpack Inventory
            foreach (var item in inventoryState)
            {
                this.chestInventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void UpdateCurrentValueWeight(Dictionary<string, int> dictionary)
        {
            this.chestInventoryUI.UpdateChestDesc(dictionary);
        }

        private void HandleDropItem(int index)
        {
            ItemInventory item = backpack.selectedItem;
            this.chestInventoryData.AddItem(index, item);
        }

        public void ResetData()
        {
            this.chestInventoryData.ResetData();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void HideChest()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                chestInventoryUI.Show();
            }
        }

        private void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                chestInventoryUI.Hide();
            }
        }

        internal void DisableColl()
        {
            coll.enabled = false;
        }
    }

}