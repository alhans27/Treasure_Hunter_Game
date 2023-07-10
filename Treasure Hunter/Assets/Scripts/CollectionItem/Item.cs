using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using UnityEngine.UI;

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
        // Destroy(gameObject);
        this.gameObject.SetActive(false);
        // this.gameObject.GetComponent<Image>().enabled = false;
    }

}
