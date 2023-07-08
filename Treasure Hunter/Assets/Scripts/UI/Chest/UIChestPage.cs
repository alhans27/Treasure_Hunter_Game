using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChestInventory.UI
{
    public class UIChestPage : MonoBehaviour
    {
        [SerializeField]
        private UIChestItem itemPrefab;

        // [SerializeField]
        // private UIInventoryDescItem itemDesc;

        [SerializeField]
        private RectTransform contentPanel;

        List<UIChestItem> listItems = new List<UIChestItem>();
        private int currentlyDraggedItemIndex = -1;

        private void Awake()
        {
            Hide();
            // itemDesc.ResetDesc();
        }
        public void InitializeInventoryUI(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIChestItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                item.transform.SetParent(contentPanel);
                listItems.Add(item);
                // item.onItemEndDrag += HandleEndDrag;
                // item.onItemDroppedOn += HandleSwap;
            }
        }

        // public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        // {
        //     if (listItems.Count > itemIndex)
        //     {
        //         listItems[itemIndex].SetData(itemImage, itemQuantity);
        //     }
        // }

        // private void HandleSwap(UIChestItem item)
        // {
        //     int index = listItems.IndexOf(item);
        //     if (index == -1)
        //     {
        //         return;
        //     }

        //     OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
        //     HandleItemSelection(item);
        // }

        // private void ResetDraggedItem()
        // {
        //     mouseFollower.Toggle(false);
        //     currentlyDraggedItemIndex = -1;
        // }

        // private void HandleEndDrag(UIChestItem item)
        // {
        //     ResetDraggedItem();
        // }
        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
            // ResetDraggedItem();
        }

        // internal void UpdateDesc(int itemIndex, Sprite itemImage, string name, string description)
        // {
        //     itemDesc.SetDesc(itemImage, name, description);
        //     DeselectAllItems();
        //     listItems[itemIndex].Select();
        // }

        internal void ResetAllItems()
        {
            foreach (var item in listItems)
            {
                item.ResetData();
            }
        }
    }
}
