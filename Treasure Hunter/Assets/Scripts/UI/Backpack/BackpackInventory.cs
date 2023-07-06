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
    public Sprite image;
    public int quantity;
    public string title, description;

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

    private void HandleSwap(InventoryItem item)
    {

    }

    private void HandleEndDrag(InventoryItem item)
    {
        mouseFollower.Toggle(false);
    }

    private void HandleBeginDrag(InventoryItem item)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(image, quantity);
    }

    private void HandleShowItemActions(InventoryItem item)
    {

    }

    private void HandleItemSelection(InventoryItem item)
    {
        itemDesc.SetDesc(image, title, description);
        listItems[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDesc.ResetDesc();

        listItems[0].SetData(image, quantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
