using System;
using System.Collections.Generic;
using UnityEngine;

public class BackpackInventory : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private InventoryDescItem itemDesc;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private MouseFollower mouseFollower;

    List<InventoryItem> listItems = new List<InventoryItem>();
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
            InventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
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

    private void HandleSwap(InventoryItem item)
    {
        int index = listItems.IndexOf(item);
        if (index == -1)
        {
            return;
        }

        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleEndDrag(InventoryItem item)
    {
        ResetDraggedItem();
    }

    private void HandleBeginDrag(InventoryItem item)
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

    private void HandleShowItemActions(InventoryItem item)
    {
        int index = listItems.IndexOf(item);
        if (index == -1)
            return;
        OnItemActionRequested?.Invoke(index);
    }

    private void HandleItemSelection(InventoryItem item)
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

    private void ResetSelection()
    {
        itemDesc.ResetDesc();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach (InventoryItem item in listItems)
        {
            item.Deselect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }
}
