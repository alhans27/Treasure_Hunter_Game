using System;
using System.Collections;
using System.Collections.Generic;
using ChestInventory.Model;
using ChestInventory.UI;
using UnityEngine;

namespace ChestInventory
{
    public class ChestController : MonoBehaviour
    {
        [SerializeField]
        private UIChestPage chestInventoryUI;

        [SerializeField]
        private ChestInventorySO chestInventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        private void Awake()
        {
            Hide();
            PrepareUI();
        }

        private void PrepareUI()
        {
            this.chestInventoryUI.InitializeInventoryUI(chestInventoryData.Size);
        }

        private void PrepareInventoryData()
        {
            this.chestInventoryData.Initialize();
            // this.chestInventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                this.chestInventoryData.AddItem(item);
            }
        }

        // private void UpdateInventoryUI(Dictionary<int, InventoryItem> dictionary)
        // {
        //     this.chestInventoryUI.ResetAllItems();
        //     foreach (var item in dictionary)
        //     {

        //     }
        // }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
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