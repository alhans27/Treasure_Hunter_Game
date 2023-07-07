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
        public void InitializeInventoryUI(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIInventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                item.transform.SetParent(contentPanel);
                listItems.Add(item);
                item.onItemClicked += HandleItemSelection;
                item.onItemRightMouseClicked += HandleShowItemActions;
                item.onItemBeginDrag += HandleBeginDrag;
                item.onItemEndDrag += HandleEndDrag;
                item.onItemDroppedOn += HandleSwap;
            }
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listItems.Count > itemIndex)
            {
                listItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleSwap(UIInventoryItem item)
        {
            int index = listItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }

            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
            HandleItemSelection(item);
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleEndDrag(UIInventoryItem item)
        {
            ResetDraggedItem();
        }

        private void HandleBeginDrag(UIInventoryItem item)
        {
            int index = listItems.IndexOf(item);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;
            HandleItemSelection(item);
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

        private void HandleItemSelection(UIInventoryItem item)
        {
            int index = listItems.IndexOf(item);
            if (index == -1)
                return;
            OnDescRequested?.Invoke(index);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        public void ResetSelection()
        {
            itemDesc.ResetDesc();
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listItems)
            {
                item.Deselect();
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            ResetDraggedItem();
        }

        internal void UpdateDesc(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDesc.SetDesc(itemImage, name, description);
            DeselectAllItems();
            listItems[itemIndex].Select();
        }

        internal void ResetAllItems()
        {
            foreach (var item in listItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}
