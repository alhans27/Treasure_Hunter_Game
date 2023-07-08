using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryBackpack : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem itemPrefab;

        [SerializeField]
        private UIInventoryDescItem itemDesc;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private MouseFollower mouseFollower;

        List<UIInventoryItem> listItems = new List<UIInventoryItem>();
        private int currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescRequested, OnItemActionRequested, OnStartDragging;
        public event Action<int, int> OnSwapItems;

        private void Awake()
        {
            Hide();
            mouseFollower.Toggle(false);
            itemDesc.ResetDesc();
        }

        // Membuat UI untuk Slot Kosong pada Backpack Inventory
        public void InitializeInventoryUI(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                // Instansiasi Tempat Item dan Mengatur Posisinya agar sesuai
                UIInventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                item.transform.SetParent(contentPanel);
                listItems.Add(item);

                // Event Handling
                item.onItemClicked += HandleItemSelection;                          // Ketika User mengklik sesuatu, jalankan HandleItemSelection
                item.onItemRightMouseClicked += HandleShowItemActions;
                item.onItemBeginDrag += HandleBeginDrag;                            // Ketika User mulai men-Drag sebuah Item Bergambar, jalankan HandleBeginDrag
                item.onItemEndDrag += HandleEndDrag;                                // Ketika User mulai melepas Drag sebuah Item Bergambar, jalankan HandleEndDrag
                item.onItemDroppedOn += HandleSwap;                                 // Ketika User men-Drop sebuah Item, jalankan HandleSwap
            }
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            // Jika Indexnya ada dalam List
            if (listItems.Count > itemIndex)
            {
                listItems[itemIndex].SetItemUI(itemImage, itemQuantity);
            }
        }

        // Event Handling ketika User Mulai Men-Drop Sebuah Item di Sebuah Slot Item, Diambil Index Tujuan dari Item tersebut dalam List Inventory
        private void HandleSwap(UIInventoryItem item)
        {
            int dest_index = listItems.IndexOf(item);
            if (dest_index == -1)
            {
                return;
            }

            // Menjalankan sebuah Action untuk memindah Index Slot Item yang di Drag menjadi Index Slot Item Tujuan
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, dest_index);
            // Untuk Menampilkan UI Deskripsi Item dari Item yang baru di Drop
            HandleItemSelection(item);
        }

        // Event Handling ketika User Melepas Drag Sebuah Item
        private void HandleEndDrag(UIInventoryItem item)
        {
            ResetDraggedItem();
        }

        // Menghilangkan Gambar Slot Item di Kursor Mouse
        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        // Event Handling ketika User Mulai Men-Drag Sebuah Item, Diambil Index dari Item tersebut dalam List Inventory
        private void HandleBeginDrag(UIInventoryItem item)
        {
            int index = listItems.IndexOf(item);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;

            // Untuk Menampilkan UI Deskripsi Item dari Item yang di Drag
            HandleItemSelection(item);

            // Menjalankan sebuah Action untuk mulai Drag Item berdasarkan indexnya dalam List UI Inventory
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleShowItemActions(UIInventoryItem item)
        {
            int index = listItems.IndexOf(item);
            if (index == -1)
                return;
            OnItemActionRequested?.Invoke(index);
        }

        // Ketika User mengklik sebuah Slot Item Bergambar
        private void HandleItemSelection(UIInventoryItem item)
        {
            // Mendapatkan Index dari Slot Item yang di Klik pada urutan List di Backpack Inventory
            int index = listItems.IndexOf(item);
            if (index == -1)
                return;

            // Menjalankan sebuah Action untuk menampilkan Deskripsi Item
            OnDescRequested?.Invoke(index);
        }

        // Mereset UI Deskripsi Item menjadi Kosong dan Men-Deselect Slot Item Bergambar yang sebelumnya dipilih
        public void ResetSelection()
        {
            // Reset UI Deskripsi Item
            itemDesc.ResetDesc();

            // Deselect Semua Item dalam Inventory
            DeselectAllItems();
        }

        // Deselect Semua Item dalam Inventory
        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listItems)
            {
                item.Deselect();
            }
        }

        // Menampilkan UI Backpack Inventory
        public void Show()
        {
            gameObject.SetActive(true);
            // Mereset UI Deskripsi Item dan UI Selected Item
            ResetSelection();
        }

        // Menyembunyikan UI Backpack Inventory
        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        internal void UpdateDesc(int itemIndex, Sprite itemImage, string name, string description, int valueItem, int weightItem)
        {
            itemDesc.SetDesc(itemImage, name, description, valueItem, weightItem);
            DeselectAllItems();
            listItems[itemIndex].Select();
        }

        internal void ResetAllItems()
        {
            foreach (var item in listItems)
            {
                item.ResetItemUI();
                item.Deselect();
            }
        }
    }
}
