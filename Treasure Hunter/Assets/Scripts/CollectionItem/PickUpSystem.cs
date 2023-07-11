using System.Collections;
using System.Collections.Generic;
using ChestInventory.Model;
using Inventory.Model;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    // Just For Debug
    // [SerializeField]
    // private ChestInventorySO inventoryData;

    void OnTriggerEnter2D(Collider2D coll)
    {
        Item item = coll.GetComponent<Item>();
        if (item != null)
        {
            // inventoryData.AddItem(item.InventoryItem, item.Quantity);
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (reminder == 0)
                item.DestroyItem();
            else
                item.Quantity = reminder;
        }
    }
}
