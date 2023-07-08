using System;
using System.Collections.Generic;
using Inventory.UI;
using UnityEngine;

namespace ChestInventory.UI
{
    public class UIChestPage : MonoBehaviour
    {
        [SerializeField]
        private UIChestItem itemPrefab;

        [SerializeField]
        private UIChestDesc description;

        [SerializeField]
        private RectTransform contentPanel;

        List<UIChestItem> listItemsUI = new List<UIChestItem>();
        public event Action<int> OnDropItems;

        // Dijalankan pertama kali sebelum Start
        private void Awake()
        {
            // Menyembunyikan UI Chest Inventory
            Hide();
        }

        // Membuat Slot Item Kosong untuk Pertama Kali dan Mengatur Deskripsi dari Chest Inventory
        public void InitializeInventoryUI(int inventorysize, int minValue, int maxWeight)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIChestItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                item.transform.SetParent(contentPanel);
                listItemsUI.Add(item);

                // Even Handling
                item.onItemDroppedOn += HandleDrop;
            }
            description.SetDesc(minValue, 0, maxWeight, 0);

        }

        private void HandleDrop(UIChestItem item)
        {
            int dest_index = listItemsUI.IndexOf(item);
            if (dest_index == -1)
                return;

            OnDropItems?.Invoke(dest_index);
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listItemsUI.Count > itemIndex)
            {
                listItemsUI[itemIndex].SetItemUI(itemImage, itemQuantity);
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        internal void ResetAllItems()
        {
            foreach (var item in listItemsUI)
            {
                item.ResetItemUI();
            }
        }
    }
}
