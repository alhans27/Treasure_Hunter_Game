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

        private void Awake()
        {
            HideChest();
            PrepareUI();
            PrepareInventoryData();
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

        private void UpdateUI(int index, ItemInventory inventory)
        {
            this.chestInventoryUI.UpdateData(index, inventory.item.ItemImage, inventory.quantity);
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
                Debug.Log("Player Mengenai Collider");
                chestInventoryUI.Show();
            }
        }
    }

}