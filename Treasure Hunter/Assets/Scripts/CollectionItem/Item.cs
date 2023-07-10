using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class Item : MonoBehaviour
{

    [field: SerializeField]
    public ItemSO InventoryItem { get; set; }

    [field: SerializeField]
    public int Quantity { get; set; }

    // [SerializeField]
    // private AudioSource audioSource;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        // audioSource.Play();
        Destroy(gameObject);
    }

}
