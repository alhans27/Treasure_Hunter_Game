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

        public ItemInventory selectedItem { get; set; }

        public void Start()
        {
            // intialItems = GameManager.Instance.GetBackpack();
            // if (intialItems.)
            // intialItems = new List<ItemInventory>();
            PrepareUI();
            PrepareInventoryData();
            // Debug.Log(intialItems);
        }

        public void Update()
        {
            // Jika Player Menekan Keyboard "B" maka Backpack Inventory akan terbuka
            if (Input.GetButtonDown("Backpack"))
            {
                // Jika UI Backpack Inventory Belum Active
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    // Menjalankan perintah untuk menampilkan UI Backpack Inventory
                    inventoryUI.Show();

                    // Menjalankan perintah untuk Update UI Slot Item berdasarkan data yang sudah tersimpan pada Model InventorySO
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                }
                // Jika UI Backpack Inventory Sudah Active
                else
                {
                    // Menjalankan perintah untuk menutup UI Backpack Inventory
                    inventoryUI.Hide();
                }
            }
        }

        // Menyiapkan UI dari Backpack Inventory Ketika Pertama Kali
        private void PrepareUI()
        {
            // Menjalankan perintah untuk membuat UI slot item kosong
            inventoryUI.InitializeInventoryUI(inventoryData.Size);

            // Event Handling
            this.inventoryUI.OnDescRequested += HandleDescRequest;              // Ketika ada Action untuk menampilkan Deskripsi Item, jalankan HandleDescRequest
            this.inventoryUI.OnStartDragging += HandleDragging;                 // Ketika ada Action untuk menampilkan Deskripsi Item, jalankan HandleDragging
            this.inventoryUI.OnSwapItems += HandleSwapItems;                    // Ketika ada Action untuk memindah posisi Slot Item Bergambar dalam List Inventory, jalankan HandleSwapItems
            this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        // Menyiapkan Model Data dari Backpack Inventory Ketika Pertama Kali
        private void PrepareInventoryData()
        {
            this.inventoryData.Initialize();
            this.inventoryData.OnInventoryUpdated += UpdateInventoryUI;         // Event Handling
            foreach (ItemInventory item in intialItems)
            {
                if (item.IsEmpty)
                    continue;
                this.inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, ItemInventory> inventoryState)
        {
            if (inventoryState != null)
            {
                // Menjadikan Slot Item yang sebelumnya dipindah menjadi Slot Item Kosong
                this.inventoryUI.ResetAllItems();
            }

            // Memperbaharui UI Backpack Inventory
            foreach (var item in inventoryState)
            {
                this.inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            throw new NotImplementedException();
        }

        // Event Handling untuk memindah index dr Item yang di Drag ke index tujuan
        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        // Event Handling untuk menangani Dragging UI Item berdasarkan Index dari Item dalam List
        private void HandleDragging(int itemIndex)
        {
            selectedItem = inventoryData.GetItemAt(itemIndex);
            if (selectedItem.IsEmpty)
                return;
            // Perintah untuk menampilkan UI Slot Item yang di Drag mengikuti Mouse
            this.inventoryUI.CreateDraggedItem(selectedItem.item.ItemImage, selectedItem.quantity);
        }

        // Event Handling untuk menangani request UI Deskripsi Item berdasarkan Index dari Item dalam List
        private void HandleDescRequest(int itemIndex)
        {
            // Menyimpan sebuah Item yang di Select ke dalam var inventoryItem berupa struck Class dari sebuah Model sebagai penyimpanan sementara
            ItemInventory inventoryItem = inventoryData.GetItemAt(itemIndex);

            // Jika Slot Item yang dipilih Kosong
            if (inventoryItem.IsEmpty)
            {
                // Deselect semua Item dan reset UI Deskripsi Item
                inventoryUI.ResetSelection();
                return;
            }
            // Jika Slot Item yang dipilih Tidak Kosong
            // Menyimpan Data-data Item yang dipilih ke dalam var item yang merupakan SriptableObject
            ItemSO item = inventoryItem.item;

            // Menjalankan perintah untuk Update UI Deskripsi Item sesuai dengan Item yang dipilih
            this.inventoryUI.UpdateDesc(itemIndex, item.ItemImage, item.name, item.Description, item.ItemValue, item.ItemWeight);
        }
    }
}